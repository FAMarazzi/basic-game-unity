/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-13 19:58:09
 * @modify date 2026-04-13 19:58:09
 * @desc [description]
 */
using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Tamaño de la visión de la cámara")]
    [SerializeField] private float camWidth = 4.1f;
    [SerializeField] private float camHeight = 9.4f;
    //lo pongo para que esté visible en el editor
    //porque a veces cambio el proyecto de computadora 
    // y tienen diferentes resoluciones las pantallas

    // Dirección actual del movimiento (SIEMPRE se usa para moverse)
    private Vector2 currentDirection = Vector2.up; // arranca yendo hacia arriba
    //getter para obtenerlo desde el script de disparo y ver para donde apunta
    public Vector2 CurrentDirection { get { return currentDirection; } }
    Vector2 horzBound;
    Vector2 vertBound;
    [Header("Movimiento del personaje")]
    [SerializeField] private float speed = 5f;
    //determina el tamaño de la vista de la cámara

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //creo un sistema de coordenadas para hacerlo
        //relativo al centro
        //-2.1f a 2.1f por ej
        horzBound = new Vector2(-camWidth / 2, camWidth / 2);
        vertBound = new Vector2(-camHeight / 2, camHeight / 2);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //hice esto para evitar que mueva y rote antes de arrancar
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // si el juego terminó salir del update y poner false la animacion de correr
        if (GameManager.Instance.gameOver)
        {
            if (anim != null) anim.SetBool("isRunning", false);
            return;
        }
        
        //inicio del juego
        //si no se presiono nada para empezar, salir del update y poner false la animacion de correr
        if (!GameManager.Instance.start)
        {
            // me fijo si paso el tiempo para evitar arrancar por error
            if (GameManager.Instance.canStart && (inputX != 0 || inputY != 0))
            {
                GameManager.Instance.ActivarInicio();
            }
            if (anim != null) anim.SetBool("isRunning", false);
            return;
        }
        
        Vector2 rotateDirection = new Vector2(inputX, inputY);

        float velX = 0f;
        float velY = 0f;

        // prioridad horizontal
        if (inputX != 0)
        {
            velX = inputX * speed;
            rotateDirection = new Vector2(inputX, 0);
        }
        else if (inputY != 0) //solamente permito mover en y si no hay presión horiz
        {
            velY = inputY * speed; 
            rotateDirection = new Vector2(0, inputY);
        }
        // Si hay input de movimiento, actualizamos la dirección guardada
        if (velX != 0)
        {
            currentDirection = new Vector2(Mathf.Sign(velX), 0);
        }
        else if (velY != 0)
        {           
            currentDirection = new Vector2(0, Mathf.Sign(velY));
        }
        // Si no hay input, usamos la última dirección
        if (GameManager.Instance.start && velX == 0 && velY == 0)
        {
            velX = currentDirection.x * speed;
            velY = currentDirection.y * speed;
        }

        rb.linearVelocity = new Vector2(velX, velY);

        if (anim != null)
        {
            anim.SetFloat("MoveX", currentDirection.x);
            anim.SetFloat("MoveY", currentDirection.y);
            anim.SetBool("isRunning", velX != 0 || velY != 0);
        }

        // espejar el sprite automáticamente si voy a la derecha (porque el sprite original mira a la izq)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (currentDirection.x < 0) sr.flipX = false;
            else if (currentDirection.x > 0) sr.flipX = true;
        }

        //limita un valor numérico dentro de un rango, si el valor es menor que el mínimo
        //devuelve el minimo, si es mayor que el max, devuelve el max,
        //si no es ninguno de los dos casos, devuelve el valor original
        float clampedX = Mathf.Clamp(transform.position.x, horzBound.x, horzBound.y);
        float clampedY = Mathf.Clamp(transform.position.y, vertBound.x, vertBound.y);
      
        if (transform.position.x != clampedX || transform.position.y != clampedY)
        {
            transform.position = new Vector3(clampedX, clampedY, 0);
        }
       
        

    }
}
