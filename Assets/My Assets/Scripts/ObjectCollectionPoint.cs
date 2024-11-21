using UnityEngine;
using System.Collections.Generic;

public class ObjectCollectionPoint : MonoBehaviour
{
    //List of tags that can be destroyed
    [SerializeField]
    private List<string> destroyableTags = new List<string>();

    //AudioManager to play sound effects (optional)
    private AudioManager audioManager;

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
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.Success); //This is just a placeholder sound
            }

            //Destroy the other object
            Destroy(collision.gameObject);
        }
    }
}
