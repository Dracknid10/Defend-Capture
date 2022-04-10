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


    private Transform goal;

    public int selected = 0;
    public bool SelectedAgent;

    NavMeshAgent agent;

    public bool resetPriority = false;

    

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();


        goal = GameObject.FindGameObjectWithTag("Rallypoint").transform;

        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

        agent = GetComponent<NavMeshAgent>();
        

        agent.avoidancePriority = Random.Range(10, 60);

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        parentarray.AllTroops.Add(gameObject);

        agent.destination = goal.position;

    }

    void Update()
    {

        if (agent.transform.position == agent.destination)
        {
            agent.avoidancePriority = 80;
            agent.ResetPath();
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
        yield return new WaitForSeconds(8);
        resetPriority = true;

    }




    void OnMouseDown()
    {

        SelectOrDeselect();

   



    }

    public void SelectOrDeselect()
    {

        if (gameObject.tag == "Soilder"|| gameObject.tag == "Tank" || gameObject.tag == "Heli")
        {

            if (selected % 2 == 0)
            {
                parentarray.addtroop(gameObject);
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                SelectedAgent = true;

            }

            if (selected % 2 == 1)
            {
                parentarray.removetroop(gameObject);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                SelectedAgent = false;

            }


            selected = selected + 1;

        }



    }

    public void MassClearDeselect()
    {

       
           
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            SelectedAgent = false;
            selected = 0;

        


    }
    


        private void OnTriggerEnter(Collider other)
    {

       

        if (other.tag == "Selector")
        {

            
            SelectOrDeselect();
            

        }
    }

}
