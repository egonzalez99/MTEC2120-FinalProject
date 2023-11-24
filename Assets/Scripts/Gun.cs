using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    public float throwForce = 10f;
    public float explosionRadius = 5f;
    public float explosionForce = 10f;

    public GameObject forceFieldPrefab;
    private GameObject forceFieldInstance;

    public GameObject explosionPrefab;

    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        // Apply initial force
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        //Apply force field
        forceFieldInstance = Instantiate(forceFieldPrefab, transform.position, Quaternion.identity);
        forceFieldInstance.SetActive(true);
        forceFieldInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input to manually explode the grenade
        if (Input.GetMouseButtonDown(0))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (!hasExploded)
        {
            // Instantiate explosion effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            forceFieldInstance.SetActive(true);

            // Apply force to nearby objects within the explosion radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider hitCollider in colliders)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Adjust the force value as needed
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                // Implement your logic here, e.g., damage the object, apply force, etc.
            }

            // Mark the grenade as exploded to prevent multiple explosions
            hasExploded = true;

            // Disable rendering and collision but keep the GameObject in the scene
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

            // Respawn the grenade after a delay
            Invoke("RespawnForce", 3f);
        }
    }

    void RespawnForce()
    {
        // Reset the grenade properties
        hasExploded = false;

    }
}
