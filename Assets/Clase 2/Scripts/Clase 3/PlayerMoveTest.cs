using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    float camWidth;
    float camHeight;

    Vector2 horzBound;
    Vector2 vertBound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camWidth = 4.2f;
        camHeight = 9.4f;

        horzBound = new Vector2(-camWidth / 2, camWidth / 2);
        vertBound = new Vector2(-camHeight / 2, camHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 5;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * 5;

        //transform.Translate(new Vector3(moveX, moveY, 0));

        float clampedX = Mathf.Clamp(transform.position.x + moveX, horzBound.x, horzBound.y);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, vertBound.x, vertBound.y);
      
        transform.position = new Vector3(clampedX, clampedY, 0);
       
        

    }
}
