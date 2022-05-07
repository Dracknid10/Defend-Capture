using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{

    public GameObject Heli;
    public GameObject solider;
    public GameObject Tank;

    public GameObject Spawn;
    public GameObject HeliSpawn;

    public int allySoldiers;
    public int allyTanks;
    public int allyHelis;

    private bool poweredup;
    private bool UnitWait;

    private bool ArmyAnalyseWait = true;

    public float population;
    public arrayofSelectedTroops troopList;

    private bool InitialWaitTime = false;

    void Start()
    {

        troopList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

        population = 0f;
        poweredup = false;
        StartCoroutine(powerup());
        StartCoroutine(powerup());
        UnitWait = true;


       

    }

    // Update is called once per frame
    void Update()
    {

        if (InitialWaitTime == true)
        {
            StartCoroutine(InitialArmy());
        }
        //StartCoroutine(AnalyseArmy());

   


    }
    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(20);
        InitialWaitTime = true;

    }
    IEnumerator InitialArmy()
    {

        for (int i = 0; i < 20; i++)
        {
            StartCoroutine(CreateUnit(solider, Spawn));
            yield return new WaitForSeconds(1);
        }

       
        

    }
    IEnumerator AnalyseArmy()
    {
        if (ArmyAnalyseWait)
        {
            ArmyAnalyseWait = false;

            allySoldiers = 0;
            allyTanks = 0;
            allyHelis = 0;

            for (int i = 0; i < troopList.AllTroops.Count; i++)
            {

                if (troopList.AllTroops[i] != null)
                {

                    if (troopList.AllTroops[i].gameObject.tag == "Soilder")
                    {
                        allySoldiers++;
                    }
                    if (troopList.AllTroops[i].gameObject.tag == "Tank")
                    {
                        allyTanks++;
                    }
                    if (troopList.AllTroops[i].gameObject.tag == "Heli")
                    {
                        allyHelis++;
                    }




                }


            }
        }



        yield return new WaitForSeconds(10);
        ArmyAnalyseWait = true;
    }

    IEnumerator CreateUnit( GameObject type, GameObject spawn)
    {
        UnitWait = false;


       Instantiate(type, spawn.transform.position, Quaternion.identity);
        population = population + 1;

        yield return new WaitForSeconds(2);
        UnitWait = true;

    }

    IEnumerator powerup()
    {
        yield return new WaitForSeconds(180);
        poweredup = true;

    }

}
