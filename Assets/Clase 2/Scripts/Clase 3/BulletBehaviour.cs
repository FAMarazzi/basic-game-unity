/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-13 19:59:16
 * @modify date 2026-04-13 19:59:16
 * @desc [description]
 */
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifeTime = 2f;

    SimpleExplosion explosion;

    void Start()
    {
        explosion = GetComponent<SimpleExplosion>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0, 1, 0) * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Enemies"))
            {
                Debug.Log("Bullet hit an enemy!");
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage); // Aplica daño al enemigo
                    explosion.CreateExplosion(transform.position); // Crea la explosión en la posición de la bala
                    
                }
            }


            Destroy(gameObject);
    }
}
