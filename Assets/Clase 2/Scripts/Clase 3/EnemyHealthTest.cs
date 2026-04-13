using UnityEngine;

public class EnemyHealthTest : MonoBehaviour
{ 
    [SerializeField] private float healthTest = 40;

    
    
    public void TakeDamage(float damage)
    {
        healthTest -= damage;
        Debug.Log("Enemy took damage. Current health: " + healthTest);

        GameManagerTest.Instance.AddScore((int)damage);

        if (healthTest <= 0)
        {
            
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
