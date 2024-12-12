using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectCollectionPoint : MonoBehaviour
{
    //List of tags that can be destroyed
    [SerializeField]
    private List<string> destroyableTags = new List<string>();

    [SerializeField]
    private TaskManager taskManager;

    //AudioManager to play sound effects
    private AudioManager audioManager;
    public GameObject winScreen;

    private void Start()
    {
        //Find the AudioManager in the scene if it exists
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Get the tag of the object we collided with
        string otherTag = collision.gameObject.tag;

        //Check if the tag is in the list of destroyable tags
        if (destroyableTags.Contains(otherTag))
        {
            //This fixes the bug of players throwing an object instead of picking it up
            //if the item gets destroyed while still in their hands by clearing the carried object state
            ObjectPickup objectPickup = FindObjectOfType<ObjectPickup>();
            objectPickup.ClearCarriedObject();

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.Success); //This is just a placeholder sound
            }

            if (taskManager != null)
            {
                //updates the task based on the item associated with it
                taskManager.UpdateTaskProgress(otherTag);

                //Check if all tasks are complete after updating the progress
                if (taskManager.AreAllTasksComplete())
                {
                    Debug.Log("All tasks completed!");

                    if (winScreen != null)
                    {
                        winScreen.SetActive(winScreen.activeSelf); //Toggles visibility of the winScreen if you won the game
                    }

                    Time.timeScale = 0;//This is a temporary "Win"
                }
                else
                {
                    Debug.Log("There are still tasks left to do!");
                }
            }

            //Destroy the other object
            Destroy(collision.gameObject);
        }
    }
}
