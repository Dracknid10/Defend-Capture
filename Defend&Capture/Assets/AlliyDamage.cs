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
    public statManager stat;
    public GameOver gameover;


    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<arrayofSelectedTroops>();

        manager.AllTroops.Add(gameObject);
        gameover = GameObject.FindGameObjectWithTag("gameOverManager").GetComponent<GameOver>();
        stat = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        Health = 5000000f;
       
        if (gameObject.tag == "PlayerBase")
        {
            Health = 10000f;
            
        }
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
    }


    private void OnTriggerEnter(Collider other)
    {
   
        if (gameObject.tag == "PlayerBase")
        {
            if (other.tag == "bullet" || other.tag == "missile" || other.tag == "Rocket")
            {
                Destroy(other.transform.gameObject);
                Health -= 10;
                HealthBar.value = Health;
               


                if (Health <= 0)
                {
                    gameover.AIWon = true;

                }
            }
        }
        
        if (gameObject.tag == "Soilder")
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

        if (gameObject.tag == "Tank")
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
        if (gameObject.tag == "Heli")
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

    }


    public void DeathCheck()
    {

        if (Health <= 0)
        {

            manager.AllTroops.Remove(gameObject);
            manager.SelectedTroops.Remove(gameObject);
            stat.CurrentPop = stat.CurrentPop - 1;
            Destroy(gameObject);
        }


    }

}
