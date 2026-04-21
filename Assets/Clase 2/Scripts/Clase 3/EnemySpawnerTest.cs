using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Calculamos los límites de la cámara en el mundo real
        float spawnX = cam.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x; // Justo a la derecha
        
        // Altura al azar (en el tercio superior de la pantalla)
        float randomY = cam.ViewportToWorldPoint(new Vector3(0, Random.Range(0.7f, 0.95f), 0)).y;

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
