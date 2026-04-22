using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    [Header("Configuración de la Explosión")]
    [Tooltip("El objeto que actuará como chispa (un pequeño círculo o cuadrado blanco).")]
    public GameObject sparkPrefab;
    
    [Tooltip("Cantidad de puntos/chispas que saldrán de la explosión.")]
    public int sparkCount = 20;
    
    [Tooltip("Fuerza mínima y máxima de la explosión.")]
    public float minForce = 5f;
    public float maxForce = 10f;

    public void CreateExplosion(Vector3 position)
    {
        for (int i = 0; i < sparkCount; i++)
        {
            GameObject spark = Instantiate(sparkPrefab, position, Quaternion.identity);
            
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            float force = Random.Range(minForce, maxForce);
            
            ExplosionParticle particleScript = spark.GetComponent<ExplosionParticle>();
            if (particleScript != null)
            {
                particleScript.Initialize(direction * force);
            }
        }
    }

}