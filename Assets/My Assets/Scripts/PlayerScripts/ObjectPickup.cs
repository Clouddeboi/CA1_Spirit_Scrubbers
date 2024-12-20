using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    //We add a referene to the audio manager so we can play sounds
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //Layer mask to specify which layers the detection will interact with
    //for now we just want the interactable layer
    private LayerMask layerMask; 

    //Size of the detection box (as a 3D box shape in front of the player)
    [SerializeField] private Vector3 boxSize = new Vector3(2f, 2f, 2f);  // Default size (2, 2, 2)

    //Offset of the detection box relative to the player (in front of the player)
    [SerializeField] private Vector3 boxOffset = new Vector3(0f, 1f, 1f); 

    //[SerializeField] float throwForce = 25f;

    //Boolean to check if the player is facing an object
    //get; allows other scripts to retrieve the value
    //private set doesn't let the other scripts edit the value
    public bool isFacingObject { get; private set; }

    //The object the player is carrying
    private GameObject currentObject = null;

    //Boolean to check if the player is carrying an object
    private bool isCarryingObject = false;

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
                //Store the first object we hit
                currentObject = hitCollider.gameObject;
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
            currentObject = null; // Clear the current object if none detected
        }
    }

    //Method to pick up the object
    public void PickupObject(GameObject objectToPickup)
    {
        if (objectToPickup != null)
        {
            //We are now carrying an object
            isCarryingObject = true;
            
            //Store the object we picked up
            currentObject = objectToPickup;
        }
    }

    //Method to drop the object 
    //(updated)Method now throws the object in the direction the player is facing
    public void DropObject(GameObject objectToDrop, float throwForce)
    {
        if (objectToDrop != null)
        {
            //Throwing Item Audio
            audioManager.PlaySFX(audioManager.ItemThrow);
            //We are no longer carrying an object
            isCarryingObject = false;

            //Apply force to the object's Rigidbody
            //Get the objects rigidbody, if its not null execute throwing item
            Rigidbody objectRigidbody = objectToDrop.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                //The direction the player is facing
                Vector3 throwDirection = transform.forward;
                //Add force to the object in the direction the player is facing
                objectRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("No Rigidbody found on the object. Unable to throw.");
            }

            //Clear the current object we were carrying
            currentObject = null;
        }
    }


    //Method to update the carried object's position
    //Call this from another script to move the object with the player
    public void UpdateCarriedObjectPosition()
    {
        if (currentObject != null && isCarryingObject)
        {
            //Position the object just in front of the player
            Vector3 carryPositionOffset = new Vector3(0, 1.5f, 0);
            currentObject.transform.position = transform.position + transform.forward * 1.75f + carryPositionOffset;  //Adjust distance if needed
            currentObject.transform.rotation = transform.rotation;  //Keep rotation aligned with player
        }
    }

    //Public method to check if the player is carrying an object
    public bool IsCarryingObject()
    {
        return isCarryingObject;
    }

    //Public method to get the currently carried object
    public GameObject GetCarriedObject()
    {
        return currentObject;
    }

    //Getter method for box size (so other scripts can access it)
    public Vector3 GetBoxSize()
    {
        return boxSize;
    }

    //Getter method for box offset (so other scripts can access it)
    public Vector3 GetBoxOffset()
    {
        return boxOffset;
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
    /*
        Method to clear the carried object state
        This is used for when we combine an item while its in our hands
        We need this so we can pick up items again as the game still thinks 
        we are holding and item and will throw it instead of picking it up
    */

    public void ClearCarriedObject()
    {
        isCarryingObject = false;
        currentObject = null;
    }
}