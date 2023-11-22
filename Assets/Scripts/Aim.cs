using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField]

    private GameObject gun;

    [SerializeField]

    private GameObject playerHead;

    private float rotationSpeed = 10.0f;
    private void Update()
    {
        //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //gun.transform.LookAt(target);

        Vector3 direction = playerHead.transform.position - gun.transform.position;

        // calculate the rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // create a rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // the gun will roatate towards the player
        gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
