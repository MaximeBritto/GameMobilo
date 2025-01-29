using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDistance = 3f;
    private Vector3 startPos;
    private bool movingRight = true;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        float movement = moveSpeed * Time.deltaTime;
        if (movingRight)
        {
            if (transform.position.x < startPos.x + moveDistance)
            {
                transform.Translate(Vector3.right * movement);
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if (transform.position.x > startPos.x - moveDistance)
            {
                transform.Translate(Vector3.left * movement);
            }
            else
            {
                movingRight = true;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
} 