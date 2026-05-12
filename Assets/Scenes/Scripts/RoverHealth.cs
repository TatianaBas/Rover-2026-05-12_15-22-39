using UnityEngine;
using UnityEngine.UI;

public class RoverHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("UI")]
    public Slider healthSlider;          // полоска здоровья
    public Image damageOverlay;          // красный экран (опционально)
    
    [Header("Audio")]
    public AudioClip damageSound;
    private AudioSource audioSource;
    
    [Header("Events")]
    public UnityEngine.Events.UnityEvent onDeath;
    
    private bool isShielded = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
    }
    
    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }
    
    private System.Collections.IEnumerator ShieldCoroutine(float duration)
    {
        isShielded = true;
        Debug.Log("🛡️ Щит активирован! Радиация не наносит урон.");
        yield return new WaitForSeconds(duration);
        isShielded = false;
        Debug.Log("Щит отключён.");
    }
    
    public void TakeDamage(float amount)
    {
        if (isShielded) return;  // щит блокирует урон
        
        currentHealth -= amount;
        
        // Эффекты при получении урона
        if (damageSound != null && audioSource != null)
            audioSource.PlayOneShot(damageSound);
            
        UpdateUI();
        Debug.Log($"Урон: {amount}. Здоровье: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateUI();
    }
    
    private void Die()
    {
        Debug.Log("Марсоход уничтожен радиацией! Возрождение...");
        
        // Сброс очков и инвентаря (образцы теряются)
        RoverInventory inventory = GetComponent<RoverInventory>();
        if (inventory != null)
        {
            inventory.ResetAfterDeath();
        }
        
        // Перемещение на спавн (через GameManager)
        if (GameManager.Instance != null && GameManager.Instance.spawnPoint != null)
        {
            transform.position = GameManager.Instance.spawnPoint.position;
            transform.rotation = GameManager.Instance.spawnPoint.rotation;
        }
        
        // Остановка физики
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        // Полное восстановление здоровья
        currentHealth = maxHealth;
        UpdateUI();
        
        // Проверяем, остались ли образцы на поле
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRoverDeath();
        }
    }
    
    private void UpdateUI()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameActive)
            return;
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
            
        if (damageOverlay != null)
        {
            float alpha = 1f - (currentHealth / maxHealth);
            Color color = damageOverlay.color;
            color.a = alpha * 0.5f;
            damageOverlay.color = color;
        }
    }
}