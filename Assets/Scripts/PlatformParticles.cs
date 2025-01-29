using UnityEngine;

public class PlatformParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem breakParticles;
    [SerializeField] private Material platformMaterial;
    
    void Start()
    {
        if (breakParticles != null && platformMaterial != null)
        {
            var main = breakParticles.main;
            main.startColor = platformMaterial.color;
        }
    }
} 