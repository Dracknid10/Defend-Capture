using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{


    public statManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<statManager>();

        manager.Enemies.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
