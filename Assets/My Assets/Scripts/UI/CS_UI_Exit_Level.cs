//Author: Anastasia McCormac
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_UI_Exit_Level : MonoBehaviour {

    public void ExitLevel () {

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
