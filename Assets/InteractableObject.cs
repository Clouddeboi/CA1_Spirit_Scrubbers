using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // This script is a marker that can be used to identify interactable objects.
    // You can extend this later if you want more functionality (e.g., when the player interacts with it).

    // Optional: Add an interact method if you want to define what happens when the player interacts with this object.
    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        // Add any interaction logic here (e.g., picking up the object, opening a door, etc.)
    }
}
