using UnityEngine;
using UnityEngine.UI;

public class RoverInventory : MonoBehaviour
{
    public int maxCapacity = 5;
    private int currentCount = 0;
    private int totalScore = 0;

    public Text inventoryText;
    public Text scoreText;

    [Header("Audio")]
    public AudioClip collectSound;
    public AudioClip depositSound;
    private AudioSource audioSource;    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
    }

    public bool CollectSample(SoilSample sample)
    {
        if (currentCount >= maxCapacity)
        {
            Debug.Log("Инвентарь полон");
            return false;
        }

        currentCount += sample.value;
        Debug.Log($"Собрано: {currentCount}/{maxCapacity}");

        if (collectSound != null && audioSource != null)
            audioSource.PlayOneShot(collectSound);

        UpdateUI();
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base") && currentCount > 0)
        {
            int deliveredCount = currentCount;  // запоминаем, сколько сдаём
            int points = deliveredCount * 100;
            
            totalScore += points;
            currentCount = 0;
            
            Debug.Log($"Сдано {deliveredCount} образцов! +{points} очков! Всего: {totalScore}");
            
            // Звук сдачи
            if (depositSound != null && audioSource != null)
                audioSource.PlayOneShot(depositSound);
            
            // Сообщаем GameManager, что образцы доставлены
            if (GameManager.Instance != null)
            {
                for (int i = 0; i < deliveredCount; i++)
                {
                    GameManager.Instance.SampleDelivered();
                }
            }
            
            UpdateUI();
        }
    }

    public void LosePoints()
    {
        totalScore /=  2;
        UpdateUI();
        Debug.Log("Очки потеряны из-за смерти!");
    }

    public void ResetAfterDeath()
    {
        totalScore = 0;
        currentCount = 0;
        UpdateUI();
        Debug.Log("Очки и инвентарь сброшены после смерти.");
    }

    private void UpdateUI()
    {
        if (inventoryText != null)
            inventoryText.text = $"{currentCount}/{maxCapacity}";
        if (scoreText != null)
            scoreText.text = $"{totalScore}";
    }
}