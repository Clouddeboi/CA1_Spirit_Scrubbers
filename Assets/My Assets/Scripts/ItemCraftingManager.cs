using UnityEngine;
using System.Collections.Generic;

public class ItemCraftingManager : MonoBehaviour
{
    //Audio Manager CLass
    AudioManager audioManager;
    //Define a class to represent a combination
    [System.Serializable]
    public class ItemCombination
    {
        //Tag of the first item
        public string item1Tag;
        //Tag of the second item
        public string item2Tag;
        //Prefab of the resulting item
        public GameObject resultPrefab;
    }

    //List of possible combinations
    //Different items might have multiple combinations
    public List<ItemCombination> combinations = new List<ItemCombination>();

    private void OnCollisionEnter(Collision collision)
    {
        //Ensure only one object handles the logic by comparing instance IDs
        //This makes sure only one of the colliding objects executes the code
        if (gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
        {
            //Loop through all the combinations we defined for the object
            foreach (ItemCombination combination in combinations)
            {
                //Check if the colliding objects match this combination
                //Verify if the tags of the two objects match one of the combinations
                if ((gameObject.CompareTag(combination.item1Tag) && collision.gameObject.CompareTag(combination.item2Tag)) ||
                    (gameObject.CompareTag(combination.item2Tag) && collision.gameObject.CompareTag(combination.item1Tag)))
                {
                    //Audio Manager
                    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
                    //Plays SFX
                    audioManager.PlaySFX(audioManager.Success);
                    //Find the ObjectPickup script on the player
                    ObjectPickup objectPickup = FindObjectOfType<ObjectPickup>();

                    //If the item being held is one of the combining items, update its state
                    //Notify the ObjectPickup script that the player no longer has an item
                    //This is so we can pick up items again even if they combine in our arms
                    if (objectPickup != null)
                    {
                        if (objectPickup.GetCarriedObject() == gameObject || objectPickup.GetCarriedObject() == collision.gameObject)
                        {
                            //Clear the held item state
                            objectPickup.ClearCarriedObject();
                        }
                    }

                    //Get the collision point
                    //Finds where the two objects collided
                    Vector3 spawnPosition = collision.contacts[0].point;

                    //Spawn the resulting item
                    //Creates the new item at the collision point
                    Instantiate(combination.resultPrefab, spawnPosition, Quaternion.identity);

                    //Destroy both objects
                    //Removes the original objects from the game
                    Destroy(gameObject);
                    Destroy(collision.gameObject);

                    //Stop checking other combinations once the current one is done
                    return;
                }
            }
        }
    }
}
