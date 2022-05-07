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

    public int PlayerSoldiers;
    public int PlayerTanks;
    public int PlayerHelis;

    public int Soldiers;
    public int Tanks;
    public int Helis;

    private bool poweredup;
    private bool UnitWait;

    private bool ArmyAnalyseWait = true;


    private bool SpawnTime = true;
    public float population;
    public arrayofSelectedTroops troopList;

    private bool InitialWaitTime = true;

    void Start()
    {

        troopList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

        population = 0f;
        poweredup = false;
        StartCoroutine(powerup());
        StartCoroutine(powerup());
        StartCoroutine(StartArmy());
        UnitWait = true;


       

    }

    // Update is called once per frame
    void Update()
    {
        if (InitialWaitTime == false)
        {
            StartCoroutine(AnalyseArmy());
            StartCoroutine(AiCounter());
        }






    }


    IEnumerator StartArmy()
    {
        if (InitialWaitTime == true)
        {

            yield return new WaitForSeconds(50);


            for (int i = 0; i < 20; i++)
            {
                StartCoroutine(CreateUnit(solider, Spawn));
               
                yield return new WaitForSeconds(2);
            }

            InitialWaitTime = false;
        }
       

        
    }

    IEnumerator AiCounter()
    {
        if (SpawnTime)
        {
            SpawnTime = false;
            int biggest = Mathf.Max(Mathf.Max(PlayerSoldiers, PlayerHelis), PlayerTanks);

            if (PlayerSoldiers == biggest)
            {
                StartCoroutine(CreateUnit(Tank, Spawn));
            }
            if (PlayerHelis == biggest)
            {
                StartCoroutine(CreateUnit(solider, Spawn));
            }
            if (PlayerTanks == biggest)
            {
                StartCoroutine(CreateUnit(Heli, HeliSpawn));
            }

            yield return new WaitForSeconds(2);
            SpawnTime = true;

        }




       
    }


    IEnumerator AnalyseArmy()
    {
        if (ArmyAnalyseWait)
        {
            ArmyAnalyseWait = false;

            PlayerSoldiers = 0;
            PlayerTanks = 0;
            PlayerHelis = 0;

            for (int i = 0; i < troopList.AllTroops.Count; i++)
            {

                if (troopList.AllTroops[i] != null)
                {

                    if (troopList.AllTroops[i].gameObject.tag == "Soilder")
                    {
                        PlayerSoldiers++;
                    }
                    if (troopList.AllTroops[i].gameObject.tag == "Tank")
                    {
                        PlayerTanks++;
                    }
                    if (troopList.AllTroops[i].gameObject.tag == "Heli")
                    {
                        PlayerHelis++;
                    }




                }


            }
        }



        yield return new WaitForSeconds(10);
        ArmyAnalyseWait = true;
    }

    IEnumerator CreateUnit( GameObject type, GameObject spawn)
    {
        

        if (population <= 50)
        {
            UnitWait = false;
            Instantiate(type, spawn.transform.position, Quaternion.identity);
            population++;

            if (type.gameObject.tag == "EnemySoldier")
            {
                Soldiers++;
            }
            if (type.gameObject.tag == "EnemyTank")
            {
                Tanks++;
            }
            if (type.gameObject.tag == "EnemyHeli")
            {
                Helis++;
            }

            yield return new WaitForSeconds(4);
            UnitWait = true;
        }

    

    }

    IEnumerator powerup()
    {
        yield return new WaitForSeconds(180);
        poweredup = true;

    }

}
