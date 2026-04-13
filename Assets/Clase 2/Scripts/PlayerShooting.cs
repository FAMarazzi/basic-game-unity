using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform nozzle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Recordar que la posición de un hijo, es relativa siempre al padre, no al origen
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, nozzle.position, Quaternion.identity);
        }
    }
}
