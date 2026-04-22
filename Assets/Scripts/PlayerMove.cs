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
        //hice esto para evitar que mueva y rote antes de arrancar
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // si el juego terminó, no hacer nada, salir del update
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        
        //inicio del juego
        if (!GameManager.Instance.start)
        {
            if (inputX != 0 || inputY != 0)
            {
                GameManager.Instance.ActivarInicio();
            }
            return;
        }
        
        Vector2 rotateDirection = new Vector2(inputX, inputY);

        float moveX = 0f;
        float moveY = 0f;

        // prioridad horizontal
        if (inputX != 0)
        {
            moveX = inputX * Time.deltaTime * speed;
            rotateDirection = new Vector2(inputX, 0);
        }
        else if (inputY != 0) //solamente permito mover en y si no hay presión horiz
        {
            moveY = inputY * Time.deltaTime * speed; 
            rotateDirection = new Vector2(0, inputY);
        }
        if (rotateDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        // Si hay input de movimiento, actualizamos la dirección guardada
        if (moveX != 0)
        {
            currentDirection = new Vector2(Mathf.Sign(moveX), 0);
        }
        else if (moveY != 0)
        {           
            currentDirection = new Vector2(0, Mathf.Sign(moveY));
        }
        // Si no hay input, usamos la última dirección
        if (GameManager.Instance.start && moveX == 0 && moveY == 0)
        {
            moveX = currentDirection.x * Time.deltaTime * speed;
            moveY = currentDirection.y * Time.deltaTime * speed;
        }
        //transform.Translate(new Vector3(moveX, moveY, 0));

        //limita un valor numérico dentro de un rango, si el valor es menor que el mínimo
        //devuelve el minimo, si es mayor que el max, devuelve el max,
        //si no es ninguno de los dos casos, devuelve el valor original
        float clampedX = Mathf.Clamp(transform.position.x + moveX, horzBound.x, horzBound.y);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, vertBound.x, vertBound.y);
      
        transform.position = new Vector3(clampedX, clampedY, 0);
       
        

    }
}
