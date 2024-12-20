using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    //getting the timer text
    [SerializeField] TextMeshProUGUI timerText;
    //Getting the remaining time set in the inspector
    [SerializeField] public float RemainingTime;
    public bool GameOver = false;
    
    //Update is called once per frame
    void Update()
    {
        //if else statement to make sure the timer doesnt go into the negatives
        if (RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;
        }
        else if (RemainingTime < 0)
        {
            RemainingTime = 0;
        }
        //Stops the game time when the timer reaches 0
        if (RemainingTime == 0)
        {
            GameOver = true;
            //We dont need to add any task checks here as if the timer reaches 0 the game will always be a loss regardless
            //The timer will also never reach 0 if the game results in a Win
        }
        //Formatting the timer into mins and seconds
        int mins = Mathf.FloorToInt(RemainingTime/60);
        int secs = Mathf.FloorToInt(RemainingTime%60);
        timerText.text = string.Format("{00:00}:{1:00}", mins, secs);
    }

    // added by Anastasia McCormac
    public float getTimeRemaining () {

        return RemainingTime;
    }
}
