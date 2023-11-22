using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUn : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform gunBarrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            GameObject go = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(gunBarrel.forward * 10000.0f);
        }
    }
}
