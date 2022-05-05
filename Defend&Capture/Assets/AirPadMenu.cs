using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirPadMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject AirPadsUI;
    public Animator UiAnimation;
    public GameObject PadOn;


    void Start()
    {

        AirPadsUI = GameObject.FindWithTag("AirPadOptions");

        UiAnimation = AirPadsUI.GetComponent<Animator>();

      

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

        AirPad AirPad;
        AirPad = GameObject.FindGameObjectWithTag("AirPadOptions").GetComponent<AirPad>();
        AirPad.PadBelong = PadOn;
        AirPad.Building = gameObject.transform.parent.gameObject;


    }

}
