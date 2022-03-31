using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Targeting : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed = 1.0f;

    public List<GameObject> EnemiesInRange = new List<GameObject>();
    public statManager manager;
    public bool targetLimiter;
    public GameObject closestTarget;
    private float Range = 100;
    public GameObject selector;

    public GameObject RELOCATE0;
    public GameObject RELOCATE1;
    public GameObject RELOCATE2;
    public GameObject RELOCATE3;

    public Vector3 RELOCATE0trans;
    public Vector3 RELOCATE1trans;
    public Vector3 RELOCATE2trans;
    public Vector3 RELOCATE3trans;

    public bool cansee;

    public GameObject Bullet;
    public GameObject FirePoint;
    private bool reload;
    

    public List<Vector3> relocatetargets = new List<Vector3>();


    NavMeshAgent agent;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        targetLimiter = true;

        agent = GetComponent<NavMeshAgent>();

        selector = GameObject.FindGameObjectWithTag("Selector");

        reload = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (targetLimiter && manager.Enemies.Count() != 0)
        {


            StartCoroutine(getTargets());



        }
        if (closestTarget != null)
        {

            Vector3 raydirection = closestTarget.transform.position - gameObject.transform.position;



            RaycastHit hitEnemey;

            RaycastHit hitDirection;



            Debug.DrawRay(transform.position, raydirection, Color.yellow, 1, true);

            if (Physics.Raycast(transform.position, raydirection, out hitEnemey, Range))
            {
                
                if (hitEnemey.transform.gameObject.tag == "EnemySoldier")
                {
                    
                    transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));
                    cansee = true;

                    if (reload == false)
                    {
                        StartCoroutine(fireBullet());
                    }
                    
                  

                }
                else if (hitEnemey.transform.gameObject.tag != "EnemySoldier") { cansee = false; }


            }







            Debug.DrawRay(transform.position, gameObject.transform.forward*Range, Color.green, 1, true);

            if (Physics.Raycast(transform.position, gameObject.transform.forward, out hitDirection, Range))
            {


                if (hitDirection.transform.gameObject.tag == "Soilder")
                {
                    if (hitDirection.transform.gameObject.GetComponent<Targeting>().cansee == true)
                    {
                        relocatetargets.Clear();

                        RELOCATE0trans = RELOCATE0.transform.position;
                        RELOCATE1trans = RELOCATE1.transform.position;
                        RELOCATE2trans = RELOCATE2.transform.position;
                        RELOCATE3trans = RELOCATE3.transform.position;

                        relocatetargets.Add(RELOCATE0trans);
                        relocatetargets.Add(RELOCATE1trans);
                        relocatetargets.Add(RELOCATE2trans);
                        relocatetargets.Add(RELOCATE3trans);

                        agent.SetDestination(relocatetargets[Random.Range(0, 4)]);

                    }




                }


            }


          



        }

    }


    IEnumerator fireBullet()
    {
        reload = true;


        Instantiate (Bullet, FirePoint.transform.position, FirePoint.transform.rotation);


        yield return new WaitForSeconds(1);

        reload = false;
    }





        IEnumerator getTargets()
    {
        targetLimiter = false;

        EnemiesInRange.Clear();
        closestTarget = null;


        for (int i = 0; i < manager.Enemies.Count(); i++)
        {
            float distance = Vector3.Distance(manager.Enemies[i].transform.position, gameObject.transform.position);

           

            if (distance <= Range)
            {
                EnemiesInRange.Add(manager.Enemies[i]);
            }


        }

        if (EnemiesInRange.Count() != 0)
        {
            GameObject Closest = EnemiesInRange[0];
            float tempdistance = Vector3.Distance(manager.Enemies[0].transform.position, gameObject.transform.position);


            for (int i = 0; i < EnemiesInRange.Count(); i++)
            {

                float distance = Vector3.Distance(manager.Enemies[i].transform.position, gameObject.transform.position);

                if (tempdistance > distance)
                {
                    Closest = manager.Enemies[i];
                    tempdistance = distance;
                }



            }

            closestTarget = Closest;
            
        }

       

        yield return new WaitForSeconds(2);

       
        targetLimiter = true;

    }


}
