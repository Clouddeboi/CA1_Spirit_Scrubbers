using UnityEngine;

public class FireSFX : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        //Find the AudioManager in the scene
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("FireSFX: AudioManager not found in the scene!");
            return;
        }


        //Note: The sfx doesnt loop properly so the script is disabled for now 
        //Might change it to play every few secs instead

        //Start looping fire ambiance if Fire clip and ambiance source are assigned
        if (audioManager.SFXSource != null && audioManager.Fire != null)
        {
            audioManager.SFXSource.clip = audioManager.Fire;
            audioManager.SFXSource.loop = true; //Enable looping
            audioManager.SFXSource.Play(); //Start playing the clip
        }
        else
        {
            Debug.LogError("FireSFX: Missing AudioSource or Fire clip in AudioManager!");
        }
    }
}
