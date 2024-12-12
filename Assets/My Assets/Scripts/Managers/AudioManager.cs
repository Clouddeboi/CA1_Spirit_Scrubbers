using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //We define our audio sources and clips
    [Header("---Audio Sources---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---Audio Clips---")]
    public AudioClip backgroundMusic;
    public AudioClip Dash;
    public AudioClip Success;
    public AudioClip WallAppears;
    public AudioClip ItemThrow;
    //public AudioClip Footsteps;

    [Header("---Ambiance---")]
    public AudioClip GhostScream;
    public AudioClip Fire;
    public AudioClip ambianceSource;

    //Array of Piano Notes
    [Header("---Piano Notes---")]
    public AudioClip[] PianoNotes;
    //we leave this empty as it gets populated by a random sound when triggered
    public AudioClip ActivePianoNote;

    //When the game starts we play the background music
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    //We call this method when we want to play an audio clip
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
