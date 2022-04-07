using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{

    private float Health = 100;
    public Slider HealthBar;


    public statManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        manager.Enemies.Add(gameObject);
        HealthBar.maxValue = Health;
        HealthBar.value = Health;

    }

    // Update is called once per frame
    void Update()
    {



        
    }


    private void OnTriggerEnter(Collider other)
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




    }



    public void DeathCheck()
    {

        if (Health <= 0)
        {

            manager.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }


    }




}
