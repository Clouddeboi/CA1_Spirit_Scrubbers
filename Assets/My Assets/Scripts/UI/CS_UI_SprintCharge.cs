// Author: Anastasia McCormac
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CS_UI_SprintCharge : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player script that contains the dash timer.")]
    private PlayerInputHandler player;

    
    private Image img;
    private float maxTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start () {

        
        img = this.GetComponentInParent<Image>();
    }

    public void addPlayer(PlayerInputHandler input) {
        if ((this.name == "IMG_P1_Charges_Stat"))
        player = input;
    }


    // Update is called once per frame
    void FixedUpdate () {

        if (player == null) {
            player = GetComponent<PlayerInputHandler>();
            maxTime = player.getDashTimer();
        }
        img.fillAmount = player.getDashTimer() / maxTime;
    }
}
