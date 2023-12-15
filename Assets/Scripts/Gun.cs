using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    public float throwForce = 1000f;
    public float raycastRange = 20f;

    public GameObject bulletPrefab;

    public GameObject ricochetBulletPrefab;
    public float fireCooldown = 0.3f; // Cooldown time in seconds

    private bool canFire = true;
    private bool isFiring = false;

    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        // Apply force
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // check for player input
        if (Input.GetMouseButtonDown(0))
        {
            ExplodeRaycast();
        }

        if (Input.GetMouseButtonDown(1) && canFire)
        {
            isFiring = true;
            Ricochet();
        }

        if (isFiring)
        {
            // Handle firing cooldown
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0)
            {
                canFire = true;
                fireCooldown = 1.0f; // Reset cooldown
                isFiring = false;
            }
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
                GameObject newBullet = Instantiate(bulletPrefab, hit.point, Quaternion.identity);

                // Set the initial velocity to move in the forward direction
                newBullet.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;

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
            Invoke("RespawnForce", 0.1f);
        }
    }

    void RespawnForce()
    {
        hasExploded = false;
    }
    void Ricochet()
    {
        if (!hasExploded)
        {
            Instantiate(ricochetBulletPrefab, transform.position, Quaternion.identity);

            // Set to false after the ricochet
            canFire = false;
        }
    }
}
