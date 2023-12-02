using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Shoot in the direction the player is facing
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bullet hit " + other.gameObject.name);
        Destroy(gameObject, 2f);
    }
}