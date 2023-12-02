using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

public class InteractableDoor : Interactable
{
    private bool isClosed = true; // Initially closed
    public GameObject doorObject; // Reference to the door GameObject
    public float doorOpenAngle = 90.0f; // Angle to open the door
    public float doorCloseAngle = 0.0f; // Angle to close the door
    public float doorSpeed = 2.0f; // Speed at which the door opens/closes
    public Transform pivotPoint; // Reference to the pivot point
    public float interactionTime = 2.0f; 
    private Quaternion doorOpenRotation;
    private Quaternion doorCloseRotation;
    private bool isMoving = false;

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
        // Initialize door rotation angles
        doorOpenRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        doorCloseRotation = Quaternion.Euler(0, doorCloseAngle, 0);
    }

    //protected override void Interaction()
    //{
    //    base.Interaction();
    //    onInteract?.Invoke();


    //    Debug.Log("InteractableDoor : Interaction was called. ");
    //    // Check if the door is currently in motion
    //    if (isMoving)
    //        return;

    //    // Determine the target rotation based on the current state
    //    Quaternion targetRotation = isClosed ? doorOpenRotation : doorCloseRotation;
    //    isClosed = !isClosed; // Toggle the door state

    //    // Start a coroutine to smoothly rotate the door
    //    StartCoroutine(RotateDoor(targetRotation));
    //}


    protected override void Interaction(GameObject player)
    {
        base.Interaction(player);

        Debug.Log("InteractableDoor : Interaction was called. ");
        // Check if the door is currently in motion
        if (isMoving)
            return;

        // Determine the target rotation based on the current state
        Quaternion targetRotation = isClosed ? doorOpenRotation : doorCloseRotation;
        isClosed = !isClosed; // Toggle the door state

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


        // Start a coroutine to smoothly rotate the door
        StartCoroutine(RotateDoor(targetRotation));
    }



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
                animator.applyRootMotion = true;

                animator.runtimeAnimatorController = interactableAnimController;
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


    private IEnumerator RotateDoor(Quaternion targetRotation)
    {

        Debug.Log("Rotating Door " + doorObject.transform.rotation);
        isMoving = true;

        while (Quaternion.Angle(doorObject.transform.rotation, targetRotation) > 0.01f)
        {
            // Smoothly rotate the door towards the target rotation
            //doorObject.transform.rotation = Quaternion.Slerp(doorObject.transform.rotation, targetRotation, Time.deltaTime * doorSpeed);


            // Adjust the pivot point's position to maintain the door's pivot
            //Vector3 pivotOffset = pivotPoint.position - doorObject.transform.position;
            //pivotOffset = Quaternion.Euler(0, Time.deltaTime * doorSpeed, 0) * pivotOffset;
            //pivotPoint.position = doorObject.transform.position + pivotOffset;
            doorObject.transform.RotateAround(pivotPoint.position, Vector3.up, Time.deltaTime * doorSpeed);

            yield return null;
        }

        // Ensure the door reaches the exact target rotation
        doorObject.transform.rotation = targetRotation;

        isMoving = false;
    }
}
