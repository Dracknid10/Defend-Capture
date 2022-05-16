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

    public List<GameObject> EnemiesInRange = new List<GameObject>();        // local list of close targets in range
    
    public bool targetLimiter;      //for performance
    public GameObject closestTarget;   
    private float Range = 150;
    public GameObject selector; 

    public GameObject RELOCATE0;
    public GameObject RELOCATE1;
    public GameObject RELOCATE2;                //gamobjects  ofr relocating around allies  --- thier vector3s change and are re calculated
    public GameObject RELOCATE3;

    public Vector3 RELOCATE0trans;
    public Vector3 RELOCATE1trans;
    public Vector3 RELOCATE2trans;
    public Vector3 RELOCATE3trans;

    private Vector3 lookingPosition;    //ray cast origin for looking at closest enemy

    public bool cansee = false; //does the unit have eye on enemy
    public bool rotateWait; //for performance

    public GameObject Bullet;
    public GameObject FirePoint;    
    private bool reload;

    public GameObject Turret;   //tanks shoot from turrets - the turret faces the enemy not the tank body

    public List<Vector3> relocatetargets = new List<Vector3>(); //stores list of vectors from the relocate targets


    NavMeshAgent agent;

    public statManager manager;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        targetLimiter = true;

        agent = GetComponent<NavMeshAgent>();

        selector = GameObject.FindGameObjectWithTag("Selector");

        reload = false;

        rotateWait = true;

        if (gameObject.tag == "Tank")
        {
            Turret = this.gameObject.transform.GetChild(1).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {


        

        if (targetLimiter && manager.Enemies.Count() != 0)
        {


            StartCoroutine(getTargets());   //if theres targets to get itll get targets



        }
        if (closestTarget != null)  //if there is a target in range - in the statement the unit faces them - if theres ally in the way itll rotate angles otheriwse itll start fireing
        {                               //this works simularly to the Enemey behaviour script

            Vector3 raydirection = closestTarget.transform.position - gameObject.transform.position;

            

            RaycastHit hitEnemey;

            RaycastHit hitDirection;

            lookingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Debug.DrawRay(lookingPosition, raydirection, Color.yellow, 1, true);

            if (Physics.Raycast(lookingPosition, raydirection, out hitEnemey, Range))
            {
                
             
                    
                    if (gameObject.tag == "Soilder" || gameObject.tag == "Heli")
                    {

                        transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));
                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        FirePoint.transform.rotation = rotation;

                        
                    }
                    if (gameObject.tag == "Tank")
                    {

                        Turret.transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));
                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        FirePoint.transform.rotation = rotation;

                    

                        if (hitEnemey.transform.gameObject.tag == "Tank" || hitEnemey.transform.gameObject.tag == "Soilder")
                        {

                            if (cansee == false)        //if the unit cannot see an enemy
                            {

                                if (hitEnemey.transform.gameObject.GetComponent<Targeting>().cansee == true) //if the unit infront can see and there is a close target
                                {

                                    StartCoroutine(rotateAngle());      //rotate to get a better angle

                                }
                            }
                            

                        }


                    }               

                    if (hitEnemey.transform.gameObject.tag == "EnemySoldier"||
                        hitEnemey.transform.gameObject.tag == "EnemyTank"||
                        hitEnemey.transform.gameObject.tag == "EnemyHeli"||
                        hitEnemey.transform.gameObject.tag == "EnemyBase")      //enemy tags
                    { 

                        cansee = true;  //if the raycasts hits it means theres no obsticle or allies in the way
                        

                    }         
                    else 
                    {
                   
                        cansee = false;     //if it hits somthng without these tags - it cant see it becuase somthing is in the way
                            
                    }

                    if (reload == false && cansee == true)
                    {

                        StartCoroutine(fireBullet());

                    }


            }



            

            lookingPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);

            Debug.DrawRay(lookingPosition, gameObject.transform.forward*Range, Color.green, 1, true);

            if (Physics.Raycast(lookingPosition, gameObject.transform.forward, out hitDirection, Range))
            {

                if (hitDirection.transform.gameObject.tag == "Heli")
                {


                    if (hitDirection.transform.gameObject.GetComponent<Targeting>().cansee == true)
                    {

                        StartCoroutine(rotateAngle());

                    }
                }
                if (hitDirection.transform.gameObject.tag == "Soilder")
                {
                        

                        if (hitDirection.transform.gameObject.GetComponent<Targeting>().cansee == true)
                        {
                        
                                StartCoroutine(rotateAngle());

                        }
                }

            }


          



        }

    }

    IEnumerator rotateAngle()
    {
        if (rotateWait)
        {
            rotateWait = false;
            

            relocatetargets.Clear();    //clears list of points

            RELOCATE0trans = RELOCATE0.transform.position;
            RELOCATE1trans = RELOCATE1.transform.position;
            RELOCATE2trans = RELOCATE2.transform.position;
            RELOCATE3trans = RELOCATE3.transform.position;      //gets new points and vectors as the unit mayve moved and the transforms need to be updated

            relocatetargets.Add(RELOCATE0trans);
            relocatetargets.Add(RELOCATE1trans);
            relocatetargets.Add(RELOCATE2trans);    //adds them to a list so they can be randomly selected
            relocatetargets.Add(RELOCATE3trans);

            agent.stoppingDistance = 0;     //becuase the distances are close highter values made it so the unit didnt move
            agent.SetDestination(relocatetargets[Random.Range(0, 4)]); //chooses a random spot to send unit to out of the relocat targets

            yield return new WaitForSeconds(5f);
            agent.stoppingDistance = 30;        //replaced stopping distance after 5 seconds
            rotateWait = true;              //can cycle back round and reactivate if they still cannot see
        }
     
    }

    IEnumerator fireBullet()
    {
        reload = true;


        Instantiate (Bullet, FirePoint.transform.position, FirePoint.transform.rotation);


        yield return new WaitForSeconds(0.5f);

        reload = false;
    }





    IEnumerator getTargets()
    {
            targetLimiter = false;  //prevent this happening every update

            EnemiesInRange.Clear();     //clears list to prevent nulls and missing obejcts being counted
            closestTarget = null;


            for (int i = 0; i < manager.Enemies.Count(); i++)
            {
                float distance = Vector3.Distance(manager.Enemies[i].transform.position, gameObject.transform.position);

           

                if (distance <= Range)                                      //sorting alogrithm compares distances and save the shortest one and saves this - produces new bench marks if the previous is beaten
                {
                    EnemiesInRange.Add(manager.Enemies[i]);
                }


            }

            if (EnemiesInRange.Count() != 0)
            {
                GameObject Closest = EnemiesInRange[0];
                float tempdistance = Vector3.Distance(EnemiesInRange[0].transform.position, gameObject.transform.position);


                for (int i = 0; i < EnemiesInRange.Count(); i++)
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


}
