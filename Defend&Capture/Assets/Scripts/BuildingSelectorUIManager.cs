using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class BuildingSelectorUIManager : MonoBehaviour //IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private PadManager pass;

    public GameObject BuildingPadUi;
    public Animator UiAnimation;

    private clickPad0 built;

    public Text desciption;

    [SerializeField] TMPro.TextMeshProUGUI text;

    [SerializeField] UnityEngine.UI.Button Barracks;
    [SerializeField] UnityEngine.UI.Button SupplyPad;
    [SerializeField] UnityEngine.UI.Button Reactor;
    [SerializeField] UnityEngine.UI.Button VD;
    [SerializeField] UnityEngine.UI.Button AP;


    void Start()
    {
        pass = FindObjectOfType<PadManager>();

        UiAnimation = BuildingPadUi.GetComponent<Animator>();

       

        Barracks = GameObject.FindGameObjectWithTag("BarracksButton").GetComponent<UnityEngine.UI.Button>();
        SupplyPad = GameObject.FindGameObjectWithTag("SupplyPadButton").GetComponent<UnityEngine.UI.Button>();
        Reactor = GameObject.FindGameObjectWithTag("ReactorButton").GetComponent<UnityEngine.UI.Button>();
        VD = GameObject.FindGameObjectWithTag("VDbutton").GetComponent<UnityEngine.UI.Button>();
        AP = GameObject.FindGameObjectWithTag("APbutton").GetComponent<UnityEngine.UI.Button>();
    }

    public void barrackstext()
    {

        text.SetText($"Barracks cost x supplies" +
            $" produce infantry troops for x supplies");

    }
    public void Supplytext()
    {

        text.SetText($"SupplyPad.......");

    }
    public void ReactorText()
    {

        text.SetText($"Reactor.......");

    }
    public void VDText()
    {

        text.SetText($"VD.......");

    }
    public void APText()
    {

        text.SetText($"AP.......");

    }


    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (Barracks)
    //    {

    //        text.SetText($"barracks.......");
    //    }

    //    if (SupplyPad)
    //    {

    //        text.SetText($"supplypad.........");
    //    }

    //    if (Reactor)
    //    {

    //        text.SetText($"reactor.....");
    //    }

    //    if (VD)
    //    {

    //        text.SetText($"vehicles........");
    //    }

    //    if (AP)
    //    {

    //        text.SetText($"airpad.........");
    //    }


    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{

    //    Debug.Log(eventData.pointerEnter.transform.gameObject);

    //    if (Barracks)
    //    {

    //       text.SetText($"barracks.......");
    //    }

    //    if (SupplyPad)
    //    {

    //        text.SetText($"supplypad.........");
    //    }

    //    if (Reactor)
    //    {

    //        text.SetText($"reactor.....");
    //    }

    //    if (VD)
    //    {

    //        text.SetText($"vehicles........");
    //    }

    //    if (AP)
    //    {

    //        text.SetText($"airpad.........");
    //    }

    //    Debug.Log(eventData.pointerEnter.tag);
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (btn.name == "GruffysButton")
    //    {
    //        Debug.Log("you have left this button");
    //        text.SetText($"Bloody Marvelous");
    //    }

    //}


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
