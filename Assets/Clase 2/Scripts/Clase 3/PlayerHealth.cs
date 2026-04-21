/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-21 09:04:12
 * @modify date 2026-04-21 09:04:12
 * @desc [description]
 */


using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [SerializeField] private Transform spawnPoint;
    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentLives-=damage;
        Debug.Log("Vidas restantes: " +currentLives);

        if(currentLives>0)
        {
            Respawn();
        }
        else
        {
            Die();
        }

    }
    
    void Respawn()
    {
        isDead = true;

        transform.position = spawnPoint.position; 
        //el transform position se mueve automáticamente a la posición de respawn

        // pequeño delay que puse para evitar daño cuando recién apareces
        //llama a una función que devuelve el estado muerto a falso pero en 2 segundos
        Invoke(nameof(RestoreVulnerability), 0.2f);
        
    }
    void Die()
    {
        Debug.Log("GAME OVER");

        // TODO: ESTO TENGO QUE CONECTARLO AL LEVEL MANAGER DESPUES
        gameObject.SetActive(false);
    }

    void RestoreVulnerability()
    {
        isDead=false;        
    }
}
