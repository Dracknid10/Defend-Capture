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


    private bool poweredup;
    private bool SoilderWait;

    private bool TroopwaitWait;
    private bool HeliWait;
    private bool TankWait;

    private bool population;

    void Start()
    {
        poweredup = false;
        SoilderWait = false;
        StartCoroutine(powerup());
        StartCoroutine(troopWait());
        SoilderWait = false;
        HeliWait = false;
        TankWait = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (SoilderWait)
        {







        }


    }
    IEnumerator troopWait()
    {
        yield return new WaitForSeconds(30);
        SoilderWait = true;

    }

    IEnumerator powerup()
    {
        yield return new WaitForSeconds(180);
        poweredup = true;

    }
    IEnumerator CreateSolider()
    {



        yield return new WaitForSeconds(5);
        SoilderWait = true;

    }
    IEnumerator CreateHeli()
    {


        yield return new WaitForSeconds(5);
        HeliWait = true;

    }
    IEnumerator CreateTank()
    {


        yield return new WaitForSeconds(5);
        TankWait = true;

    }
}
