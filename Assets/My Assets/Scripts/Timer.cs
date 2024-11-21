using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //getting the timer text
    [SerializeField] TextMeshProUGUI timerText;
    //Getting the remaining time set in the inspector
    [SerializeField]float RemainingTime;
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
            Time.timeScale = 0;
        }
        //Formatting the timer into mins and seconds
        int mins = Mathf.FloorToInt(RemainingTime/60);
        int secs = Mathf.FloorToInt(RemainingTime%60);
        timerText.text = string.Format("{00:00}:{1:00}", mins, secs);
    }
}
