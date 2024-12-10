// Author Anastasia McCormac
using UnityEngine;

public class CS_UI_Show_Player_Stat : MonoBehaviour {

    GameObject panelP1;
    GameObject panelP2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start () {
        
        panelP1 = GameObject.Find("Panel_P1_Stat");
        panelP2 = GameObject.Find("Panel_P2_Stat");

        panelP1.SetActive(false);
        panelP2.SetActive(false);

    }

    public void EnablePlayerStat () {

        if (panelP1.active == false)
            panelP1.SetActive(true);
        

        else
            panelP2.SetActive(true);        
    }
}
