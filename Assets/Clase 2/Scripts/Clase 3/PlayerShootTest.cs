using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform nozzle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Puedo instanciar el prefab con la posición del nozzle y la rotación 
            // en una sóla línea (en este caso no importa la rotación)
            Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
        }
    }
}
