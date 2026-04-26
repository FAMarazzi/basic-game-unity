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
    private PlayerMove playerMove;

    private float nozzleOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        // guardo la distancia a la que puse el nozzle originalmente
        nozzleOffset = nozzle.localPosition.magnitude;
        if (nozzleOffset == 0) nozzleOffset = 0.5f; // por las dudas
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

            Quaternion bulletRotation = nozzle.rotation;
            if (playerMove != null && playerMove.CurrentDirection != Vector2.zero)
            {
                // muevo el nozzle para que la bala salga por adelante del personaje y no desde la cabeza
                //tuve que usar esto porque al cambiar de animación pero no de rotación, el nozzle quedaba en el mismo lugar
                nozzle.localPosition = playerMove.CurrentDirection * nozzleOffset;

                float angle = Mathf.Atan2(playerMove.CurrentDirection.y, playerMove.CurrentDirection.x) * Mathf.Rad2Deg;
                bulletRotation = Quaternion.Euler(0, 0, angle - 90);
            }

            Instantiate(bulletPrefab, nozzle.position, bulletRotation);
            fireTimer = 0f;
        }
    }
}
