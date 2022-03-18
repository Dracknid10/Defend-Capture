using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactor1 : MonoBehaviour
{
    // Start is called before the first frame update



    private statManager manager;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        manager.addreactorlvl();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
