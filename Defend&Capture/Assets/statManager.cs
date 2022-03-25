using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int reactorLvl = 0;
    public int Supplies;
    public int MaxPop;
    public int CurrentPop;


    
    public Text UIsupplies;
    public Text UIPopualtion;
    public Text UIReactorLVL;


    public List<GameObject> Enemies = new List<GameObject>();


    void Start()
    {
        MaxPop = 50;
        CurrentPop = 0;
        Supplies = 7000;




    }

    // Update is called once per frame
    void Update()
    {

        UIsupplies.text = Supplies.ToString();
        UIReactorLVL.text = reactorLvl.ToString();
        UIPopualtion.text = CurrentPop.ToString() + "/50";


    }


    public int addsupplies(int suppliesToAdd)
    {

        Supplies = Supplies + suppliesToAdd;


        return 0;
    }

    public void addreactorlvl()
    {

        reactorLvl = reactorLvl + 1;

    }
}
