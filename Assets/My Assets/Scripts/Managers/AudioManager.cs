using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //We define our audio sources and clips
    [Header("---Audio Sources---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---Audio Clips---")]
    public AudioClip backgroundMusic;
    public AudioClip Dash;
    public AudioClip Success;
    public AudioClip WallAppears;
    public AudioClip PianoKey1;
    public AudioClip PianoKey2;
    public AudioClip PianoKey3;
    public AudioClip PianoKey4;
    //public AudioClip Footsteps;
    public AudioClip ItemThrow;

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
