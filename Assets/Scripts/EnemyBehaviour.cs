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
    private Animator anim;
    private bool isSlowed = false; // para saber si ya le pegaron y lo ralentizaron

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
        anim = GetComponent<Animator>();
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

        // le paso la direccion al animator
        if (anim != null && direction != Vector2.zero)
        {
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
        }

        // espejar el sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // lo invierto para que apunte bien
            if (direction.x < 0) sr.flipX = true;
            else if (direction.x > 0) sr.flipX = false;
        }

        // uso fisicas para que no se trabe
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

    public void ApplySlowdown(float slowFactor)
    {
        // me aseguro de que no se ralentice 20 veces si le pega mucho
        if (!isSlowed)
        {
            isSlowed = true;
            speed *= slowFactor;
            // le cambio el tono dependiendo del nivel
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
                {
                    sr.color = new Color(0.85f, 0.8f, 0.1f); // dorado un poco mas oscuro para el lv 2
                }
                else
                {
                    sr.color = new Color(1f, 0.9f, 0.2f); // amarillo dorado brillante para el nivel 1
                }
            }
        }
    }
}

