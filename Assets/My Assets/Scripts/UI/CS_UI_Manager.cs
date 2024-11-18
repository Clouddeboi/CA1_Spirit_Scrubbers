// Author Anastasia McCormac
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// TODO: This is bad, I need to exhange it for a better system, but it will do for Interim Release. 
/// Even if I change this for an array it would be better. 
/// </summary>
public class CS_UI_Manager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of All loaded Canvases.")]
    [ReadOnly]
    private List<Canvas> cvs;

    private string[] cvNames = {

        "CV_Main_Menu",
        "CV_Options_Menu",
        "CV_Pause_Menu",
        "CV_Game_Play",
        "CV_Game_Over_Menu",
        "CV_Victory_Menu"
        };
/*
    [SerializeField]
    [ReadOnly]
    [Tooltip("Main Menu Canvas")]
    private Canvas mainMenu;

    [SerializeField]
    [ReadOnly]
    [Tooltip("Options Menu Canvas")]
    private Canvas optionsMenu;

    [SerializeField]
    [ReadOnly]
    [Tooltip("Gameplay UI Canvas")]
    private Canvas gameUI;

    [SerializeField]
    [ReadOnly]
    [Tooltip("Pause Menu Canvas")]
    private Canvas pauseMenu;

    [SerializeField]
    [ReadOnly]
    [Tooltip("Game Over Menu Canvas")]
    private Canvas gameOverMenu;

    [SerializeField]
    [ReadOnly]
    [Tooltip("Game Victory Menu Canvas")]
    private Canvas gameVictoryMenu;*/

    private void Start () {
        
        foreach(string n in cvNames) {

            if (GameObject.Find(n) != null) {

                cvs.Add(GameObject.Find(n).GetComponent<Canvas>());
                GameObject.Find(n).GetComponent<Canvas>().enabled = false;
            }
        }

        if (cvs.Contains<Canvas>(GameObject.Find("CV_Main_Menu").GetComponent<Canvas>())) {

            cvs[0].enabled = true;
        }
    }
    /*
    private void Start () {

        // Dynamically find all Relevant canvases in the scene. 
        if (GameObject.Find("CV_Main_Menu") != null) {

            mainMenu = GameObject.Find("CV_Main_Menu").GetComponent<Canvas>();
        }

        if (GameObject.Find("CV_Options_Menu") != null) {

            optionsMenu = GameObject.Find("CV_Options_Menu").GetComponent<Canvas>();
            optionsMenu.enabled = false;
        }

        // If main Menu canvas is found, no need to look for in-game canvases as they should not be present. 
        if (mainMenu == null) {

            if (GameObject.Find("CV_Game_Play") != null) {

                gameUI = GameObject.Find("CV_Game_Play").GetComponent<Canvas>();
            }

            if (GameObject.Find("CV_Pause_Menu") != null) {

                pauseMenu = GameObject.Find("CV_Pause_Menu").GetComponent<Canvas>();
                pauseMenu.enabled = false;

            }

            if (GameObject.Find("CV_Game_Over_Menu") != null) {

                gameOverMenu = GameObject.Find("CV_Game_Over_Menu").GetComponent<Canvas>();
                gameOverMenu.enabled = false;
            }

            if (GameObject.Find("CV_Victory_Menu") != null) {

                gameVictoryMenu = GameObject.Find("CV_Victory_Menu").GetComponent<Canvas>();
                gameVictoryMenu.enabled = false;
            }
        }
    }
 */

    /// <summary>
    /// Method to switch to specified canvas.
    /// </summary>
    /// <param name="target"></param>
    public void swapToCanvas(string target) {

        foreach (Canvas cv in cvs) {

            if (cv != GameObject.Find(target).GetComponent<Canvas>()) {

                cv.enabled = false;
            }

            else {

                cv.enabled = true;
            }
        }
    }   
}
