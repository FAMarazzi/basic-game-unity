using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]private float bulletSpeed=10f;
    //con public lo paso al editor al inspector para modificarla
    //pero si no quiero que sea public, lo hago privado y le agrego el serializefield


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
        //La forma mas rapida de destruir un objeto es con Destroy indicando el objeto y el tiempo
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, 1f, 0f) * Time.deltaTime * bulletSpeed);
    }
}
