using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barracks1 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BarracksUI;
    public Animator UiAnimation;

    public GameObject Soilder;
    public Vector3 spawnLoc;

    private statManager manager;

    void Start()
    {

        BarracksUI = GameObject.FindWithTag("Barracks Options");

        UiAnimation = BarracksUI.GetComponent<Animator>();

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        spawnLoc = GameObject.FindWithTag("SpawnPoint").transform.position;

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

    }

    public void closemenu()
    {
    

        UiAnimation.SetBool("Ui Off", true);
        UiAnimation.SetBool("Ui On", false);


    }


    public void CreateTroop()
    {
        if (manager.Supplies >= 300 && manager.CurrentPop <= 50)
        {

            GameObject newSoilder = (GameObject)Instantiate(Soilder);
            newSoilder.transform.position = spawnLoc;
            manager.Supplies = manager.Supplies - 300;
            manager.CurrentPop = manager.CurrentPop + 1;

        }


    }



}
