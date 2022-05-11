using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplypad1 : MonoBehaviour
{


    private statManager manager;
    private bool addgap = false;

    public GameObject PadOn;
    public GameObject buttonObject;

    // Start is called before the first frame update
    void Start()
    {


        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        buttonObject.SetActive(false);


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
        manager.addsupplies(50);
        yield return new WaitForSeconds(1);
        addgap = false;

    }

    void OnMouseDown()
    {

        buttonObject.SetActive(true);



        StartCoroutine(closemenu());


    }

    public void destroybuilding()
    {


        PadOn.GetComponent<clickPad0>().builtUpon = false;
        Destroy(gameObject.transform.parent.gameObject);



    }

    IEnumerator closemenu() {

        yield return new WaitForSeconds(3f);
        buttonObject.SetActive(false);




    }





}
