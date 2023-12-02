using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

public class InteractableBed : Interactable
{
    public GameObject BedObject; // Reference to the Bed GameObject
    public float interactionTime = 2.0f;


    private RuntimeAnimatorController originalAnimController;
    public RuntimeAnimatorController interactableAnimController;
    private Animator animator;

    public GameObject Marker;
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;

    public UnityEvent onInteract;


    public override void Start()
    {
        base.Start();
        // Initialize Bed rotation angles

    }




    protected override void Interaction(GameObject player)
    {
        base.Interaction(player);

        Debug.Log("InteractableBed : Interaction was called. ");


        // Move Player to Marker
        // Move the player to the Marker GameObject
        // Get the Animator component attached to this GameObject
        animator = player.GetComponent<Animator>();
        originalAnimController = player.GetComponent<Animator>().runtimeAnimatorController;
        characterController = player.GetComponent<CharacterController>();
        thirdPersonController = player.GetComponent<ThirdPersonController>();

        player.transform.position = Marker.transform.position;
        player.transform.rotation = Marker.transform.rotation;
        characterController.enabled = false;
        thirdPersonController.enabled = false;



        // Swap Animations
        StartCoroutine(SwapAnimation());


    }

    bool isSleeping = false; 

    private IEnumerator SwapAnimation()
    {


        // Ensure we have an Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
        else
        {
            // Assign the initial RuntimeAnimatorController
            if (interactableAnimController != null)
            {
                if(!isSleeping)
                {
                    animator.applyRootMotion = true;

                    animator.runtimeAnimatorController = interactableAnimController;

                    animator.SetTrigger("SleepEnter");
                    isSleeping = true;
                }
                else
                {
                    animator.applyRootMotion = true;

                    animator.runtimeAnimatorController = interactableAnimController;

                    animator.SetTrigger("SleepExit");
                    isSleeping = false;
                }

            }

            yield return new WaitForSeconds(interactionTime);

            if (originalAnimController != null)
            {
                animator.applyRootMotion = false;

                animator.runtimeAnimatorController = originalAnimController;
            }


            //else
            //{
            //    Debug.LogError("RuntimeAnimatorController not assigned!");
            //}
            OnAnimationComplete();
        }

        yield return null;
    }


    public void OnAnimationComplete()
    {
        // This method is called from an Animation Event when the default animation clip finishes playing

        // Swap to the other controller and play its default animation
        characterController.enabled = true;
        thirdPersonController.enabled = true;
    }


 
}
