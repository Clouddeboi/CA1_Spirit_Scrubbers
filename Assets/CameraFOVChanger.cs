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

        //distance between the two players
        float distance = Vector3.Distance(player1.position, player2.position);

        //target FOV based on the distance
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / maxDistance);

        //Smoothly transition the camera's FOV to the target FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
