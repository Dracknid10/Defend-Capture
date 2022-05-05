using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrackMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BarracksUI;
    public Animator UiAnimation;
    public GameObject PadOn;


    void Start()
    {

        BarracksUI = GameObject.FindWithTag("Barracks Options");

        UiAnimation = BarracksUI.GetComponent<Animator>();

    }

  

    void OnMouseDown()
    {


        UiAnimation.SetBool("Ui true", true);
        UiAnimation.SetBool("Ui Off", false);
        UiAnimation.SetBool("Ui On", true);


        Barracks1 BarracksUI;
        BarracksUI = GameObject.FindGameObjectWithTag("Barracks Options").GetComponent<Barracks1>();
        BarracksUI.PadBelong = PadOn;
        BarracksUI.Building = gameObject.transform.parent.gameObject;


    }

  

  


}
