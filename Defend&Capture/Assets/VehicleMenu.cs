using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VehicleMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject VehicleUi;
    public Animator UiAnimation;
    public GameObject PadOn;


    void Start()
    {

        VehicleUi = GameObject.FindWithTag("VehicleUiOptions");

        UiAnimation = VehicleUi.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnMouseDown()
    {
       

        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);
        UiAnimation.SetBool("Ui On", true);

        VehicleDepot VehicleDepot;
        VehicleDepot = GameObject.FindGameObjectWithTag("VehicleUiOptions").GetComponent<VehicleDepot>();
        VehicleDepot.PadBelong = PadOn;
        VehicleDepot.Building = gameObject.transform.parent.gameObject;

    }




}