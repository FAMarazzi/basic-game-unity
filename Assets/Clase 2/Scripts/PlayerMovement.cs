using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed=5f;
    //no se aconseja poner público pero lo hice para modificarla en inspector

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizMove, vertMove, 0f) * Time.deltaTime * speed);
    }
}
