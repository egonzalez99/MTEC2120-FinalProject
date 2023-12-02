using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChair : MonoBehaviour
{
    private bool isPlayerNear = false;
    private Animator playerAnimator;

    // Adjust this value to set the distance at which the player can interact with the chair.
    public float interactionDistance = 2f;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Change KeyCode.E to the key you want to use for interaction
        {
            InteractWithChair();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            playerAnimator = null;
        }
    }

    void InteractWithChair()
    {
        // Check if the player has an animator component
        if (playerAnimator != null)
        {
            // Trigger the sitting animation
            playerAnimator.SetTrigger("Sit");

            // Disable player movement or perform other actions during sitting animation
            // For example, you can disable the player controller script.
        }
    }
}
