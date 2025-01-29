using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.3f;
    [SerializeField] private Vector3 offset = new Vector3(10, 5, -10);
    [SerializeField] private float lookAheadAmount = 2f;
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        // Position cible avec décalage
        Vector3 desiredPosition = target.position + offset;
        
        // Ajout de look-ahead basé sur la vitesse horizontale
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            desiredPosition += new Vector3(targetRb.linearVelocity.x * lookAheadAmount, 0, 0);
        }
        
        // Smooth follow
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        // La caméra regarde toujours le joueur
        transform.LookAt(target);
    }
} 