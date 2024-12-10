// Author: Anastasia McCormac
using UnityEngine;
using UnityEngine.UI;

public class CS_UI_TimerBar : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Timer script that contains the time component forr the game.")]
    private TimerCount time;

    private Image img;
    

    private float maxTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxTime = time.getTimeRemaining();
        img = this.GetComponentInParent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        img.fillAmount = time.getTimeRemaining() / maxTime;
        
    }
}
