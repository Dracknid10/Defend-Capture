using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickPad0 : MonoBehaviour
{
    // Start is called before the first frame update

    //each pad has a clickpad0 script, there is one PadManager and one BuildingSelectorUIManager
    //clickpad0 && PadManager && BuildingSelectorUIManager are responsable in collecting the necessary infomation to build the right building on the right pad
    //when the pad is clicked
    //-the pad is passed to PadManager when it is clicked
    //-the UI come up with the buttons to choose the buildings comes up
    //-on BuildingSelectorUIManager the button selected sends PadManager the name of the building to build
    //-PadManager has the pad and building name BuildingSelectorUIManager activates the method to build the building - it checks for correct resources and power
    //-PadManager activates the building couroutines here in clickpad0 where it instantiates the building then moves it so both local pivots are the same transform
    //-clickpad0 is now built upon and cannot be built on again



    private PadManager pass; //access to the padmanager scipt (on playerbase gameobject)

    public GameObject BuildingPadUi;
    public Animator UiAnimation;

    public Vector3 spawnPos;
    public GameObject pad;
   
    private statManager manager;


    public GameObject BarracksLVL1;
    public GameObject SupplyPadLVL1;
    public GameObject ReactorLVL1;          //prefabs of the buildings that can be instantiated - lvl 1 is aviable atm
    public GameObject VehicleDepoLVL1;
    public GameObject AirPadLVL1;

    

    public bool builtUpon = false;

    void Start()
    {

        pass = FindObjectOfType<PadManager>(); //pad manager


        UiAnimation = BuildingPadUi.GetComponent<Animator>(); //canvas elements for the building options are located on the buildingpadUI under the MainUi Canvas gameobject since all pads share the same UI

        spawnPos = gameObject.transform.GetChild(0).gameObject.transform.position; // gameobject that represents where the building will spawn
        

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>(); //ultimatly changes suppiles when building is built

      
    }

    void OnMouseDown()  // when player clicks empty pad
    {

        if (UiAnimation.GetBool("Ui On") == false)
        {
            pass.selectedpad(gameObject);       //passes the selected pad to the pad manager for later use in building spawning
        }


        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);   //animator bools are set to true and animation triggers and UI can appear
        UiAnimation.SetBool("Ui On", true);

    }


 //////////////////////Building Spawning/////////////////////
 //the methods are the same with the only the supplies, building and script of the building changing




    public void BuildBarracksNest()
    {

        StartCoroutine(buildBarracks());          //cannot call Couroutines from other scripts - nest is nessesary to call it from Padmanager

    }                                           

  IEnumerator buildBarracks()
   {

        builtUpon = true;       //prevent spad being rebuilt on
        manager.Supplies = manager.Supplies - 500;  //barraks cost 500
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(BarracksLVL1);
        Building.transform.position = spawnPos;                             //moves building to pads spawn position - they connect and give the effect of the base being on top of the pad
        Building.GetComponentInChildren<BarrackMenu>().PadOn = gameObject;  //so the building can be removed in the future the building has a spot to remeber which pad its built on


       
    }



    public void BuildSupplyPadNest()
    {

        StartCoroutine(buildSupplyPad());

    }

    IEnumerator buildSupplyPad()
    {

        builtUpon = true;
        manager.Supplies = manager.Supplies - 150;
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(SupplyPadLVL1);
        Building.transform.position = spawnPos;
        Building.GetComponentInChildren<Supplypad1>().PadOn = gameObject;
        
    }


    public void BuildReactornest()
    {

        StartCoroutine(BuildReactor());

    }

    IEnumerator BuildReactor()
    {

        builtUpon = true;
        manager.Supplies = manager.Supplies - 1500;
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(ReactorLVL1);
        Building.transform.position = spawnPos;
        Building.GetComponentInChildren<reactor1>().PadOn = gameObject;


    }


    public void BuildVechDeponest()
    {

        StartCoroutine(BuildVechDepo());

    }

    IEnumerator BuildVechDepo()
    {

        builtUpon = true;
        manager.Supplies = manager.Supplies - 700;
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(VehicleDepoLVL1);
        Building.transform.position = spawnPos;
        Building.GetComponentInChildren<VehicleMenu>().PadOn = gameObject;

    }


    public void BuildAirPadNest()
    {

        StartCoroutine(BuildAirPad());

    }

    IEnumerator BuildAirPad()
    {

        builtUpon = true;
        manager.Supplies = manager.Supplies - 700;
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(AirPadLVL1);
        Building.transform.position = spawnPos;
        Building.GetComponentInChildren<AirPadMenu>().PadOn = gameObject;

    }


}


