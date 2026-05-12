using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  
    
    [Header("Game State")]
    public bool isGameActive = false;
    public bool isGameOver = false;
    
    [Header("Samples")]
    public int totalSamplesOnField;
    private int samplesDelivered = 0;
    
    [Header("Spawn")]
    public Transform spawnPoint;
    public GameObject roverPrefab;
    private GameObject currentRover;
    
    [Header("UI")]
    public UIManager uiManager;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        UpdateTotalSamples();
        samplesDelivered = 0;
        
        currentRover = GameObject.FindGameObjectWithTag("Player");
        
        if (uiManager != null)
            uiManager.ShowStartScreen();
        
        isGameActive = false;
    }
    
    // Обновляет общее количество образцов на поле
    public void UpdateTotalSamples()
    {
        totalSamplesOnField = FindObjectsOfType<SoilSample>().Length;
        Debug.Log($"Обновлено количество образцов на поле: {totalSamplesOnField}");
    }
    
    public void StartGame()
    {
        isGameActive = true;
        isGameOver = false;
        
        if (currentRover == null)
        {
            currentRover = GameObject.FindGameObjectWithTag("Player");
            
            if (currentRover == null && roverPrefab != null && spawnPoint != null)
            {
                currentRover = Instantiate(roverPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        
        if (currentRover != null)
        {
            RoverHealth health = currentRover.GetComponent<RoverHealth>();
            if (health != null)
            {
                health.onDeath.RemoveListener(OnRoverDeath);
                health.onDeath.AddListener(OnRoverDeath);
            }
        }
        
        if (uiManager != null)
            uiManager.HideAllScreens();
    }
    
    // Вызывается при смерти марсохода
    public void OnRoverDeath()
    {
        Debug.Log("Марсоход умер! Пересчитываем образцы...");
        
        UpdateTotalSamples();
        
        samplesDelivered = 0;
        
        if (totalSamplesOnField == 0)
        {
            GameOver();
        }
    }
    
    public void SampleDelivered()
    {
        if (!isGameActive) return;
        
        samplesDelivered++;
        Debug.Log($"Доставлено образцов: {samplesDelivered}/{totalSamplesOnField}");
        
        if (samplesDelivered >= totalSamplesOnField && totalSamplesOnField > 0)
        {
            WinGame();
        }
    }
    
    private void WinGame()
    {
        isGameActive = false;
        Debug.Log("ПОБЕДА! Все образцы доставлены на базу!");
    
        if (uiManager != null)
            uiManager.ShowWinScreen();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckRemainingSamples()
    {
        UpdateTotalSamples();
        
        if (totalSamplesOnField == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameActive = false;
        Debug.Log("Игра окончена! На поле не осталось образцов.");
        
        if (uiManager != null)
            uiManager.ShowLoseScreen();
    }
}