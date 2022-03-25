using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplypad1 : MonoBehaviour
{


    private statManager manager;
    private bool addgap = false;

    // Start is called before the first frame update
    void Start()
    {


        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();



    }

    // Update is called once per frame
    void Update()
    {


        if (!addgap)
        {
            StartCoroutine(addSupplies());
        }
        


    }

    IEnumerator addSupplies()
    {

        addgap = true;   
        manager.addsupplies(20);
        yield return new WaitForSeconds(1);
        addgap = false;

    }


}
