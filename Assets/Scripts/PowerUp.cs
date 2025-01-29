using UnityEngine;

public enum PowerUpType
{
    DoubleSaut,
    SpeedBoost,
    Shield
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType type;
    [SerializeField] private float duration = 5f;
    [SerializeField] private ParticleSystem collectEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivatePowerUp(type, duration);
                if (collectEffect != null)
                {
                    Instantiate(collectEffect, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
} 