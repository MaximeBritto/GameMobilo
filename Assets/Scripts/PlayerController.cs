using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float zLimit = 2f; // Limite de mouvement en Z
    
    [Header("Score")]
    public float score = 0f;
    private float maxHeight = 0f;
    
    [Header("PowerUps")]
    private bool hasDoubleSaut = false;
    private bool hasSpeedBoost = false;
    private bool hasShield = false;
    private float powerUpTimer = 0f;
    
    [Header("Life")]
    [SerializeField] private int maxLives = 3;
    public int currentLives;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;
    
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private TrailRenderer speedTrail;
    [SerializeField] private GameObject shieldVisual;
    
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 originalScale;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        currentLives = maxLives;
        
        // Configuration du Rigidbody pour un meilleur contrôle
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        if (speedTrail) speedTrail.enabled = false;
        if (shieldVisual) shieldVisual.SetActive(false);
    }
    
    void Update()
    {
        // Mise à jour du score
        if (transform.position.y > maxHeight)
        {
            score += transform.position.y - maxHeight;
            maxHeight = transform.position.y;
        }
        
        // Mouvement horizontal avec limite en Z
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y,
            Mathf.Clamp(rb.position.z + verticalInput * moveSpeed * Time.deltaTime, -zLimit, zLimit)
        );
        
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, 0);
        rb.position = new Vector3(rb.position.x, rb.position.y, movement.z);
        
        // Rotation du personnage selon la direction
        if (horizontalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0, horizontalInput > 0 ? 90 : -90, 0);
        }
        
        // Saut automatique
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            if (jumpEffect) jumpEffect.Play();
            
            // Animation de squash and stretch
            StartCoroutine(SquashAndStretch());
        }
        
        // Gestion des power-ups
        HandlePowerUps();
    }
    
    private System.Collections.IEnumerator SquashAndStretch()
    {
        // Écrasement
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.8f, originalScale.z * 1.2f);
        yield return new WaitForSeconds(0.1f);
        
        // Étirement
        transform.localScale = new Vector3(originalScale.x * 0.8f, originalScale.y * 1.2f, originalScale.z * 0.8f);
        yield return new WaitForSeconds(0.1f);
        
        // Retour à la normale
        transform.localScale = originalScale;
    }
    
    private void HandlePowerUps()
    {
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                DisableAllPowerUps();
            }
        }
        
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            
            // Vérifier si c'est une plateforme cassable
            BreakablePlatform breakable = collision.gameObject.GetComponent<BreakablePlatform>();
            if (breakable != null)
            {
                breakable.Break();
            }
        }
    }
    
    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        powerUpTimer = duration;
        switch (type)
        {
            case PowerUpType.DoubleSaut:
                hasDoubleSaut = true;
                jumpForce *= 1.5f;
                break;
            case PowerUpType.SpeedBoost:
                hasSpeedBoost = true;
                moveSpeed *= 1.5f;
                if (speedTrail) speedTrail.enabled = true;
                break;
            case PowerUpType.Shield:
                hasShield = true;
                if (shieldVisual) shieldVisual.SetActive(true);
                break;
        }
    }
    
    private void DisableAllPowerUps()
    {
        if (hasDoubleSaut)
        {
            jumpForce /= 1.5f;
        }
        if (hasSpeedBoost)
        {
            moveSpeed /= 1.5f;
            if (speedTrail) speedTrail.enabled = false;
        }
        if (hasShield && shieldVisual)
        {
            shieldVisual.SetActive(false);
        }
        
        hasDoubleSaut = false;
        hasSpeedBoost = false;
        hasShield = false;
    }
    
    public void TakeDamage()
    {
        if (isInvincible || hasShield) return;
        
        currentLives--;
        if (currentLives <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            isInvincible = true;
            invincibilityTimer = 2f;
        }
    }
} 