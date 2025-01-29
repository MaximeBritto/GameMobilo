using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private PlayerController player;
    
    [Header("PowerUp Indicators")]
    [SerializeField] private GameObject doubleSautIndicator;
    [SerializeField] private GameObject speedBoostIndicator;
    [SerializeField] private GameObject shieldIndicator;
    
    void Update()
    {
        if (player != null)
        {
            UpdateHUD();
        }
    }
    
    void UpdateHUD()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {Mathf.Floor(player.score)}";
        }
        
        if (livesText != null)
        {
            livesText.text = $"Vies: {player.currentLives}";
        }
    }
} 