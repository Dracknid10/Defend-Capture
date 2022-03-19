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


    private Transform goal;

    public int selected = 0;


    NavMeshAgent agent;

    public bool resetPriority = false;

    

    void Start()
    {

       

        goal = GameObject.FindGameObjectWithTag("Rallypoint").transform;

        parentarray = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        agent.avoidancePriority = Random.Range(10, 60);

        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }

    void Update()
    {

        if (agent.transform.position == agent.destination)
        {
            agent.avoidancePriority = 80;
            agent.ResetPath();
            resetPriority = false;

        }
        //while (resetPriority)
        //{
        //    StartCoroutine(priorityRandomiser());
        //}

    }

    IEnumerator priorityRandomiser()
    {
        if (resetPriority)
        {
            yield return new WaitForSeconds(3);
            agent.avoidancePriority = Random.Range(10, 60);
            yield return new WaitForSeconds(3);
        }

    }




    void OnMouseDown()
    {

        if (gameObject.tag == "Soilder")
        {

            if (selected % 2 == 0)
            {
                parentarray.addtroop(gameObject);
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

            }

            if (selected % 2 == 1)
            {
                parentarray.removetroop(gameObject);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);

            }


            selected = selected + 1;

        }
 


    }



}
