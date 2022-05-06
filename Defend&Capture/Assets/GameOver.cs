using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update



    public bool playerWon = false;
    public bool AIWon = false;

    public TMPro.TextMeshProUGUI Timegame;
    public TMPro.TextMeshProUGUI EnemiesKilledtext;
    public TMPro.TextMeshProUGUI WinOrLose;
    public int EnemiesKilled;

    private float secondsCount =0;
    public int minuteCount = 0;

    private bool timer = true;

    public Animator UiAnimation;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       

        if (playerWon == true || AIWon == true)
        {

            UiAnimation.SetBool("GameOver", true);


            Timegame.SetText("You Survived: " + minuteCount + ":" + secondsCount);
            EnemiesKilledtext.SetText("You Killed: " + EnemiesKilled);

            if (playerWon == true)
            {
                WinOrLose.SetText("You Destroyed the Foe!");

            }

            if (AIWon == true)
            {
                WinOrLose.SetText("the Enemy has won");

            }

            StartCoroutine(pauseTime());

        }
        else
        {
            if (timer == true)
            {
                StartCoroutine(timercount());
            }
        }

       


    }
    IEnumerator pauseTime()
    {

        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;

    }





        IEnumerator timercount()
    {

        timer = false;

        secondsCount = secondsCount + 1;

        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }


        yield return new WaitForSeconds(1f);

        timer = true;
    }







}
