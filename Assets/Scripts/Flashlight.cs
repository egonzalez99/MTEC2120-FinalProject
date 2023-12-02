using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.F; // Change the key as needed
    public float lightIntensity = 5f;

    private Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.intensity = 0f;
    }

    void Update()
    {
        // Toggle the flashlight on/off with the specified key
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        // Toggle the flashlight intensity
        flashlight.intensity = (flashlight.intensity == 0f) ? lightIntensity : 0f;
    }
}
