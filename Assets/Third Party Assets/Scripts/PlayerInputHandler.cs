using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Mover mover;
    private ObjectPickup objectPickup;

    //Set the interactable layer in the Inspector
    [SerializeField] private LayerMask interactableLayer; 

    //Set the detection box size (3D) in the Inspector
    [SerializeField] private Vector3 detectionBoxSize = new Vector3(2f, 2f, 2f); 

    private void Awake()
    {
        //Get the PlayerInput component attached to this GameObject
        playerInput = GetComponent<PlayerInput>();
        //Find all movers and assign the appropriate one to the player index
        var movers = FindObjectsOfType<Mover>();
        int index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);

        //Check if mover is present
        if (mover == null)
        {
            Debug.LogError("Mover component for player index " + index + " not found.");
            return;
        }

        //Ensure ObjectPickup is attached to this player's GameObject
        objectPickup = mover.GetComponent<ObjectPickup>();
        if (objectPickup == null)
        {
            //Attach ObjectPickup at runtime if not already attached
            objectPickup = mover.gameObject.AddComponent<ObjectPickup>();
            //Set LayerMask for interactables
            objectPickup.SetLayerMask(interactableLayer); 
            //Set detection range (box size)
            objectPickup.SetDetectionRange(detectionBoxSize); 
        }
    }

    //Method to handle movement input
    public void OnMove(CallbackContext context)
    {
        if (mover != null)
        {
            //Pass the movement input to the Mover script
            mover.SetInputVector(context.ReadValue<Vector2>());
        }
    }

    //Method to handle interact input (button press)
    public void OnInteract(CallbackContext context)
    {
        //Debugging for when we are interacting with an object/press the interact input
        if (context.performed)
        {
            Debug.Log("Interact button pressed!");

            if (objectPickup != null)
            {
                //Check for interactable objects in front of the player
                objectPickup.CheckForObject();

                if (objectPickup.isFacingObject)
                {
                    //If player is not already carrying an object, pick it up
                    if (!objectPickup.IsCarryingObject())
                    {
                        GameObject objectToPickup = objectPickup.GetCarriedObject();
                        if (objectToPickup != null)
                        {
                            objectPickup.PickupObject(objectToPickup);
                            Debug.Log("Picked up object: " + objectToPickup.name);
                        }
                    }
                    //Otherwise drop the object if it's already being carried
                    else
                    {
                        GameObject objectToDrop = objectPickup.GetCarriedObject();
                        if (objectToDrop != null)
                        {
                            objectPickup.DropObject(objectToDrop);
                            Debug.Log("Dropped object: " + objectToDrop.name);
                        }
                    }
                }
                else
                {
                    Debug.Log("No object to interact with.");
                }
            }
        }
    }

    private void Update()
    {
        //Update the position of the carried object every frame
        if (objectPickup != null && objectPickup.IsCarryingObject())
        {
            //Keep the carried object in front of the player
            objectPickup.UpdateCarriedObjectPosition();
        }
    }
}
