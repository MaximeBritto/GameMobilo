using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject breakablePlatformPrefab;
    [SerializeField] private float breakablePlatformChance = 0.3f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float yOffset = 2.5f;
    
    [Header("Power-Ups")]
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float powerUpSpawnChance = 0.2f;
    
    private float currentHeight;
    
    void Start()
    {
        currentHeight = 0f;
        // Générer les premières plateformes
        for (int i = 0; i < 10; i++)
        {
            SpawnPlatform();
        }
    }
    
    void SpawnPlatform()
    {
        Vector3 position = new Vector3(
            Random.Range(minX, maxX),
            currentHeight,
            0
        );
        
        // Choisir aléatoirement entre plateforme normale et cassable
        GameObject prefabToSpawn = Random.value < breakablePlatformChance ? 
            breakablePlatformPrefab : platformPrefab;
            
        Instantiate(prefabToSpawn, position, Quaternion.identity);
        currentHeight += yOffset;
        
        // Chance de spawn d'un power-up sur la plateforme
        if (Random.value < powerUpSpawnChance)
        {
            Vector3 powerUpPosition = position + Vector3.up * 1f;
            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerUp], powerUpPosition, Quaternion.identity);
        }
    }
    
    void Update()
    {
        // Générer de nouvelles plateformes quand le joueur monte
        if (Camera.main.transform.position.y + 10 > currentHeight)
        {
            SpawnPlatform();
        }
    }
} 