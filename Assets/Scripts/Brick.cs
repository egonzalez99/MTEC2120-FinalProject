using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    public delegate void OnInitializePhysics();
    public static event OnInitializePhysics onInitializePHysics;

    private float startTime;


    private bool isRestored = false; 

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        BrickController.onBrickRestore += Restore;
        Brick.onInitializePHysics += InitializePhysics; 
    }

    private void InitializePhysics()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false ; 
    }

    public void Restore()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

    }


    
    private void OnCollisionEnter(Collision other)
    {



        if (other.gameObject.CompareTag("Bullet"))
        {

            Debug.Log("Collsion entered. " + other.gameObject.name);

            onInitializePHysics?.Invoke(); 

        }
    }


    // Update is called once per frame
    void Update()
    {
        float timeElapsed= (Time.time - startTime);
        float duration = 10.0f;

        float fraction = timeElapsed / duration;

        if(timeElapsed < duration)
        {
            Debug.Log("Fraction of time is " + fraction);

            transform.position = Vector3.Lerp(transform.position, initialPosition, fraction);
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, fraction);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            isRestored = true; 
        }
        else
        {
            isRestored = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
        }

    }
}
