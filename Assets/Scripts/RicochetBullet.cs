using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBullet : MonoBehaviour
{
    public float bulletSpeed = 200f;
    public int maxRicochets = 30;
    public GameObject bulletPrefab; // Reference to the ricochet bullet prefab

    private int ricochetCount = 0;

    void Start()
    {
        // Set initial velocity in the forward direction
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet has reached the maximum number of ricochets
        if (ricochetCount >= maxRicochets)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the reflection direction based on the surface normal
        Vector3 reflectedDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

        // Update the bullet's velocity to the reflected direction
        GetComponent<Rigidbody>().velocity = reflectedDirection * bulletSpeed;

        // Increment the ricochet count
        ricochetCount++;

        // Instantiate a new ricochet bullet prefab
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

}
