using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject BarracksLVL1;
    //public GameObject SupplyPadLVL1;
    //public GameObject ReactorLVL1;
    //public GameObject VehicleDepoLVL1;
    //public GameObject AirPadLVL1;

    private clickPad0 built;
    


    public string BuildingToBuild;

    [SerializeField] private GameObject SelectedPad;

    public Vector3 spawnPos;


    private statManager manager;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
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

            StartCoroutine(nestBuildBuilding());

     }

    IEnumerator nestBuildBuilding()
    {



        spawnPos = SelectedPad.transform.GetChild(0).gameObject.transform.position;

        built = SelectedPad.GetComponent<clickPad0>();


        if (built.builtUpon == false)
        {

            if (manager.Supplies >= 500)
            {

                if (BuildingToBuild == "Barracks")
                {

                    if (SelectedPad != null)
                    {

                        built.BuildBarracksNest();

                    }

                }
            }

            if (manager.Supplies >= 150)
            {

                if (BuildingToBuild == "Supply Pad")
                {

                    if (SelectedPad != null)
                    {

                        built.BuildSupplyPadNest();

                    }


                }

            }

            if (manager.Supplies >= 1500)
            {

                if (BuildingToBuild == "Reactor")
                {

                    if (SelectedPad != null)
                    {

                        built.BuildReactornest();

                    }
                }
            }

            if (manager.reactorLvl >= 1)
            {

                if (manager.Supplies >= 700)
                {


                    if (BuildingToBuild == "Vehicle Depo")
                    {

                        if (SelectedPad != null)
                        {

                            built.BuildVechDeponest();


                        }


                    }

                    if (BuildingToBuild == "Air Pad")
                    {
                        if (SelectedPad != null)
                        {
                           built.BuildAirPadNest();

                        }

                    }

                }
    
            }
        }

        yield return new WaitForSeconds(0);

    }




}
