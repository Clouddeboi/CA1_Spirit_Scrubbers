//Author: Anastasia McCormac
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_UI_Start_Game : MonoBehaviour
{

    public void StartGame () {

        SceneManager.LoadScene(1);
    }

}
