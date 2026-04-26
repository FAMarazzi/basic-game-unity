/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-21 20:36:04
 * @modify date 2026-04-21 20:36:04
 * @desc [description]
 */

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public int maxEnemiesToSpawn = 10; // max enemigos
    private int spawnedEnemies = 0; // cuantos salieron
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    void SpawnEnemy()
    {
         if (!GameManager.Instance.start || GameManager.Instance.gameOver)
        {
            return;
        }

        // me fijo si ya salieron todos
        if (spawnedEnemies >= maxEnemiesToSpawn)
        {
            return;
        }

        // Calculamos los límites de la cámara en el mundo real
        float spawnX = cam.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x; // Justo a la derecha
        
        // Altura al azar (en el tercio superior de la pantalla para que pasen por el hueco entre paredes)
        float randomY = cam.ViewportToWorldPoint(new Vector3(0, Random.Range(0.7f, 0.8f), 0)).y;

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        spawnedEnemies++; // le sumo 1
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }
}
