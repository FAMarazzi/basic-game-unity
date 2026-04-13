using UnityEngine;

public class EnemyBehaviourTest : MonoBehaviour
{
    private float speed;
    private bool movingLeft;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        // Asignamos una velocidad aleatoria entre 2 y 6
        speed = Random.Range(2f, 6f);
        
        // Determinamos dirección inicial: si está a la derecha, va a la izquierda
        movingLeft = transform.position.x > 0;
    }

    void Update()
    {
        // Movimiento constante
        float direction = movingLeft ? -1f : 1f;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        CheckBounds();
    }

    void CheckBounds()
    {
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);

        // Si se sale por el lado opuesto (fuera del rango 0 a 1 en el viewport)
        if (screenPos.x < -0.1f || screenPos.x > 1.1f)
        {
            // Decisión al azar: 50% volver, 50% desaparecer
            if (Random.value > 0.5f)
            {
                movingLeft = !movingLeft; // Cambia dirección
            }
            else
            {
                Destroy(gameObject); // Desaparece
            }
        }
    }
}
