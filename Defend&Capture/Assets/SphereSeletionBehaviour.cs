using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereSeletionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update




    NavMeshAgent agent;
    public Camera cam;

    private arrayofSelectedTroops parentarray;


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("left shift"))
        {

            parentarray.clearSelection();

        }

        if (Input.GetKey("left shift"))
        {

            


            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            gameObject.SetActive(true);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                agent.nextPosition = hit.point;

            }
        }
        else { gameObject.transform.GetChild(0).gameObject.SetActive(false); }
       

      
    }






    }