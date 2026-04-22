/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-21 08:50:20
 * @modify date 2026-04-21 08:50:20
 * @desc [description]
 */
 
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed=1;
    private bool movingLeft;
    private Camera cam;

    [SerializeField] private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //CHEQUEO SI es nulo para no tener errores al morir el personaje
        //al desactivar el gameobject
        //ESTO CREO QUE LO PUEDO SACAR PORQUE ERAN ERRORES QUE SEGUIAN SPAWNEANDO ENEMIGOS
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player no encontrado");
        }

        cam = Camera.main;
        }

    void Update()
    {
        //si el juego no arrancó, no se mueven, y si terminó (el personajé murio) igual
        if (!GameManager.Instance.start || GameManager.Instance.gameOver)
        {
            return;
        }
        // Movimiento constante
        Vector2 direction = (player.position - transform.position).normalized;  
        //sería como hacer un vector hacia el jugador para que siga la dirección mas "directa"
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
            //me di cuenta que playerHealth devolvía nulo al morir el personaje
            //y generaba errores por cada frame
            //ya que el gameobject ya no existe, así que solamente hago eso si no es null
            //ESTO CREO QUE LO PUEDO SACAR PORQUE ERAN ERRORES QUE SEGUIAN MOVIENDOSE LOS ENEMIGOS
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}
