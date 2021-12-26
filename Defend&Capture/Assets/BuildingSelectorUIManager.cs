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
        pass.BuildBuilding();

    }

    public void BuildSupplyPad()
    {

        pass.selectedBuilding("Supply Pad");
        pass.BuildBuilding();

    }

    public void BuildReactor()
    {

        pass.selectedBuilding("Reactor");
        pass.BuildBuilding();

    }

    public void BuildVehicleDepo()
    {

        pass.selectedBuilding("Vehicle Depo");
        pass.BuildBuilding();

    }

    public void BuildAirPad()
    {

        pass.selectedBuilding("Air Pad");
        pass.BuildBuilding();
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
