using UnityEngine;

public class PowerUpVisual : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.2f;
    
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        // Rotation
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        
        // Mouvement de flottement
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
} 