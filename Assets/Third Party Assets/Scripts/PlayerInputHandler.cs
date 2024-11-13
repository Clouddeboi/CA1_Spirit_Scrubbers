using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Mover mover;
    private ObjectPickup objectPickup;

    [SerializeField] private LayerMask interactableLayer; //Set this in the Inspector
    [SerializeField] private float raycastRange = 10f; //Set raycast range in the Inspector

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<Mover>();
        int index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);

        //check if mover is present
        if (mover == null)
        {
            Debug.LogError("Mover component for player index " + index + " not found.");
            return;
        }

        //Ensure ObjectPickup is attached to this player's GameObject
        objectPickup = mover.GetComponent<ObjectPickup>();
        if (objectPickup == null)
        {
            objectPickup = mover.gameObject.AddComponent<ObjectPickup>(); //Attach at runtime
            objectPickup.SetLayerMask(interactableLayer); //Set LayerMask for interactables
            objectPickup.SetRaycastRange(raycastRange); //Set raycast range
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (mover != null)
        {
            mover.SetInputVector(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(CallbackContext context)
    {
        //debugging for when we are interacting with an object/press the interact input
        if (context.performed)
        {
            Debug.Log("Interact button pressed!");

            if (objectPickup != null)
            {
                objectPickup.CheckForObject();

                if (objectPickup.isFacingObject)
                {
                    Debug.Log("Interact button pressed and interacted with an object!");
                }
                else
                {
                    Debug.Log("No object to interact with.");
                }
            }
        }
    }
}
