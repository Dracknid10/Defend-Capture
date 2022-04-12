using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class HelicopterTargeting : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed = 1.0f;

    public List<GameObject> EnemiesInRange = new List<GameObject>();
    public statManager manager;
    public bool targetLimiter;
    public GameObject closestTarget;
    private float Range = 100;
    public GameObject selector;


    public bool cansee;

    public GameObject Bullet;
    public GameObject FirePoint;
    public GameObject FirePoint2;
    private bool reload;





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

          



            Debug.DrawRay(transform.position, raydirection, Color.yellow, 1, true);

            if (Physics.Raycast(transform.position, raydirection, out hitEnemey, Range))
            {

                if (hitEnemey.transform.gameObject.tag == "EnemySoldier")
                {

                    Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                    Vector3 direction2 = hitEnemey.transform.position - FirePoint2.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    Quaternion rotation2 = Quaternion.LookRotation(direction2);

                    FirePoint.transform.rotation = rotation;
                    FirePoint2.transform.rotation = rotation2;


                    cansee = true;

                    if (reload == false)
                    {
                        StartCoroutine(fireBullet());
                    }



                }
                else if (hitEnemey.transform.gameObject.tag != "EnemySoldier") { cansee = false; }


            }






        }

    }


    IEnumerator fireBullet()
    {
        reload = true;


        Instantiate(Bullet, FirePoint.transform.position, FirePoint.transform.rotation);
        Instantiate(Bullet, FirePoint2.transform.position, FirePoint.transform.rotation);


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