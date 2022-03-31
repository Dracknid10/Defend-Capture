using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private float movementSpeed = 300;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * Time.deltaTime * movementSpeed;


    }

 
}
