using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int reactorLvl;
    public int Supplies;
    public int MaxPop;
    public int CurrentPop;
    



    void Start()
    {
        MaxPop = 50;
        CurrentPop = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int addsupplies(int suppliesToAdd)
    {

        Supplies = Supplies + suppliesToAdd;


        return 0;
    }
}
