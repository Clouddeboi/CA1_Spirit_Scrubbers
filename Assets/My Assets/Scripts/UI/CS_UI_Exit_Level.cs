//Author: Anastasia McCormac
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_UI_Exit_Level : MonoBehaviour {

    public void ExitLevel () {

        SceneManager.LoadScene(0);
    }
}
