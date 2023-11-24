using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAmmo : MonoBehaviour
{

    public GameObject explosionPrefab;
    public float explosionRadius = 5f;
    public float explosionForce = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Change "Fire1" to your input
        {
            Explosion();
        }
    }

   void Explosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

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

        // Destroy the projectile
        Destroy(gameObject);
    }
}
