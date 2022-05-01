using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
public class EnemyBehaviour : MonoBehaviour
{

    private float Health;
    public Slider HealthBar;
    

    public statManager manager;

    public GameObject Sphere;
    public CaptainSphereBehaviour SphereManger;


    public arrayofSelectedTroops TargetingManager;


    public List<GameObject> EnemiesInRange = new List<GameObject>();
    public List<GameObject> CloseAllies = new List<GameObject>();
    public List<Vector3> relocatetargets = new List<Vector3>();


    public GameObject closestTarget;
    private float Range = 200;
    private float defaultStoppingDistance = 30f;
    public bool targetLimiter;
    private Vector3 lookingPosition;
    public bool cansee;
    public bool movingInRange;
    private bool reload;
    private bool Asigned;
    public GameObject Bullet;
    public GameObject FirePoint;
    NavMeshAgent agent;

    public GameObject RELOCATE0;
    public GameObject RELOCATE1;
    public GameObject RELOCATE2;
    public GameObject RELOCATE3;

    public Vector3 RELOCATE0trans;
    public Vector3 RELOCATE1trans;
    public Vector3 RELOCATE2trans;
    public Vector3 RELOCATE3trans;

    public GameObject Turret;

    public int number;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        TargetingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();
        
        manager.Enemies.Add(gameObject);

        Asigned = false;
        movingInRange = false;


        Health = 500f;
        HealthBar.maxValue = Health;
        HealthBar.value = Health;


        targetLimiter = true;

        reload = false;

        agent = GetComponent<NavMeshAgent>();


        if (gameObject.tag == "EnemyTank")
        {
            Turret = this.gameObject.transform.GetChild(0).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Asigned == false)
        {
            Asigned = true;
            AssignSpheres();
        }

        if (cansee == false && movingInRange == false)
        {
            agent.ResetPath();
            agent.SetDestination(Sphere.transform.position);
        }
        if (targetLimiter && TargetingManager.AllTroops.Count != 0)
        {


            StartCoroutine(getTargets());
            StartCoroutine(getCloseAllies());


        }
        if (closestTarget != null)
        {

            Vector3 raydirection = closestTarget.transform.position - gameObject.transform.position;

            RaycastHit hitEnemey;

            RaycastHit hitDirection;

            lookingPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);

            Debug.DrawRay(lookingPosition, raydirection, Color.red, 1, true);

            if (Physics.Raycast(lookingPosition, raydirection, out hitEnemey, Range))
            {

                if (hitEnemey.transform.gameObject.tag == "Soilder" || hitEnemey.transform.gameObject.tag == "Tank" || hitEnemey.transform.gameObject.tag == "Heli")
                {

                    agent.ResetPath();


                    if (gameObject.tag == "EnemySoldier" || gameObject.tag == "EnemyHeli")
                    {
                        
                        transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));
                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        FirePoint.transform.rotation = rotation;

                        cansee = true;
                    }

                    

                    if (gameObject.tag == "EnemyTank")
                    {

                        
                        Turret.transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));

                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        FirePoint.transform.rotation = rotation;

                        cansee = true;
                    }
                    

                    if (reload == false)
                    {
                        StartCoroutine(fireBullet());
                    }



                }
                else if (hitEnemey.transform.gameObject.tag != "Soilder" || hitEnemey.transform.gameObject.tag == "Tank" || hitEnemey.transform.gameObject.tag == "Heli") 
                { 
                    
                    cansee = false;
                    agent.stoppingDistance = defaultStoppingDistance;
                    
                }


            }

            

            lookingPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);

            Debug.DrawRay(lookingPosition, gameObject.transform.forward * Range, Color.green, 1, true);

            if (Physics.Raycast(lookingPosition, gameObject.transform.forward, out hitDirection, Range))
            {



                if (hitDirection.transform.gameObject.tag == "EnemySoldier")
                {

                    if (hitDirection.transform.gameObject.GetComponent<EnemyBehaviour>().cansee == true)
                    {
                        rotateAngle();

                    }
                }


                if (hitDirection.transform.gameObject.tag == "EnemyTank")
                {

                    if (hitDirection.transform.gameObject.GetComponent<EnemyBehaviour>().cansee == true)
                    {
                        rotateAngle();

                    }
                }


            }


           


        }
        else { cansee = false; }


    }

 
    private void rotateAngle()
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

        agent.SetDestination(relocatetargets[Random.Range(0, relocatetargets.Count)]);



    }


    IEnumerator getCloseAllies()
    {
        CloseAllies.Clear();

        if (cansee == false)
        {
            for (int i = 0; i < manager.Enemies.Count(); i++)
            {
                float distance = Vector3.Distance(manager.Enemies[i].transform.position, gameObject.transform.position);



                if (distance <= Range)
                {
                    CloseAllies.Add(manager.Enemies[i]);
                }


            }

            for (int i = 0; i < CloseAllies.Count(); i++)
            {
                if (CloseAllies != null)
                {
                    if (CloseAllies[i].GetComponent<EnemyBehaviour>().cansee == true && cansee == false && CloseAllies[i] != null)
                    {
                        if (CloseAllies[i].GetComponent<EnemyBehaviour>().relocatetargets != null)
                        {
                            agent.stoppingDistance = Range;
                            movingInRange = true;
                            agent.ResetPath();
                            agent.SetDestination(CloseAllies[i].GetComponent<EnemyBehaviour>().closestTarget.transform.position);

                            



                            break;
                        }
                        
                        
                        
                    }

                }


            }
        }

        yield return new WaitForSeconds(1f);
    }




        IEnumerator fireBullet()
    {
        reload = true;

        movingInRange = false;

        Instantiate(Bullet, FirePoint.transform.position, FirePoint.transform.rotation);


        yield return new WaitForSeconds(0.5f);

        reload = false;
    }



    IEnumerator getTargets()
    {
        targetLimiter = false;


        EnemiesInRange.Clear();
        closestTarget = null;



        for (int i = 0; i < TargetingManager.AllTroops.Count; i++)
        {
            if (TargetingManager.AllTroops[i] != null)
            {
                float distance = Vector3.Distance(TargetingManager.AllTroops[i].transform.position, gameObject.transform.position);



                if (distance <= Range)
                {
                    EnemiesInRange.Add(TargetingManager.AllTroops[i]);
                }
            }

         


        }

        if (EnemiesInRange.Count != 0)
        {
            GameObject Closest = EnemiesInRange[0];
            float tempdistance = Vector3.Distance(EnemiesInRange[0].transform.position, gameObject.transform.position);


            for (int i = 0; i < EnemiesInRange.Count; i++)
            {

                float distance = Vector3.Distance(EnemiesInRange[i].transform.position, gameObject.transform.position);

                if (tempdistance > distance)
                {
                    Closest = EnemiesInRange[i];
                    tempdistance = distance;
                }



            }

            closestTarget = Closest;


        }

        yield return new WaitForSeconds(2);


        targetLimiter = true;





    }

    private void OnTriggerEnter(Collider other)
        {


        

        if (gameObject.tag == "EnemySoldier")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject);

                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();


                }

                if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 70;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }

            if (gameObject.tag == "EnemyTank")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject);

                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();


                }

                if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 70;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }
            if (gameObject.tag == "EnemyHeli")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject);

                    Health -= 70;
                    HealthBar.value = Health;
                    DeathCheck();


                }

                if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 10;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }

        }




        public void DeathCheck()
        {

            if (Health <= 0)
            {

                manager.Enemies.Remove(gameObject);
                Destroy(gameObject);
            }


        }


        public void AssignSpheres()
        {

            Sphere = manager.CaptainSpheres[Random.Range(0, manager.CaptainSpheres.Count)];

            Sphere.gameObject.GetComponent<CaptainSphereBehaviour>().SubForces.Add(gameObject);

            

        }

    }
