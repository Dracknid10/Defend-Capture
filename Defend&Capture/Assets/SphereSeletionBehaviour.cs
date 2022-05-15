using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereSeletionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    // this object has the Selector tag --- looking in the OnclickScript.cs --- this game object is turned off and on and if it collides with a frinedly unit it triggers its select/deslect method


    NavMeshAgent agent;
    public Camera cam;

    private arrayofSelectedTroops parentarray;


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);   //contains collider
        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("left shift"))
        {

            parentarray.clearSelection();   //will reset all units to unselected by removing them from the parent list

        }

        if (Input.GetKey("left shift"))
        {

            


            gameObject.transform.GetChild(0).gameObject.SetActive(true);    //activates collider

            gameObject.SetActive(true);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))      //same idea as the arrayofselectedtroops.cs method - this agent will follow the mouse position exactly due to its speed
            {
                
                agent.nextPosition = hit.point; //moves agent to cursor location on navmesh

            }
        }
        else { gameObject.transform.GetChild(0).gameObject.SetActive(false); } // if its not being pressed the gameobject with the collider isnt active and cannot collide.        

      
    }


}