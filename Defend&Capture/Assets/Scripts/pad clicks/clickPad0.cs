using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickPad0 : MonoBehaviour
{
    // Start is called before the first frame update

    private PadManager pass;

    public GameObject BuildingPadUi;
    public Animator UiAnimation;
    public Vector3 spawnPos;
    public clickPad0 built;
    private statManager manager;


    public GameObject BarracksLVL1;
    public GameObject SupplyPadLVL1;
    public GameObject ReactorLVL1;
    public GameObject VehicleDepoLVL1;
    public GameObject AirPadLVL1;

    private GameObject currentBuilding;

    public bool builtUpon = false;

    void Start()
    {

        pass = FindObjectOfType<PadManager>();


        UiAnimation = BuildingPadUi.GetComponent<Animator>();

        spawnPos = gameObject.transform.GetChild(0).gameObject.transform.position;


        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

      
    }

    void OnMouseDown()
    {

        if (UiAnimation.GetBool("Ui On") == false)
        {
            pass.selectedpad(gameObject);
        }


        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);
        UiAnimation.SetBool("Ui On", true);

    }



    public void BuildBarracksNest()
    {

        StartCoroutine(buildBarracks());

    }

  IEnumerator buildBarracks()
    {

        builtUpon = true;
        manager.Supplies = manager.Supplies - 500;
        yield return new WaitForSeconds(5);
        GameObject Building = (GameObject)Instantiate(BarracksLVL1);
        Building.transform.position = spawnPos;

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
        currentBuilding = Building;
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
        currentBuilding = Building;
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
        currentBuilding = Building;
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
        currentBuilding = Building;
    }


}


