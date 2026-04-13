using UnityEngine;

public class BulletBehaviourTest : MonoBehaviour
{
    [SerializeField] private float bulletSpeedTest = 10f;
    [SerializeField] private float damageTest = 20f;
    [SerializeField] private float lifeTimeTest = 2f;

    SimpleExplosion explosion;
    
    // Update is called once per frame
    void Update()
    {
        explosion = GetComponent<SimpleExplosion>();

        transform.Translate(new Vector3(0, 1, 0) * bulletSpeedTest * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Enemies"))
            {
                Debug.Log("Bullet hit an enemy!");
                EnemyHealthTest enemyHealth = collision.gameObject.GetComponent<EnemyHealthTest>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageTest); // Aplica daño al enemigo
                    explosion.CreateExplosion(transform.position); // Crea la explosión en la posición de la bala
                    
                }
            }


            Destroy(gameObject);
    }
}
