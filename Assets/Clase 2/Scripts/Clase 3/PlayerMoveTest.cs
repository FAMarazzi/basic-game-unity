using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{

    [Header("Tamaño de la visión de la cámara")]
    [SerializeField] private float camWidth = 4.1f;
    [SerializeField] private float camHeight = 9.4f;
    //lo pongo para que esté visible en el editor
    //porque a veces cambio el proyecto de computadora 
    // y tienen diferentes resoluciones las pantallas

    Vector2 horzBound;
    Vector2 vertBound;
    [Header("Movimiento del personaje")]
    [SerializeField] private float speed = 5f;
    //determina el tamaño de la vista de la cámara

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //creo un sistema de coordenadas para hacerlo
        //relativo al centro
        //-2.1f a 2.1f por ej
        horzBound = new Vector2(-camWidth / 2, camWidth / 2);
        vertBound = new Vector2(-camHeight / 2, camHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //transform.Translate(new Vector3(moveX, moveY, 0));

        //limita un valor numérico dentro de un rango, si el valor es menor que el mínimo
        //devuelve el minimo, si es mayor que el max, devuelve el max,
        //si no es ninguno de los dos casos, devuelve el valor original
        float clampedX = Mathf.Clamp(transform.position.x + moveX, horzBound.x, horzBound.y);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, vertBound.x, vertBound.y);
      
        transform.position = new Vector3(clampedX, clampedY, 0);
       
        

    }
}
