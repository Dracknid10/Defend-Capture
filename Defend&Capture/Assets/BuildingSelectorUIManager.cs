using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectorUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PadManager pass;

    public GameObject BuildingPadUi;
    public Animator UiAnimation;



    void Start()
    {
        pass = FindObjectOfType<PadManager>();

        UiAnimation = BuildingPadUi.GetComponent<Animator>();
    }


    public void BuildBarracks()
    {

        pass.selectedBuilding("Barracks");

    }

    public void BuildSupplyPad()
    {

        pass.selectedBuilding("Supply Pad");

    }

    public void BuildReactor()
    {

        pass.selectedBuilding("Reactor");

    }

    public void BuildVehicleDepo()
    {

        pass.selectedBuilding("Vehicle Depo");

    }

    public void BuildAirPad()
    {

        pass.selectedBuilding("Air Pad");

    }


    public void RTTB()
    {

        pass.selectedBuilding("");
        pass.selectedpad(null);

        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui On", false);
        UiAnimation.SetBool("Ui Off", true);


    }



}
