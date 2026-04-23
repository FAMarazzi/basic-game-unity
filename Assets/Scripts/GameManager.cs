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

    // ===== AGREGADO =====
    [Header("Inicio del Juego")]
    public bool start = false; // MISMO nombre que usabas
    public bool gameOver=false;
    public GameObject startText; // MISMO nombre

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (startText != null)
            startText.SetActive(true);
    }

    // ===== AGREGADO =====
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
        Debug.Log("Puntuación actualizada: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString("D4");
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
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public int GetCurrentScore()
    {
        return score;
    }
}