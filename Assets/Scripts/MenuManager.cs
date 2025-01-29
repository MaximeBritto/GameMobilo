using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject[] powerUpIndicators;
    
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
} 