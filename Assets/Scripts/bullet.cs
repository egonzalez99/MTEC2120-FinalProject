using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    private float bSpeed = 10.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        rb.velocity = transform.forward * bSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("bullet hit" + other.gameObject.name);
        Destroy(gameObject, 2f);
    }
}