/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-21 08:21:30
 * @modify date 2026-04-21 08:21:30
 * @desc [description]
 */

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{ 
    [SerializeField] private float health = 40;
    [SerializeField] private int scoreValue = 100;
    private bool isDead = false;

    [SerializeField] private AudioClip hitSound; // sonido al recibir danio
    [SerializeField] private AudioClip dieSound; // sonido al morir
    
    
    public void TakeDamage(float damage)
    {
        
        if (isDead) return; //retorno para salir de la función si el personaje ya está muerto
                            //para que no chequee su muerte 2 veces

        health -= damage; //le resto el daño a su vida
        Debug.Log("El enemigo sufrió daño. Vida actual: " + health);
        

        if (health <= 0)
        {
            if (dieSound != null) AudioSource.PlayClipAtPoint(dieSound, transform.position);
            isDead=true;
            GameManager.Instance.AddScore(scoreValue); //y en el puntaje sumo el valor que me da matar un personaje
            Die();
        }
        else
        {
            if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position); //si está cargado el sonido, lo reproduzco
            // si no murió con este tiro, le tiro la red (se pone amarillo y lo hace lento)
            //si murió no se pone amarillo
            EnemyBehaviour behaviour = GetComponent<EnemyBehaviour>();
            if (behaviour != null)
            {
                behaviour.ApplySlowdown(0.5f); // le saca la mitad de la velocidad
            }
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
