using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    public float throwForce = 1000f;
    public float raycastRange = 20f;

    public GameObject forceFieldPrefab;
    private GameObject forceFieldInstance;

    public GameObject forcePrefab;

    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        // Apply force
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        // apply force field
        forceFieldInstance = Instantiate(forceFieldPrefab, transform.position, Quaternion.identity);
        forceFieldInstance.SetActive(true);
        forceFieldInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check for player input
        if (Input.GetMouseButtonDown(0))
        {
            ExplodeRaycast();
        }
    }

    void ExplodeRaycast()
    {
        if (!hasExploded)
        {
            // raycast to detect objects in front
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastRange))
            {
                // instantiate explosion effect at the hit
                Instantiate(forcePrefab, hit.point, Quaternion.identity);

                forceFieldInstance.SetActive(true);

                // apply force to the hit object
                Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
                if (hitRb != null)
                {
                    // adjust the force value as needed
                    hitRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }


            }

            // mark to prevent multiple explosions
            hasExploded = true;

            // disable rendering and collision but keep the gameObject 
            Renderer renderer = GetComponent<Renderer>();
            Collider collider = GetComponent<Collider>();

            if (renderer != null)
            {
                renderer.enabled = false;
            }

            if (collider != null)
            {
                collider.enabled = false;
            }

            // Respawnafter delay
            Invoke("RespawnForce", 1f);
        }
    }

    void RespawnForce()
    {
        hasExploded = false;
    }
}
