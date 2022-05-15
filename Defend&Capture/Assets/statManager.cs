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


    public bool playerWon = false;
    public bool AIWon = false;



    public Text UIsupplies;
    public Text UIPopualtion;
    public Text UIReactorLVL;


    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> CaptainSpheres = new List<GameObject>();
    public List<GameObject> PatrolPoints = new List<GameObject>();



    void Start()
    {
        MaxPop = 50;    //to limit the size of an army - saves on computing power
        CurrentPop = 0;         //default starting values
        Supplies = 150;     //supply pads are 150 and are the cheapest building - the player can only build a supply pad first

    }

    // Update is called once per frame
    void Update()
    {

        UIsupplies.text = Supplies.ToString();
        UIReactorLVL.text = reactorLvl.ToString();              // updates the UI with the above values
        UIPopualtion.text = CurrentPop.ToString() + "/50";

        if (CurrentPop < 0)
        {
            CurrentPop = 0;     //error handeling
        }

    }

    









    public int addsupplies(int suppliesToAdd)
    {

        Supplies = Supplies + suppliesToAdd;        //supply pads will call this and add supplies
            

        return 0;
    }

    public void addreactorlvl()
    {

        reactorLvl = reactorLvl + 1;        //reactors call this once - only 1 power is needed to unlock more buildings

    }
}
