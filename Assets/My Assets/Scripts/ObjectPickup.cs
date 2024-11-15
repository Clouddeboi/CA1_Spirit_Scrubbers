using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    //Layer mask to specify which layers the detection will interact with
    //for now we just want the interactable layer
    private LayerMask layerMask; 

    //Size of the detection box (as a 3D box shape in front of the player)
    private Vector3 boxSize = new Vector3(2f, 2f, 2f);  // Default size (2, 2, 5)

    //Offset of the detection box relative to the player (in front of the player)
    private Vector3 boxOffset = new Vector3(0f, 1f, 1f); 

    //Boolean to check if the player is facing an object
    //get; allows other scripts to retrieve the value
    //private set doesn't let the other scripts edit the value
    public bool isFacingObject { get; private set; }

    //Method to set the detection box size dynamically
    //This is to allow changing the box size from another script
    public void SetDetectionRange(Vector3 newBoxSize)
    {
        // Update the detection box size when called
        boxSize = newBoxSize;
    }

    //Method to configure LayerMask dynamically
    //This is because we want to get the layer from another script
    public void SetLayerMask(LayerMask newLayerMask)
    {
        layerMask = newLayerMask;
    }

    //Method to check for objects in the detection area
    public void CheckForObject()
    {
        //Reset the value for isFacingObject every time we check
        //This is so it doesn't always stay as true by accident
        isFacingObject = false;

        //Calculate the center of the detection box
        //This is so we can offset the box according to the player position
        Vector3 boxCenter = transform.position + transform.TransformDirection(boxOffset);
        
        //Perform a box overlap check to detect objects within the detection box
        //We are checking for objects with our defined layer mask (interactable)
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, layerMask);

        //if we detect an object we draw a line to the closest object and debug the info in the terminal
        if (hitColliders.Length > 0)
        {
            //We iterate through everything
            //We use foreach to not worry about indexing
            foreach (Collider hitCollider in hitColliders)
            {
                //We Write the name of the object we hit with the detection box
                Debug.Log("Object detected: " + hitCollider.gameObject.name);
                //We set our bool to true
                isFacingObject = true;
                //Draw a debug line from the player to the detected object
                Debug.DrawLine(transform.position, hitCollider.transform.position, Color.green); //Show a debug line
                //Stop after finding the first interactable object
                //This is so we don't accidently end up detecting multiple items and so we won't pick them all up at once
                break;
            }
        }
        else
        {
            //If no objects are detected
            Debug.Log("No objects detected in the box.");
        }
    }

    //Draw the detection box for debugging in the scene view
    private void OnDrawGizmos()
    {
        //We make sure to only draw the box when the game is playing
        if (!Application.isPlaying) return;
        
        //Set the color of the gizmo
        Gizmos.color = Color.yellow;
        //We draw the box taking in account the offset and player position in the world
        Vector3 boxCenter = transform.position + transform.TransformDirection(boxOffset);
        //Setting the rotation, position and scale of the box
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        //Draw a wireframe box to visualize the detection area
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
