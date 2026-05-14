using UnityEngine;
using UnityEngine.InputSystem;  

public class ShieldController : MonoBehaviour
{
    [Header("Shield Settings")]
    public float shieldDuration = 3f;
    public float cooldown = 5f;
    
    [Header("Visual")]
    public GameObject shieldVisual;
    public AudioClip shieldActivateSound;
    
    private RoverHealth roverHealth;
    private AudioSource audioSource;
    private bool isOnCooldown = false;
    
    void Start()
    {
        roverHealth = GetComponent<RoverHealth>();
        audioSource = GetComponent<AudioSource>();
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }
    
    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameActive)
            return;
        // Активация щита по клавише "E" (через новую систему ввода)
        if (Keyboard.current.eKey.wasPressedThisFrame && !isOnCooldown)
        {
            ActivateShield();
        }
    }
    
    void ActivateShield()
    {
        if (roverHealth != null)
            roverHealth.ActivateShield(shieldDuration);
            
        if (shieldVisual != null)
            shieldVisual.SetActive(true);
            
        if (shieldActivateSound != null && audioSource != null)
            audioSource.PlayOneShot(shieldActivateSound);
            
        Debug.Log($"Щит активирован на {shieldDuration} секунд!");
        
        StartCoroutine(ShieldCooldown());
    }
    
    private System.Collections.IEnumerator ShieldCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(shieldDuration);
        
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
            
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
        Debug.Log("Щит готов к использованию!");
    }
}
