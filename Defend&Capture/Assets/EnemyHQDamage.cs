using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHQDamage : MonoBehaviour
{

    public float Health;
    public Slider HealthBar;
    public statManager manager;
    public GameOver gameover;
   
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();
        manager.Enemies.Add(gameObject);
        gameover = GameObject.FindGameObjectWithTag("gameOverManager").GetComponent<GameOver>();

        if (gameObject.tag == "EnemyBase")
        {
            Health = 10000f;

        }
        HealthBar.maxValue = Health;
        HealthBar.value = Health;


    }


    private void OnTriggerEnter(Collider other)
    {

        if (gameObject.tag == "EnemyBase" && other.tag == "bullet" || other.tag == "missile" || other.tag == "Rocket")
        {

            Health -= 10;
            HealthBar.value = Health;
            if (Health <= 0)
            {
                gameover.playerWon = true;
            }

        }
    }
}
