

using UnityEngine;

public class cameracontroller : MonoBehaviour
{
   
    public float speedZ = 0f;
    public float speedX = 0f;

    public float panBorderThickness = 10f;


    void Update()
    {

        transform.Translate(speedX * Time.deltaTime, 0, speedZ * Time.deltaTime, Space.Self);


        if (Input.GetKeyDown(KeyCode.W))
        {

            speedZ = 100f;

        }
        if (Input.GetKeyUp(KeyCode.W))
        {

            speedZ = 0f;

        }


        if (Input.GetKeyDown(KeyCode.S))
        {

            speedZ = -100f;

        }
        if (Input.GetKeyUp(KeyCode.S))
        {

            speedZ = 0f;

        }


        if (Input.GetKeyDown(KeyCode.A))
        {

            speedX = -100f;

        }
        if (Input.GetKeyUp(KeyCode.A))
        {

            speedX = 0f;

        }


        if (Input.GetKeyDown(KeyCode.D))
        {

            speedX = 100f;

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

