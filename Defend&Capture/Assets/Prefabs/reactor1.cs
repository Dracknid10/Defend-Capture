using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactor1 : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject PadOn;
    public GameObject buttonObject;

    private statManager manager;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        manager.addreactorlvl();
        buttonObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

        buttonObject.SetActive(true);



        StartCoroutine(closemenu());


    }

    public void destroybuilding()
    {

        manager.reactorLvl = manager.reactorLvl - 1;
        PadOn.GetComponent<clickPad0>().builtUpon = false;
        Destroy(gameObject.transform.parent.gameObject);



    }

    IEnumerator closemenu()
    {

        yield return new WaitForSeconds(3f);
        buttonObject.SetActive(false);




    }

}
