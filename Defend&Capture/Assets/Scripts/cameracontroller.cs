

using UnityEngine;

public class cameracontroller : MonoBehaviour
{
   
    public float speedZ = 0f; // initialises speed ofset as 0
    public float speedX = 0f;
    private Vector3 startPos; //saves starting point for use of returning to base
    void Start()
    {
        startPos = gameObject.transform.position; // gets cameras starting location (starts at base)
    }
    void Update()
    {
        transform.Translate(speedX * Time.deltaTime, 0 , speedZ * Time.deltaTime, Space.Self);

        //when WASD are pressed the X and Y it replaces these translate() values to move the camera on the X axis 
        //Q and E are pressed so achive the same result but using the rotate method and moving 

        if (Input.GetKeyDown(KeyCode.Alpha1))   // pressing 1 will return teh camera parent to its starting point - at the front of home base
        {
            gameObject.transform.position = startPos;
           
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            speedZ = 200f;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            speedZ = 0f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            speedZ = -200f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            speedZ = 0f;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            speedX = -200f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            speedX = 0f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            speedX = 200f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            speedX = 0f;
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0, 1, 0);
        }

    }
}

