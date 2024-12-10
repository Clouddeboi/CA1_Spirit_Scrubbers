using UnityEngine;

public class RandomPath : MonoBehaviour
{
    public float speed = 5f; //Movement speed of the object

    //Assign the corner GameObjects in the Inspector
    public Transform cornerTopLeft;
    public Transform cornerTopRight;
    public Transform cornerBottomLeft;
    public Transform cornerBottomRight;

    private float minX, maxX, minZ, maxZ; //Calculated bounds
    private Vector3 targetPosition;

    void Start()
    {
        //Calculate bounds based on the positions of the corner GameObjects
        //This is so the bounds are not just corners
        minX = Mathf.Min(cornerTopLeft.position.x, cornerBottomLeft.position.x);
        maxX = Mathf.Max(cornerTopRight.position.x, cornerBottomRight.position.x);
        minZ = Mathf.Min(cornerBottomLeft.position.z, cornerTopLeft.position.z);
        maxZ = Mathf.Max(cornerTopRight.position.z, cornerBottomRight.position.z);

        //Set the initial target position
        SetNewTargetPosition();
    }

    void Update()
    {
        //Move towards the current target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //Check if the object has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            //Generate a new target position within the bounds
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        //Generate a random position within the rectangle defined by the bounds
        targetPosition = new Vector3(
            Random.Range(minX, maxX),//Random x within bounds
            transform.position.y,//Maintain current y position (we can adjust this to also change if we want to eventually)
            Random.Range(minZ, maxZ)//Random z within bounds
        );
    }
}
