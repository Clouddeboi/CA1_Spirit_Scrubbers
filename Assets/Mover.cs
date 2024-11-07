using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f; //Player's movement speed

    [SerializeField]
    private int playerIndex = 0; //Player's index, to differentiate between players

    private Rigidbody rb; //Reference to Rigidbody component
    private Vector2 inputVector = Vector2.zero; //Stores input direction from the player

    private void Awake()
    {
        //Get the Rigidbody component, if missing, gives an error
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }

        //Ensure Rigidbody is set to use discrete collision detection
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    //Getter for player index
    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    //Setter to update movement direction
    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void FixedUpdate()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is still missing during FixedUpdate!");
            return;
        }

        //Convert input to 3D direction
        Vector3 targetMoveDirection = new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed;

        //Calculate new position based on input and current position
        Vector3 newPosition = rb.position + targetMoveDirection * Time.fixedDeltaTime;

        //Use MovePosition to move the Rigidbody
        rb.MovePosition(newPosition);
    }
}
