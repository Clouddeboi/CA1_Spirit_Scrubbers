using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Preferences", menuName = "Scriptable Objects/CS_Settings_Preferences")]
public class CS_Settings_Preferences : ScriptableObject {

    [SerializeField]
    [Tooltip("Game version indicator")]
    private string game_Ver = "Interim_Release";

    [Header("Audio Settings")]

    [SerializeField]
    [Tooltip("Volume Setting for Global Audio Volume.")]
    [Range(0, 1)]
    private float vol_Master = 0.75f;

    [SerializeField]
    [Tooltip("Volume Setting for Effect Sounds.")]
    [Range(0, 1)]
    private float vol_SFX= 0.75f;

    [SerializeField]
    [Tooltip("Volume Setting for Background Music.")]
    [Range(0, 1)]
    private float vol_BGM = 0.75f;

    protected float Vol_Master { get => vol_Master; set => vol_Master = value; }
    protected float Vol_SFX { get => vol_SFX; set => vol_SFX = value; }
    protected float Vol_BGM { get => vol_BGM; set => vol_BGM = value; }
}