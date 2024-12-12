using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Random Path Settings")]
    [SerializeField] private RandomPath CollectioPoint; //Reference to the cube with the random path movement
    [SerializeField] private float RandomPathActivationDelay = 12.5f; //Delay before random movement starts

    [Header("Wall Settings")]
    [SerializeField] private LayeredWallHazard Wall; //Reference to the cube with the random path movement
    [SerializeField] private float WallActivationDelay = 22.5f; //Delay before random movement starts

    [Header("Game Over Settings")]
    [SerializeField] private TimerCount timer; //Reference to the TimerCount script
    [SerializeField] private GameObject GameOverScreen; //Game Over screen
    private bool isGameOverScreenDisplayed = false;

    void Start()
    {
        //Start the RandomPath movement after the specified delay
        Invoke(nameof(ActivateRandomPath), RandomPathActivationDelay);

        //Start the Wall hazard
        Invoke(nameof(ActivateWall), RandomPathActivationDelay);
    }

    void Update()
    {
        // Check if the game is over
        if (timer != null && timer.GameOver && !isGameOverScreenDisplayed)
        {
            DisplayGameOverScreen();
        }
    }

    //Method to activate the random movement after delay
    void ActivateRandomPath()
    {
        if (CollectioPoint != null)
        {
            CollectioPoint.enabled = true; //Enable the RandomPath script, starting the random movement
            Debug.Log("Random path movement activated!");
        }
    }

    void ActivateWall()
    {
        if(Wall != null)
        {
            Wall.enabled = true; //Enable the wall script
            Debug.Log("Wall hazard activated!");
        }
    }

    //Display the Game Over screen
    void DisplayGameOverScreen()
    {
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
            Debug.Log("WorldManager: Game Over screen displayed!");
            isGameOverScreenDisplayed = true; //Prevent multiple activations
        }

        Time.timeScale = 0; //Pause the game
    }
}
