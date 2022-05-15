using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class arrayofSelectedTroops : MonoBehaviour
{
    // Start is called before the first frame update


    public List<GameObject> SelectedTroops = new List<GameObject>();    //selected troops get added to this list when theyre clicked on in onclickScript.cs
    public List<GameObject> AllTroops = new List<GameObject>(); //troops as they spawn get added to this list
    

    [SerializeField]
    NavMeshAgent agent;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
        
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);    //store ray coming from camera to the mouse position
            RaycastHit hit;     //store what raycasts hit

            if (Physics.Raycast(ray, out hit)) //if the ray hits somthing
            {

                for (int i = 0; i < SelectedTroops.Count; i++)      //iterate through the selected troops and give the new destination of the mouse coords
                {
                  
                    agent = SelectedTroops[i].GetComponent<NavMeshAgent>();

                    agent.SetDestination(hit.point);    //gets the list[i] agent and set thier destination as the mouse click coord

                    SelectedTroops[i].GetComponent<onclickscript>().resetPriority = true; // this will start randomising unit priority so they dont dance around getting to a point

                }
            }

        }

    }


    public GameObject addtroop(GameObject trooptoadd)
    {
        
            SelectedTroops.Add(trooptoadd); //when a troop is selected using in onclickscript, the game object is sent over and added to the list

        return trooptoadd;

    }

    public GameObject removetroop(GameObject trooptoRemove)
    {
  
            SelectedTroops.Remove(trooptoRemove);   //the game object is removed from the list
    

        return trooptoRemove;

    }






    public void clearSelection()
    {
        //iterating through a for loop and using the above removetroop method meant changing the list as it was iterating through
        //to avoid errors the method resets the values of the unit to an unslected state then clears the list of selected troops

        if (SelectedTroops.Count != 0)
        {
            for (int i = 0; i < SelectedTroops.Count; i++)
            {
                SelectedTroops[i].GetComponent<onclickscript>().MassClearDeselect(); //MassClearDeselect resets the units selected status so the select/deselect isnt thrown of sequence

            }
            SelectedTroops.Clear(); //wipes list making it deslect all units
          
        }
    }

}
