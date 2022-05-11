using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptainSphereBehaviour : MonoBehaviour
{

    public statManager manager;
    public List<GameObject> SubForces = new List<GameObject>();
    NavMeshAgent agent;
    
    



    void Start()
    {


        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        manager.CaptainSpheres.Add(gameObject);

        agent = GetComponent<NavMeshAgent>();
        GetDestination();


    }

    // Update is called once per frame
    void Update()
    {

        if (agent.hasPath)
        {
            
        }
        else {
            GetDestination();
            
        }
 
    
    }

    public void GetDestination()
    {

        //for (int i = 0; i < SubForces.Count; i++)
        //{
        //    if (SubForces[i] == null)
        //    {

        //        SubForces.Remove(SubForces[i]);

        //    }
        //}


        for (int i = 0; i < SubForces.Count; i++)
        {
            if (SubForces[i] != null)
            {
                if (SubForces[i].GetComponent<EnemyBehaviour>().cansee)
                {
                    agent.SetDestination(SubForces[i].transform.position);
                    break;
                }
                else 
                { 
                    agent.SetDestination(manager.PatrolPoints[Random.Range(0, manager.PatrolPoints.Count)].transform.position); 
                
                }

            }
            else 
            { 
                agent.SetDestination(manager.PatrolPoints[Random.Range(0, manager.PatrolPoints.Count)].transform.position); 
            }
          

        }
       

        


    }



}
