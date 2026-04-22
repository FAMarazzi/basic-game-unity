// ExplosionParticle.cs
// Controla el movimiento, la gravedad y el desvanecimiento de cada punto.
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    private Vector2 velocity;
    private float lifeTime = 1.5f; // Cuánto dura la chispa
    private float timer = 0f;
    
    [Header("Físicas Simples")]
    public float gravityScale = 9.8f;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    /// <summary>
    /// Inicializa la chispa con una velocidad inicial.
    /// </summary>
    public void Initialize(Vector2 initialVelocity)
    {
        velocity = initialVelocity;
    }

    void Update()
    {
        // 1. Aplicar Gravedad a la velocidad (caída)
        velocity.y -= gravityScale * Time.deltaTime;

        // 2. Mover la posición
        transform.Translate(velocity * Time.deltaTime);

        // 3. Lógica de vida y desvanecimiento (Fade out)
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }

        // 4. Destruir cuando se vuelve invisible
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}