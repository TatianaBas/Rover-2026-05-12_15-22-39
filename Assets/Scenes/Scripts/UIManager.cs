using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    
    [Header("Buttons")]
    public Button startButton;
    public Button winRestartButton;    
    public Button loseRestartButton;   

    
    private void Start()
    {
        // Назначаем кнопки
        if (startButton != null)
            startButton.onClick.AddListener(() => GameManager.Instance.StartGame());
            
            
        if (winRestartButton != null)
            winRestartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());

        if (loseRestartButton != null)
            loseRestartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
            
        ShowStartScreen();
    }
    
    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
    
    public void ShowWinScreen()
    {
        startScreen.SetActive(false);
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
    }
    
    public void ShowLoseScreen()
    {
        startScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(true);
    }
    
    public void HideAllScreens()
    {
        startScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
}