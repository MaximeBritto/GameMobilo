using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float breakDelay = 0.5f;
    [SerializeField] private ParticleSystem breakEffect;
    
    public void Break()
    {
        // Activer l'effet de particules
        if (breakEffect != null)
        {
            breakEffect.Play();
            // Détacher le système de particules pour qu'il continue après la destruction
            breakEffect.transform.parent = null;
            Destroy(breakEffect.gameObject, breakEffect.main.duration);
        }
        
        // Désactiver le rendu et le collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        Destroy(gameObject, breakDelay);
    }
} 