
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 200f;

    public float panBorderThickness = 10f;


    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        if (Input.GetKey("q")){
            rot.y -= speed * Time.deltaTime;

        }
        if (Input.GetKey("e"))
        {
            rot.y += speed * Time.deltaTime;

        }


        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness){

            pos.z += speed * Time.deltaTime;


        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {

            pos.z -= speed * Time.deltaTime;


        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {

            pos.x += speed * Time.deltaTime;


        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {

            pos.x -= speed * Time.deltaTime;


        }

        transform.position = pos;
        transform.rotation = rot;


    }
}
