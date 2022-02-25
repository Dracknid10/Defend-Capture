using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BarracksLVL1;
    public GameObject SupplyPadLVL1;
    public GameObject ReactorLVL1;
    public GameObject VehicleDepoLVL1;
    public GameObject AirPadLVL1;

    private clickPad0 built;
    


    public string BuildingToBuild;

    [SerializeField] private GameObject SelectedPad;

    private Vector3 spawnPos;

    


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

        StartCoroutine(nestBuildBuilding());

    }

    IEnumerator nestBuildBuilding()
    {



        spawnPos = SelectedPad.transform.GetChild(0).gameObject.transform.position;

        built = SelectedPad.GetComponent<clickPad0>();


        if (built.builtUpon == false)
        {




            if (BuildingToBuild == "Barracks")
            {

                if (SelectedPad != null)
                {

                    built.builtUpon = true;
                    yield return new WaitForSeconds(5);
                    GameObject Building = (GameObject)Instantiate(BarracksLVL1);
                    Building.transform.position = spawnPos;

                   

                }


            }


            if (BuildingToBuild == "Supply Pad")
            {

                if (SelectedPad != null)
                {

                    built.builtUpon = true;
                    yield return new WaitForSeconds(5);
                    GameObject Building = (GameObject)Instantiate(SupplyPadLVL1);
                    Building.transform.position = spawnPos;

                    

                }


            }


            if (BuildingToBuild == "Reactor")
            {

                if (SelectedPad != null)
                {

                    built.builtUpon = true;
                    yield return new WaitForSeconds(5);
                    GameObject Building = (GameObject)Instantiate(ReactorLVL1);
                    Building.transform.position = spawnPos;

                  

                }


            }

            if (BuildingToBuild == "Vehicle Depo")
            {

                if (SelectedPad != null)
                {

                    built.builtUpon = true;
                    yield return new WaitForSeconds(5);
                    GameObject Building = (GameObject)Instantiate(VehicleDepoLVL1);
                    Building.transform.position = spawnPos;


                }


            }

            if (BuildingToBuild == "Air Pad")
            {

                if (SelectedPad != null)
                {

                    built.builtUpon = true;
                    yield return new WaitForSeconds(5);
                    GameObject Building = (GameObject)Instantiate(AirPadLVL1);
                    Building.transform.position = spawnPos;

              

                }


            }
        }

        yield return new WaitForSeconds(0);

    }


}
