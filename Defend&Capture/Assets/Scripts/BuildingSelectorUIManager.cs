using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class BuildingSelectorUIManager : MonoBehaviour //IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private PadManager pass;
    private statManager manager;


    public GameObject BuildingPadUi;
    public Animator UiAnimation;

    [SerializeField] TMPro.TextMeshProUGUI text;

    [SerializeField] UnityEngine.UI.Button Barracks;
    [SerializeField] UnityEngine.UI.Button SupplyPad;
    [SerializeField] UnityEngine.UI.Button Reactor;
    [SerializeField] UnityEngine.UI.Button VD;
    [SerializeField] UnityEngine.UI.Button AP;


    public Text BarrackText;
    public Text SupplyText;
    public Text reactorText;
    public Text VehcileText;
    public Text AirText;

   


    void Start()
    {
        pass = FindObjectOfType<PadManager>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        UiAnimation = BuildingPadUi.GetComponent<Animator>();

       

        Barracks = GameObject.FindGameObjectWithTag("BarracksButton").GetComponent<UnityEngine.UI.Button>();     
        SupplyPad = GameObject.FindGameObjectWithTag("SupplyPadButton").GetComponent<UnityEngine.UI.Button>();
        Reactor = GameObject.FindGameObjectWithTag("ReactorButton").GetComponent<UnityEngine.UI.Button>();
        VD = GameObject.FindGameObjectWithTag("VDbutton").GetComponent<UnityEngine.UI.Button>();
        AP = GameObject.FindGameObjectWithTag("APbutton").GetComponent<UnityEngine.UI.Button>();
    }

    void Update()
    {
        if (manager.Supplies >= 500)
        {
            BarrackText.color = Color.white;
        }
        else
        {
            BarrackText.color = Color.red;
        }

            if (manager.Supplies >= 150)
            {
                SupplyText.color = Color.white;
            }
            else
            {
                SupplyText.color = Color.red;
            }

        if (manager.Supplies >= 1500)
        {
            reactorText.color = Color.white;
        }
        else
        {
            reactorText.color = Color.red;
        }

            if (manager.Supplies >= 700 && manager.reactorLvl >= 1)
            {
                VehcileText.color = Color.white;
            }
            else
            {
                VehcileText.color = Color.red;
            }

        if (manager.Supplies >= 700 && manager.reactorLvl >= 1)
        {
            AirText.color = Color.white;
        }
        else
        {
            AirText.color = Color.red;
        }






    }

  
    public void barrackstext()
    {

        text.SetText($"Barracks cost 500 supplies and" +
            $" produce infantry troops for 300 supplies each.");

    }
    public void Supplytext()
    {

        text.SetText($"Supply pads cost 150 supplies and serve to provide supplies over time");

    }
    public void ReactorText()
    {

        text.SetText($"Reactors cost 1500 supplies and provide power to your base, you'll need one to build a vehicle depot and air pad");

    }
    public void VDText()
    {

        text.SetText($"Vehicle Depots cost 700 supplies and make tanks at 300 supplies each");

    }
    public void APText()
    {

        text.SetText($"Air pads cost 700 supplies and make helicopters at 300 supplies each");

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

        
        UiAnimation.SetBool("Ui On", false);
        UiAnimation.SetBool("Ui Off", true);


    }

   
}
