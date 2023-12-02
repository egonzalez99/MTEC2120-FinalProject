using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    [SerializeField] float maxInteractingDistance = 10;
    [SerializeField] float interactingRadius = 1;

    LayerMask layerMask;
    //Transform cameraTransform;
    InputAction interactAction;

    //For Gizmo
    Vector3 origin;
    Vector3 direction;
    Vector3 hitPosition;
    float hitDistance;

    public Interactable interactableTarget;

    // Start is called before the first frame update
    void Start()
    {
        //cameraTransform = Camera.main.transform;
        layerMask = LayerMask.GetMask("Interactable", "Enemy", "NPC");

        interactAction = GetComponent<PlayerInput>().actions["Interact"];
        interactAction.performed += Interact;
    }
    // Update is called once per frame
    void Update()
    {
        direction = transform.TransformDirection(Vector3.forward);
        origin = transform.position;
        RaycastHit hit;

        //if (Physics.SphereCast(origin, interactingRadius, direction, out hit, maxInteractingDistance, layerMask))
        if (Physics.Raycast(origin, direction, out hit, maxInteractingDistance, layerMask))
        {

            Debug.Log("Raycast hit.");
            hitPosition = hit.point;
            hitDistance = hit.distance;
            if (hit.transform.TryGetComponent<Interactable>(out interactableTarget))
            {
                Debug.Log("TargetOn.");

                interactableTarget.TargetOn();
            }
        }
        else
        {
            if (interactableTarget)
            {
                interactableTarget.TargetOff();
                interactableTarget = null;
            }

        }
    }
    private void Interact(InputAction.CallbackContext obj)
    {

        //Debug.Log("Interact was called. " + interactableTarget.name);
        if (interactableTarget != null)
        {
            if (Vector3.Distance(transform.position, interactableTarget.transform.position) <= interactableTarget.interactionDistance)
            {
                // From this
                interactableTarget.Interact();

                // To this
                interactableTarget.Interact(this.gameObject);
            }
        }
        else
        {
            print("nothing to interact!");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + direction * hitDistance);
        Gizmos.DrawWireSphere(hitPosition, interactingRadius);
    }
    private void OnDestroy()
    {
        interactAction.performed -= Interact;
    }
}
