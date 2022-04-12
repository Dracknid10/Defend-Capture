using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlliyDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public float Health;
    public Slider HealthBar;
    public arrayofSelectedTroops manager;



    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();
        

      
        Health = 100f;
        HealthBar.maxValue = Health;
        HealthBar.value = Health;


    }


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("HIT");

        if (gameObject.tag == "Soilder")
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
                Health -= 70;
                HealthBar.value = Health;
                DeathCheck();
            }
        }

    }


    public void DeathCheck()
    {

        if (Health <= 0)
        {

            manager.AllTroops.Remove(gameObject);
            Destroy(gameObject);
        }


    }

}
