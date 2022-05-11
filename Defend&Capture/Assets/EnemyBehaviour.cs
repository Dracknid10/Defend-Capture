using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
public class EnemyBehaviour : MonoBehaviour
{
    public ParticleSystem Sparks;


    //health bar variables
    private float Health;
    public Slider HealthBar;
    

    public statManager manager; // gives access to parent statmanager script - the statmanager deals with the AI unit lists
    public GameOver gameover;  // gives access to parent game over script
    public GameObject Sphere; //units are given a seperate objects to follow when not in combat - it is assigned to this variable
    public CaptainSphereBehaviour SphereManger; //spheres are randomly allocated and have there own script and own behaviours

    public arrayofSelectedTroops TargetingManager; //gives access to the arrayofselcted troops script - this has infomation on lists of player controlled troops 
    public EnemySpawningScript spawner; //the spawner needs to know if this unit dies - the population count for the AI team is managed here

    public List<GameObject> EnemiesInRange = new List<GameObject>();    //sorts through player units and get units in range that can be targeted
    public List<GameObject> CloseAllies = new List<GameObject>();   //get allies in range to see if any of those guys 
    public List<Vector3> relocatetargets = new List<Vector3>();


    public GameObject closestTarget; //closest viable enemy unit is stored here
    private float Range = 150; //declares maximum range that units can "see"
    private float defaultStoppingDistance = 30f; //effects navmesh agents stopping distance
    public bool targetLimiter; //prevents getting the sloestest target every update
    public bool repathingLimiter; // allows sphere to get ahead by forcing couroutine to wait
    private Vector3 lookingPosition; //where rays will start at
    public bool cansee; //if the unit can see an enemy this will be true other false
    public bool movingInRange; //units will try to get closer if they see a friend who sees an enemy - is theyre moving it ignores new destintaitons from the spheres
    private bool reload; //revents spamming of projectiles
    private bool Asigned; //bool for assigning a sphere - cannot be in start becuase both the sphere and unit start functons have to be there 
    public GameObject Bullet; //stores projectile type
    public GameObject FirePoint; //bullets fire from this location
    NavMeshAgent agent; //the agent component on the gameobject
    public bool rotateWait; //so they have a chance to rotate to the spot other it'll trigger again and they might get stuck or worse...... wiggle


    ///////////variables for relocating unit around///////////////////////
  
    public GameObject RELOCATE0;
    public GameObject RELOCATE1;
    public GameObject RELOCATE2;        //stores the rotate objects - theyre located ajacent children to the unit and the unit brings them with it - if theres a unit in front of them that cansee the enemy will attempt to move to get a better angle
    public GameObject RELOCATE3;

    public Vector3 RELOCATE0trans;
    public Vector3 RELOCATE1trans;
    public Vector3 RELOCATE2trans;      //stores location of these rotate objects
    public Vector3 RELOCATE3trans;

    ////////////////////////////////////////////

    public GameObject Turret;   //tanks have turrets they shoot from


    // Start is called before the first frame update
    void Start()
    {

        Sparks.Stop(); //for signifing health


        ////////////////////other script communication///////////
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>(); // one game manager - that script is editied by these child scripts
        TargetingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>(); // this is a child of arrayfselectedtroops - it has all the current player controlled troops in it
        gameover = GameObject.FindGameObjectWithTag("gameOverManager").GetComponent<GameOver>(); //game over script access    
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawningScript>(); //spawner script access
        ////////////////////////////////////////////////////////



        manager.Enemies.Add(gameObject); // adds troop to all enemy troops list

        ///////////couroutine limiters or stoppers - forces it to wait again before activating again/////////////
        Asigned = false;
        movingInRange = false;
        rotateWait = true;
        targetLimiter = true;
        repathingLimiter = true;
        reload = false;
        //////////////////////////////////

        /////////////health values/////////
        Health = 500f;
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
        //////////////////////////////////

        agent = GetComponent<NavMeshAgent>(); //grabs agent component from gameobject


        if (gameObject.tag == "EnemyTank")
        {
            Turret = this.gameObject.transform.GetChild(0).gameObject;     //sets turret value if the gamobject is a tank - otherwise its left empty
        }

    }

    // Update is called once per frame
    void Update()
    {
    

       

        if (Asigned == false)
        {
            Asigned = true;     //unit is asigned to a sphere only once
            AssignSpheres();
        }
    

        if (cansee == false && movingInRange == false && targetLimiter == true && closestTarget == null)
        {

            StartCoroutine(getIdleDetination());    //it will get a desination of its captain sphere if its not moving into range of a player troop and it cant currently see one and it hasnt reccently done it and it has no targets in range

        }
        if (targetLimiter && TargetingManager.AllTroops.Count != 0)
        {


            StartCoroutine(getTargets());       //if it hasnt done these reccently and the current player troop count = 0 itll get targets in range
            StartCoroutine(getCloseAllies());   //gets close allies


        }
        if (closestTarget != null)
        {

          

            Vector3 raydirection = closestTarget.transform.position - gameObject.transform.position; //get rotation towards closest target

            RaycastHit hitEnemey; // this will attempt to go towards the closest enemy - what it hit
                                            
            RaycastHit hitDirection; //this goes in the direction the unity is facing and analyses what theyre looking at

            lookingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); //gets game objects position

            Debug.DrawRay(lookingPosition, raydirection, Color.red, 1, true); // draws ray from unit to closest target

            if (Physics.Raycast(lookingPosition, raydirection, out hitEnemey, Range)) //shoots ray towards closest target - return true if hit somthing
            {

                    if (gameObject.tag == "EnemySoldier" || gameObject.tag == "EnemyHeli")
                    {
                        
                        transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z)); //makes unit look in enemies direction
                                                                                                                                                     
                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position; //gets direction to look in
                        Quaternion rotation = Quaternion.LookRotation(direction); //get rotational value 
                        FirePoint.transform.rotation = rotation; //apllies Quaternion to transform to make it look at the closest enemy
                            
                            //depending on what unit is this attached to,  it will turn the firepoint to look at the closest enemy by storing the relative position of the target and 
                        
                    }

                    

                    if (gameObject.tag == "EnemyTank")
                    {

                        // this works the same as above but asigned the turret to look at the enemy instead of the entire object
                        Turret.transform.LookAt(new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z));
                        Vector3 direction = hitEnemey.transform.position - FirePoint.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        FirePoint.transform.rotation = rotation;


                        //if (hitEnemey.transform.gameObject.tag == "EnemyTank" || hitEnemey.transform.gameObject.tag == "EnemySoldier") 
                        //{

                        //    if (cansee == false)
                        //    {
                        //     

                        //    if (hitEnemey.transform.gameObject.GetComponent<EnemyBehaviour>().cansee == true)
                        //        {
                        //           StartCoroutine(rotateAngle());

                        //        }
                        //    }

                        //}



                    }


                


                if (hitEnemey.transform.gameObject.tag == "Soilder" ||
                    hitEnemey.transform.gameObject.tag == "Tank" ||
                    hitEnemey.transform.gameObject.tag == "Heli" ||         //if the unit can see an enemy its cansee value is set to true otherwise it is false
                    hitEnemey.transform.gameObject.tag == "PlayerBase")
                {

                    cansee = true;
                   
                }
                else 
                { 
                    
                    cansee = false;

                    if (movingInRange == false)
                    {
                        agent.stoppingDistance = defaultStoppingDistance; //this is defaulted to 30 to prevent units conjesting around a single vector3
                    }
                  
                    
                }

                if (reload == false && cansee == true)
                {
                    StartCoroutine(fireBullet());   //units will start shooting if they can see an enemy and they've taken the time to reload
                }


            }

            

            //lookingPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);

            //Debug.DrawRay(lookingPosition, gameObject.transform.forward * Range, Color.green, 1, true);

            //if (Physics.Raycast(lookingPosition, gameObject.transform.forward, out hitDirection, Range))
            //{



            //    if (hitDirection.transform.gameObject.tag == "EnemySoldier")
            //    {

            //        if (hitDirection.transform.gameObject.GetComponent<EnemyBehaviour>().cansee == true)
            //        {

                      
            //           StartCoroutine(rotateAngle());

            //        }
            //    }

            //}


           


        }
        else { cansee = false; } //if the ray misses everything they cant see anything


    }

    IEnumerator getIdleDetination()
    {


        repathingLimiter = false;

       
        agent.SetDestination(Sphere.transform.position); 
        //if the unit cannot see a unit it will attempt to follow its commander
        //the update funciton then follows with a method that see if a close ally can see in wtich the setdestinaiton is replaced wth tring to get in range of the enemy its ally can see
        yield return new WaitForSeconds(2f);

        repathingLimiter = true;

    }

    //IEnumerator rotateAngle()
    //{
    //    if (rotateWait)
    //    {
    //        rotateWait = false;

    //        relocatetargets.Clear();
    //        agent.ResetPath();



    //        RELOCATE0trans = RELOCATE0.transform.position;
    //        RELOCATE1trans = RELOCATE1.transform.position;
    //        RELOCATE2trans = RELOCATE2.transform.position;
    //        RELOCATE3trans = RELOCATE3.transform.position;

    //        relocatetargets.Add(RELOCATE0trans);
    //        relocatetargets.Add(RELOCATE1trans);
    //        relocatetargets.Add(RELOCATE2trans);
    //        relocatetargets.Add(RELOCATE3trans);

    //        agent.stoppingDistance = 0;
            

    //        agent.SetDestination(relocatetargets[Random.Range(0, 4)]);

    //        yield return new WaitForSeconds(2f);
    //        agent.stoppingDistance = 30;
    //        rotateWait = true;
    //    }

    //}


    IEnumerator getCloseAllies()
    {
        CloseAllies.Clear(); //refreshes list

        if (closestTarget != null)
        {
            movingInRange = false;  //does not need to move in range again if its already in range of an enemy
        }

        if (cansee == false && closestTarget == null && movingInRange == false) //only triggers if they're idle
        {

            for (int i = 0; i < manager.Enemies.Count(); i++)
            {
                float distance = Vector3.Distance(manager.Enemies[i].transform.position, gameObject.transform.position);

                //sorting algorithm that saves the distances as they get closer using the enemies list (in this context the enemies list holds the AI units)

                if (distance <= Range)
                {
                    CloseAllies.Add(manager.Enemies[i]); //it adds the allies if theyre in range 
                }


            }

            for (int i = 0; i < CloseAllies.Count(); i++) //grabs the closest allies and checks to see if they can see an enemy - if they can it stops the loop and sets the desination to get closer to the enemy its ally is shooting at
            {
                if (CloseAllies[i] != null && CloseAllies[i].tag != "EnemyBase") //ignores its base as it doesnt have an Enemy behaviour script as well as checking the allies didnt die during the loop iteration
                {
                    if (CloseAllies[i].GetComponent<EnemyBehaviour>().cansee == true && cansee == false && CloseAllies[i] != null) //if it cant see an enemy but a ally can AND the unit that can see still exists
                    {
                      
                            agent.stoppingDistance = Range -50; //gets in ranger but without getting up too close
                            movingInRange = true; //while this is true the unit wont follow its captain and will try to fight instead
                            agent.SetDestination(CloseAllies[i].GetComponent<EnemyBehaviour>().closestTarget.transform.position); // moves closer to enemy
                            yield return new WaitForSeconds(4f); //give it time to get there
                            movingInRange = false; // can now be triggered again if nessesary
                            agent.stoppingDistance = defaultStoppingDistance; //reset stopping distance
                        break; //breaks loop so it doesnt do this for every enemy that can see, just one
              
                    }

                }
            }
        }
        yield return new WaitForSeconds(4f);
    }




    IEnumerator fireBullet()
    {
        reload = true; //prevents from spamming projectiles

        movingInRange = false; // if it can fire at a enemy its in range

        Instantiate(Bullet, FirePoint.transform.position, FirePoint.transform.rotation); // create bullet at firepoints rotation and localtion


        yield return new WaitForSeconds(0.5f); //relaod speed

        reload = false;
    }



    IEnumerator getTargets()
    {
        targetLimiter = false;


        EnemiesInRange.Clear(); //resets list
        closestTarget = null; //resets object


        //sorting alogorithm for getting all player controled units in range
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

        if (EnemiesInRange.Count != 0) //if there are no enemies in range skip this
        {
            GameObject Closest = EnemiesInRange[0]; //stores closest enemy
            float tempdistance = Vector3.Distance(EnemiesInRange[0].transform.position, gameObject.transform.position); //stores previous itterations distance from gaobject if its closer - initialises as element 0 in the array


            for (int i = 0; i < EnemiesInRange.Count; i++)
            {

                float distance = Vector3.Distance(EnemiesInRange[i].transform.position, gameObject.transform.position); // distance of current player controlled unit

                if (tempdistance > distance)
                {
                    Closest = EnemiesInRange[i];        //if the enemy is closer it gets stored and its distance become the new benchmark to beat for the new itteration
                    tempdistance = distance;
                }



            }

            closestTarget = Closest; // give global variable a value


        }

        yield return new WaitForSeconds(2); //do this every 2 seconds


        targetLimiter = true;





    }

    private void OnTriggerEnter(Collider other)
    {

        /////////////////////HEALTH VARIABLES AND VALUES////////////////////////
        

        //the game has a rock paper sissors style combat meaning units counter other units - for each unit type it checks what project it got hit by using the tags and deducts the health accordingly

        if (gameObject.tag == "EnemySoldier")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject); //removes bullet from the game

                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck(); //death check performs some other duties involving removing population, destroying the unit and removing it from lists of active troops and updating the game over screan variables


            }

            if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 100;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }

            if (gameObject.tag == "EnemyTank")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject);

                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck();


                }

                if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 100;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }
            if (gameObject.tag == "EnemyHeli")
            {
                if (other.tag == "bullet")
                {
                    Destroy(other.transform.gameObject);

                    Health -= 100;
                    HealthBar.value = Health;
                    DeathCheck();


                }

                if (other.tag == "missile")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck();
                }

                if (other.tag == "Rocket")
                {
                    Destroy(other.transform.gameObject);
                    Health -= 50;
                    HealthBar.value = Health;
                    DeathCheck();
                }
            }


        if (Health < Health/3)
        {
            Sparks.Play();
        }

    }




    public void DeathCheck()
        {

        //death check performs some other duties involving removing population, destroying the unit and removing it from lists of active troops and updating the game over screan variables

        if (Health <= 0)
            {

                manager.Enemies.Remove(gameObject);
                Destroy(gameObject);
                gameover.EnemiesKilled = gameover.EnemiesKilled + 1;
                spawner.population = spawner.population - 1;


            }


        }


    public void AssignSpheres()
    {

        // when the unit spawns it will be asigned a sphere to follow when idle so it roams the map with other allied units as opposed to randomly

            Sphere = manager.CaptainSpheres[Random.Range(0, 3)];

            Sphere.gameObject.GetComponent<CaptainSphereBehaviour>().SubForces.Add(gameObject);

           
     }

    }
