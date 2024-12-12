using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    //AudioManager to play sound effects
    private AudioManager audioManager;
    public Transform target; //Target object to rotate around
    public float speed = 0f; //speed of the movement
    public float radius = 0f; //radius of the path
    public float angle = 0f; //current angle of the object

    private float targetSpeed; // Target speed for smooth transition
    private float targetRadius; // Target radius for smooth transition
    private float speedVelocity = 0f; // Time to transition to new speed
    private float radiusVelocity = 0f; // Time to transition to new radius

    private float smoothTime = 4f;
    [SerializeField] private float screamThreshold = 6.5f;

    void Start()
    {
        //Find the AudioManager in the scene if it exists
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //Call RandomizeProperties every 10 seconds starting immediately
        InvokeRepeating("RandomizeMovement", 0f, 8f);

        //Initialize target values to be the current ones
        targetSpeed = speed;
        targetRadius = radius;
    }

    //Update is called once per frame
    void Update()
    {
        // Smoothly transition to the new speed and radius using SmoothDamp
        speed = Mathf.SmoothDamp(speed, targetSpeed, ref speedVelocity, smoothTime);
        radius = Mathf.SmoothDamp(radius, targetRadius, ref radiusVelocity, smoothTime);

        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y;
        float z = target.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, z);

        angle += speed * Time.deltaTime;
    }

    public void RandomizeMovement()
    {
        Debug.Log($"Before: Speed = {speed}, Radius = {radius}");

        speed = Random.Range(1f, 7f);  //Random speed between 1 and 5
        radius = Random.Range(2f, 10f); //Random radius between 0.5 and 3

        Debug.Log($"After: Speed = {speed}, Radius = {radius}");

        //if the speed is over the threshold, play the scream sound effect
        if(speed >= screamThreshold)
        {
            audioManager.PlaySFX(audioManager.GhostScream);
        }
    }
}
