using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class onclickscript : MonoBehaviour
{
    [SerializeField]
    private arrayofSelectedTroops parentarray;
    GameObject GameManager;


    private statManager manager;


    private Transform goal; //units move to the rally point outside of base - this stores the transform

    public int selected = 0;    //0 is unselcted 1 is selected
    public bool SelectedAgent;  //keeps track if its selected after the fact

    NavMeshAgent agent;

    public bool resetPriority = false;

    

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();


        goal = GameObject.FindGameObjectWithTag("Rallypoint").transform;

        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>(); //when troops spawn theyre added to the AllTroops array

        agent = GetComponent<NavMeshAgent>();
        

        agent.avoidancePriority = Random.Range(10, 60); // avoidance priority is randomised every 8 seconds to avoid having the units 'dance' around getting to the same point

        gameObject.transform.GetChild(0).gameObject.SetActive(false); //this game object is the gold selection circle - it gets set to active and not active when the unit is selected

        

        agent.destination = goal.position; //unit goes towards rally point

    }

    void Update()
    {

        if (agent.transform.position == agent.destination) //agent.haspath
        {
            agent.avoidancePriority = 80;
            agent.ResetPath();                      //if the agent has reached the path the priority get set to lower so other units can reach the destination if they sent to the same point
            resetPriority = false;

        }

        
        if (resetPriority)
        {
            StartCoroutine(priorityRandomiser());
        }

    }

    IEnumerator priorityRandomiser()
    {

        resetPriority = false;
        agent.avoidancePriority = Random.Range(10, 60); 
        //while theyre walking theyre priority gets randomly reset to allow for other units to move in their space to reach thier destination
        //if theyre the same they all end up dancing around the space - depending on the amout of troops the stopping distance may not be enough

        yield return new WaitForSeconds(8);
        resetPriority = true;

    }




    void OnMouseDown()
    {

        SelectOrDeselect(); // clicking on the calls the method

    }

    public void SelectOrDeselect()
    {

        if (gameObject.tag == "Soilder"|| gameObject.tag == "Tank" || gameObject.tag == "Heli")
        {

            if (selected % 2 == 0)
            {
                parentarray.addtroop(gameObject); //adds unit to selected troop list in arrayofSelectedTroops.cs
                gameObject.transform.GetChild(0).gameObject.SetActive(true);        //when the troop is selected  it turns on the circle
                SelectedAgent = true;

            }

            if (selected % 2 == 1)
            {
                parentarray.removetroop(gameObject); //removes from selected troop list in arrayofSelectedTroops.cs
                gameObject.transform.GetChild(0).gameObject.SetActive(false);   //turns of the circle if the number is odd
                SelectedAgent = false;

            }


            selected = selected + 1;    //adds one to the count 

        }



    }

    public void MassClearDeselect()
    {
        
       
           
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            SelectedAgent = false;                                              //this is called from pressing shift to deselect all
            selected = 0;   //set back to 0

        


    }



    private void OnTriggerEnter(Collider other)
    {



        if (other.tag == "Selector")
        {

                
            SelectOrDeselect();     //the mass SHIFT select will trigger the select method - deslects them if theyre selected already


        }
    }

}
