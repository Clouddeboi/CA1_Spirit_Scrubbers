//Author: Anastasia McCormac
using UnityEngine;

public class CS_UI_Quit_Game : MonoBehaviour
{

    public void QuitGame () {
        Time.timeScale = 1;
        Application.Quit();
    }
}
