using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class arrayofSelectedTroops : MonoBehaviour
{
    // Start is called before the first frame update


    public List<GameObject> SelectedTroops = new List<GameObject>();
    public List<GameObject> AllTroops = new List<GameObject>();

    [SerializeField]
    NavMeshAgent agent;
    public Camera cam;

    void Start()
    {
       

    }
    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {

           

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                for (int i = 0; i < SelectedTroops.Count; i++)
                {
                   

                    agent = SelectedTroops[i].GetComponent<NavMeshAgent>();

                    agent.SetDestination(hit.point);

                    SelectedTroops[i].GetComponent<onclickscript>().resetPriority = true;



                }
            }

        }




    }


    public GameObject addtroop(GameObject trooptoadd)
    {
        
            SelectedTroops.Add(trooptoadd);

        return trooptoadd;

    }

    public GameObject removetroop(GameObject trooptoRemove)
    {
  
            SelectedTroops.Remove(trooptoRemove);
    

        return trooptoRemove;

    }






    public void clearSelection()
    {


        if (SelectedTroops.Count != 0)
        {

           


            for (int i = 0; i < SelectedTroops.Count; i++)
            {

                SelectedTroops[i].GetComponent<onclickscript>().MassClearDeselect();
                

            }

            SelectedTroops.Clear();
          
        }
    }

      



}
