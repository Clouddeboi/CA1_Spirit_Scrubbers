using UnityEngine;

public class LayeredWallHazard : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [Header("Wall Settings")]
    [SerializeField] private float toggleInterval = 5f; // Time interval for toggling visibility

    [Header("Components")]
    [SerializeField] private MeshRenderer meshRenderer; // Assign the wall's MeshRenderer
    [SerializeField] private Collider wallCollider;     // Assign the wall's Collider

    private bool isVisible = true; // Tracks the wall's visibility state

    void Start()
    {
        // Ensure components are assigned; if not, find them automatically
        if (!meshRenderer) meshRenderer = GetComponent<MeshRenderer>();
        if (!wallCollider) wallCollider = GetComponent<Collider>();

        // Start the toggle routine
        InvokeRepeating(nameof(ToggleWall), toggleInterval, toggleInterval);
    }

    public void ToggleWall()
    {
        audioManager.PlaySFX(audioManager.WallAppears);
        // Toggle visibility
        isVisible = !isVisible;

        // Update renderer and collider
        if (meshRenderer) meshRenderer.enabled = isVisible;
        if (wallCollider) wallCollider.enabled = isVisible;

        Debug.Log($"Wall is now {(isVisible ? "visible" : "invisible")}");
    }
}
