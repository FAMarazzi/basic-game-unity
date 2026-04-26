/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-20 20:30:37
 * @modify date 2026-04-20 20:30:37
 * @desc [description]
 */

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estadísticas del Juego")]
    [SerializeField] private int score = 0;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject gameOverText;
    public GameObject victoryText; // texto para cuando gano el juego

    // variables para el inicio del juego
    [Header("Inicio del Juego")]
    public bool start = false; 
    [HideInInspector] public bool canStart = false; // le pongo cooldown para que no arranque de una si toco sin querer
    [HideInInspector] public int enemiesKilled = 0; // cuento los enemigos muertos en este nivel
    public bool gameOver=false;
    public GameObject startText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // paso las variables de la ui de la escena nueva al gamemanager viejo
            Instance.scoreText = this.scoreText;
            Instance.livesText = this.livesText;
            Instance.gameOverText = this.gameOverText;
            Instance.victoryText = this.victoryText;
            Instance.startText = this.startText;

            // freno el inicio para que haya que tocar una tecla
            Instance.start = false;
            Instance.canStart = false;
            Instance.enemiesKilled = 0; // reinicio muertes
            Instance.Invoke(nameof(AllowStart), 1f); 
            // 1 seg de delay porque me pasaba que arrancaba solo si venia moviendome

            if (Instance.startText != null)
            {
                Instance.startText.SetActive(true);
            }

            // si es el nivel 1 reinicio score y el game over
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Instance.score = 0;
                Instance.gameOver = false;
            }

            // actualizo texto
            Instance.AddScore(0);

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (startText != null)
            startText.SetActive(true);

        Invoke(nameof(AllowStart), 1f); // delay inicial
    }

    void AllowStart()
    {
        canStart = true;
    }

    public void ActivarInicio()
    {
        if (!start)
        {
            start = true;

            if (startText != null)
                startText.SetActive(false);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        enemiesKilled++; // le sumo 1 al contador de kills
        
        Debug.Log("Puntuación actualizada: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString("D4");
        }

        // busco el spawner para ver cuantos faltan
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            // me fijo si maté a todos
            if (enemiesKilled >= spawner.maxEnemiesToSpawn)
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneManager.LoadScene(2);
                }
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    GameCompleted();
                }
            }
        }
    }
    public void ShowLives(int currentLives)
    {
        livesText.text = "Vidas: " + currentLives;
    }

    
    public void GameOver()
    {
        gameOver = true;

        Debug.Log("GAME OVER");

        // mostrar UI
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }

        // frenar spawn
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        // volver al menú después de 2 segundos
        Invoke(nameof(LoadMenu), 2f);

    }
    public void GameCompleted()
    {
        gameOver = true;

        // frenar spawn
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null) spawner.StopSpawning();

        // prendo el texto de victoria si existe
        if (victoryText != null)
        {
            victoryText.SetActive(true);
        }

        // 4 segs para que se lea bien
        Invoke(nameof(LoadMenu), 4f);
    }

    void LoadMenu()
    {
        // Cargo la escena por su número de índice
        SceneManager.LoadScene(0);
    }
    public int GetCurrentScore()
    {
        return score;
    }
}