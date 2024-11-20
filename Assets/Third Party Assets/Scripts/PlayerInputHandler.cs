using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    //Audio Manager
    AudioManager audioManager;
    private PlayerInput playerInput;
    private Mover mover;
    private ObjectPickup objectPickup;

    //Set the interactable layer in the Inspector
    [SerializeField] private LayerMask interactableLayer;

    //Set the detection box size (3D) in the Inspector
    [SerializeField] private Vector3 detectionBoxSize = new Vector3(2f, 2f, 2f);

    //Set the dash speed in the editor
    [SerializeField] private float dashSpeed = 20f;
    //Duration of the dash
    [SerializeField] private float dashDuration = 0.2f;
    //Cooldown time for dashing
    [SerializeField] private float dashCooldown = 1f;
    //Bool to check if the player is dashing
    private bool isDashing = false; 
    //Time when the dash ends
    private float dashEndTime = 0f;
    //Time when the player can dash again
    private float nextDashTime = 0f; 
    //dashEndTime and nextDashTime are used as variables for a dash cooldown

    private void Awake()
    {
        //Audio Manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

    //Method to handle dash input
    public void OnDash(CallbackContext context)
    {
        //Check if the dash input is performed and if dashing is allowed
        if (context.performed && Time.time >= nextDashTime)
        {
            //Dash SFX
            audioManager.PlaySFX(audioManager.Dash);
            StartDash();
        }
    }

    private void StartDash()
    {
        //Set dashing bool
        isDashing = true;
        //Set when the dash ends
        dashEndTime = Time.time + dashDuration; 
        //Set next dash availability
        nextDashTime = Time.time + dashCooldown;
        //Increase mover's speed to dash speed
        //This is so we can only dash while moving
        mover.SetDashSpeed(dashSpeed);
    }

    //Method to handle interact input (button press)
    public void OnInteract(CallbackContext context)
    {
        //Check if the interact input is performed
        if (context.performed)
        {
            if (objectPickup != null)
            {
                //Check for interactable objects in front of the player
                objectPickup.CheckForObject();

                if (objectPickup.isFacingObject)
                {
                    //If player is not carrying an object, pick it up
                    if (!objectPickup.IsCarryingObject())
                    {
                        GameObject objectToPickup = objectPickup.GetCarriedObject();
                        if (objectToPickup != null)
                        {
                            objectPickup.PickupObject(objectToPickup);
                        }
                    }
                    //Otherwise, drop the carried object
                    else
                    {
                        GameObject objectToDrop = objectPickup.GetCarriedObject();
                        if (objectToDrop != null)
                        {
                            objectPickup.DropObject(objectToDrop);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        //Update the position of the carried object every frame
        if (objectPickup != null && objectPickup.IsCarryingObject())
        {
            objectPickup.UpdateCarriedObjectPosition();
        }

        //End dash if the duration has passed
        if (isDashing && Time.time >= dashEndTime)
        {
            //Reset dashing bool
            isDashing = false;
            //Reset mover's speed to normal
            mover.SetDashSpeedToNormal();
        }
    }
}
