using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BarracksLVL1;
    public GameObject SupplyPadLVL1;



    public string BuildingToBuild;

    [SerializeField] private GameObject SelectedPad;

    private Vector3 spawnPos;

    


    void Start()
    {


        
       



    }

    // Update is called once per frame
    void Update()
    {


      


        
    }



    public GameObject selectedpad(GameObject pad)
    {

        SelectedPad = pad;


        return SelectedPad;
    }

    public string selectedBuilding(string Building)
    {


        BuildingToBuild = Building;




        return BuildingToBuild;
    }


    public void BuildBuilding()
    {



        spawnPos = SelectedPad.transform.GetChild(0).gameObject.transform.position;



        if (BuildingToBuild == "Barracks")
        {

            if (SelectedPad != null)
            {

                GameObject Building = (GameObject)Instantiate(BarracksLVL1);
                Building.transform.position = spawnPos;

            }


        }


        if (BuildingToBuild == "Supply Pad")
        {

            if (SelectedPad != null)
            {

                GameObject Building = (GameObject)Instantiate(SupplyPadLVL1);
                Building.transform.position = spawnPos;

            }


        }



    }


}
