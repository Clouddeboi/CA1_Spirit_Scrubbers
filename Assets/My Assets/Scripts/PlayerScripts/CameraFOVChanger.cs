using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    [SerializeField]
    private Transform player1; //Reference to Player 1

    [SerializeField]
    private Transform player2; //Reference to Player 2

    [SerializeField]
    private float minFOV = 55f; //Min camera FOV

    [SerializeField]
    private float maxFOV = 87f; //Max camera FOV

    [SerializeField]
    private float zoomSpeed = 5f; //Speed of camera zoom (in/out)

    [SerializeField]
    private float maxDistance = 20f; //Distance at which the camera reaches max FOV

    [SerializeField]
    private float moveSpeed = 5f; //Speed of camera movement (left/right only)

    [SerializeField]
    private float sideDistanceThreshold = 5f; //Threshold for how close the players need to be for the camera to slide left/right

    private Camera cam; //Reference to the Camera component

    private void Start()
    {
        //Get the Camera component
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component not found!");
        }
    }

    private void Update()
    {
        if (cam == null || player1 == null || player2 == null)
            return;

        //Calculate the distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        //Determine if the players are very close to each other (within the threshold)
        bool areVeryClose = distance < sideDistanceThreshold;

        //If the players are very close, move the camera horizontally to the midpoint
        if (areVeryClose)
        {
            //Calculate the midpoint between the two players
            Vector3 midpoint = (player1.position + player2.position) / 2f;

            //Set the camera position to the midpoint and keep the y and z values the same since we dont use them for this
            Vector3 targetPosition = new Vector3(midpoint.x, transform.position.y, transform.position.z);

            //Smoothly move the camera horizontally to the midpoint position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            //If the players are not very close, adjust the camera's FOV based on the distance
            float targetFOV = Mathf.Lerp(minFOV, maxFOV, Mathf.Clamp(distance / maxDistance, 0f, 1f));
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

            //keep the camera at the midpoint (but without sliding) when they are far apart
            Vector3 midpoint = (player1.position + player2.position) / 2f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(midpoint.x, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        }
    }
}
