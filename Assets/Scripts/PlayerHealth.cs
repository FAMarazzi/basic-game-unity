/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-21 09:04:12
 * @modify date 2026-04-21 09:04:12
 * @desc [description]
 */

using System.Collections; //importo la librería para el IEnumerator
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [SerializeField] private Transform spawnPoint;
    private bool isDead = false;
    [SerializeField] private float blinkInterval = 0.1f;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioClip hitSound; // sonido al recibir danio
    [SerializeField] private AudioClip dieSound; // sonido al morir

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.ShowLives(currentLives);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentLives-=damage;
        Debug.Log("Vidas restantes: " +currentLives);
        GameManager.Instance.ShowLives(currentLives);

        if(currentLives>0)
        {
            if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
            Respawn();
        }
        else
        {
            if (dieSound != null) AudioSource.PlayClipAtPoint(dieSound, transform.position);
            Die();
        }

    }

    //función para parpadear cuando respawnea mientras
    //no es vulnerable
    IEnumerator BlinkEffect(float duration)
    {
    float timer = 0f;

    while (timer < duration)
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;

        yield return new WaitForSeconds(blinkInterval);
        timer += blinkInterval;
    }

    spriteRenderer.enabled = true;
    }
    void Respawn()
    {
        isDead = true;

        // ignoro las colisiones físicas entre la capa Jugador y Enemigos mientras está parpadeando y recién spawneado
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Jugador"), LayerMask.NameToLayer("Enemigos"), true);

        transform.position = spawnPoint.position; 
        //el transform position se mueve automáticamente a la posición de respawn

        StartCoroutine(BlinkEffect(1f)); // arranca y establece duración del efecto de parpadeo
        // pequeño delay que puse para evitar daño cuando recién apareces
        //llama a una función que devuelve el estado muerto a falso pero en 2 segundos
        Invoke(nameof(RestoreVulnerability), 1f);
        
    }
    
    void Die()
    {
        Debug.Log("GAME OVER");
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }

    void RestoreVulnerability()
    {
        isDead=false;
        // vuelvo a activar las colisiones físicas entre Jugador y Enemigos
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Jugador"), LayerMask.NameToLayer("Enemigos"), false);
    }
}
