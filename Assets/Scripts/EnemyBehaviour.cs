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
    //Lo pongo para que el enemigo entre a la pantalla desde la derecha
    //y se mueva hacia el jugador una vez que pasó la pared
    [SerializeField] private float enterX = 1.38f;
    private bool passedWall = false;
    private Camera cam;
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //si el juego no arrancó, no se mueven, y si terminó (el personajé murio) igual
        if (!GameManager.Instance.start || GameManager.Instance.gameOver)
        {
            return;
        }
        // Movimiento
        Vector2 direction;
        
        if (!passedWall)
        {
            direction = Vector2.left;
            if (transform.position.x <= enterX)
            {
                passedWall = true;
            }
        }
        else
        {
            direction = (player.position - transform.position).normalized;  
            //sería como hacer un vector hacia el jugador para que siga la dirección mas "directa"
        }

        // Rotación: hace que el sprite mire hacia donde se está moviendo
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        // Usamos físicas igual que en el Player para que no se queden atascados
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
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
