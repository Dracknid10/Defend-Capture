using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPad6 : MonoBehaviour
{
    // Start is called before the first frame update

    private PadManager pass;

    public GameObject spawn;

    public GameObject BuildingPadUi;
    public Animator UiAnimation;


    void Start()
    {

        pass = FindObjectOfType<PadManager>();


        UiAnimation = BuildingPadUi.GetComponent<Animator>();


    }

    void OnMouseDown()
    {

        pass.selectedpad(gameObject);


        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);
        UiAnimation.SetBool("Ui On", true);




    }



}
