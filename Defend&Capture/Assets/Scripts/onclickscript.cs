using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclickscript : MonoBehaviour
{
    [SerializeField]
    private arrayofSelectedTroops parentarray;
    GameObject GameManager;

    public int selected = 2;


    void Start()
    {

        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

    }

    void OnMouseDown()
    {

        if (gameObject.tag == "Soilder")
        {

            if (selected % 2 == 0)
            {
                parentarray.addtroop(gameObject);
               
                
            }

            if (selected % 2 == 1)
            {
                parentarray.removetroop(gameObject);
                
               
            }


            selected = selected + 1;

        }



    }



}
