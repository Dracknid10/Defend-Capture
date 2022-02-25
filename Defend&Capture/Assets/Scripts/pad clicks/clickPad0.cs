using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickPad0 : MonoBehaviour
{
    // Start is called before the first frame update

    private PadManager pass;

    public GameObject BuildingPadUi;
    public Animator UiAnimation;

    public bool builtUpon = false;

    void Start()
    {

        pass = FindObjectOfType<PadManager>();


        UiAnimation = BuildingPadUi.GetComponent<Animator>();


    }

    void OnMouseDown()
    {

        if (UiAnimation.GetBool("Ui On") == false)
        {
            pass.selectedpad(gameObject);
        }


        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);
        UiAnimation.SetBool("Ui On", true);

      

        



    }



}


