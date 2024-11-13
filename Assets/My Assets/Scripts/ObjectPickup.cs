using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    //Layer mask to specify which layers the raycast will interact with
    //for now we just want the interactable layer
    private LayerMask layerMask; 

    //The Range of the Raycast
    private float raycastRange = 10f; 
    //We check if the player is facing an object
    //get; allows other scripts to retrieve the value
    //private set doesn't let the other scripts edit the value
    public bool isFacingObject { get; private set; }

    //Method to configure LayerMask dynamically
    //This is because we want to get the layer from another script
    public void SetLayerMask(LayerMask newLayerMask)
    {
        layerMask = newLayerMask;
    }

    //Method to configure Raycast Range dynamically
    //This is because we want to get the layer from another script
    public void SetRaycastRange(float newRange)
    {
        raycastRange = newRange;
    }

    //Method to check for interactable objects in front of the player
    public void CheckForObject()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, raycastRange, layerMask))
        {
            Debug.Log("Hit Something: " + hitInfo.collider.gameObject.name);//write the name of the object we hit with the raycast
            Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.green);//we use a green raycast
            isFacingObject = true;//we set the bool as true since we are looking at the object
        }
        else//if we aren't we do the opposite
        {
            Debug.Log("Hit Nothing!");
            Debug.DrawRay(transform.position, transform.forward * raycastRange, Color.red);
            isFacingObject = false;
        }
    }
}
