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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //debo asignarlo de esta manera ya que no se permite poner 
        // algo del gameobject en un prefab (el gameobject aún no fue instanciado)

        cam = Camera.main;
        // Asignamos una velocidad aleatoria entre 2 y 6
    }

    void Update()
    {
        // Movimiento constante
        Vector2 direction = (player.position - transform.position).normalized;  
        //sería como hacer un vector hacia el jugador para que siga la dirección mas "directa"
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
