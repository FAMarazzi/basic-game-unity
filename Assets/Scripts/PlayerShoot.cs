/**
 * @author Federico Marazzi
 * @email federicoandresmarazzi@gmail.com
 * @create date 2026-04-13 19:59:00
 * @modify date 2026-04-13 19:59:00
 * @desc [description]
 */
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform nozzle;
    //controlar el ratio de disparo desde el editor
    [SerializeField] private float fireRate = 0.3f;
    private float fireTimer=0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.start || GameManager.Instance.gameOver)
        {
            return;
        }
        Debug.Log("Update Shoot corriendo");
        //al ser un método void puedo retornar para abandonar el método en cada frame
        //en el que el start del gamemanager no sea true, si es true, ya dejo de retornar
        //y ejecuto el método con normalidad.

        fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            Debug.Log("Está detectando barra esp");
            Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
            fireTimer = 0f;
        }
    }
}
