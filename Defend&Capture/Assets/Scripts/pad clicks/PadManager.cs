using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pad0;
    public GameObject pad1;
    public GameObject pad2;
    public GameObject pad3;
    public GameObject pad4;
    public GameObject pad5;
    public GameObject pad6;

    public GameObject Barracks;

    public string BuildingToBuild;

    [SerializeField] private GameObject SelectedPad;

    void Start()
    {
        
       



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




        return Building;
    }

}
