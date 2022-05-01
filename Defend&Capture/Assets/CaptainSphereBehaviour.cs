using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptainSphereBehaviour : MonoBehaviour
{

    public statManager manager;
    public List<GameObject> SubForces = new List<GameObject>();
    NavMeshAgent agent;
    Vector3 finalPosition;
    // Start is called before the first frame update
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

        if (agent.transform.position == finalPosition)
        {
            GetDestination();
            Debug.Log("hit");
        }
 
    
    }

    public void GetDestination()
    {

        Vector3 randomDirection = Random.insideUnitSphere * 100;


        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 100, 1);
        finalPosition = hit.position;
        agent.SetDestination(finalPosition);


    }



}
