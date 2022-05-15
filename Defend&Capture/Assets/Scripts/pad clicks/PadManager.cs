using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadManager : MonoBehaviour
{
    // Start is called before the first frame update

    //check top of clickpad0 for explaination of this building spawning system
  

    private clickPad0 built;
    
    public string BuildingToBuild; // string received from BuildingSelectorUIManager after the polayer has clicked the building button (supply pad, barracks ect..)

    [SerializeField] private GameObject SelectedPad; //recieves pad game object that was selected by the player

    public Vector3 spawnPos; //gets set to the pads child which has its pivot point set to the middle top of the pad


    private statManager manager; // checks supplies from statmanager

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
    }

    public GameObject selectedpad(GameObject pad)
    {

        SelectedPad = pad;          //pad is sent over from clickpad0 and stored in Selected Pad


        return SelectedPad;
    }

    public string selectedBuilding(string Building)
    {


        BuildingToBuild = Building;         // BuildingSelectorUIManager send over the building name and also triggers the BuildBuilding coroutine
        return BuildingToBuild;
    }

     public void BuildBuilding()
     {

            StartCoroutine(nestBuildBuilding());    //couroutines cannot be called from other scripts but methos can nest them

     }

    IEnumerator nestBuildBuilding()
    {



        spawnPos = SelectedPad.transform.GetChild(0).gameObject.transform.position; //the first child of the pad will be the pivot in the middle

        built = SelectedPad.GetComponent<clickPad0>();  //gets the pads clickpad0 script to activate its method for building the correct building


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
