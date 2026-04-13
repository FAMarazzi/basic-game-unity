using UnityEngine;
using TMPro;

public class GameManagerTest : MonoBehaviour
{
    // Instancia estática única del GameManager
    public static GameManagerTest Instance { get; private set; }

    [Header("Estadísticas del Juego")]
    [SerializeField] private int score = 0;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Hace que el GameManager persista entre cambios de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruimos esta para evitar duplicados
            Destroy(gameObject);
        }
    }

     public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntuación actualizada: " + score);
        
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString("D4");
        }
       
    }

    /// <summary>
    /// Método para obtener la puntuación actual.
    /// </summary>
    public int GetCurrentScore()
    {
        return score;
    }
}
