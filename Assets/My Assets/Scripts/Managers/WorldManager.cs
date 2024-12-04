using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Random Path Settings")]
    [SerializeField] private RandomPath CollectioPoint; //Reference to the cube with the random path movement
    [SerializeField] private float RandomPathActivationDelay = 20f; //Delay before random movement starts

    [Header("Wall Settings")]
    [SerializeField] private LayeredWallHazard Wall; //Reference to the cube with the random path movement
    [SerializeField] private float WallActivationDelay = 32f; //Delay before random movement starts

    void Start()
    {
        //Start the RandomPath movement after the specified delay
        Invoke(nameof(ActivateRandomPath), RandomPathActivationDelay);

        //Start the Wall hazard
        Invoke(nameof(ActivateWall), RandomPathActivationDelay);
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
}
