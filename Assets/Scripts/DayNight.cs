using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light directionalLight;  // Reference to your directional light
    public Color dayColor = Color.yellow;  // Color for daytime
    public Color nightColor = Color.blue;  // Color for nighttime
    public float dayIntensity = 0.30f;  // Intensity for daytime
    public float nightIntensity = 0.30f;  // Intensity for nighttime

    private bool isDay = true;

    void Start()
    {
        // Assuming your directional light is the one that should be modified
        if (directionalLight == null)
        {
            // If not set in the Inspector, try to find the directional light in the scene
            directionalLight = GameObject.FindObjectOfType<Light>();
        }

        // Set initial lighting conditions
        SetDayNight(isDay);
    }

    void Update()
    {
        // Check for 'Q' key press to toggle day-night
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleDayNight();
        }
    }

    public void ToggleDayNight()
    {
        // Toggle between day and night
        isDay = !isDay;
        SetDayNight(isDay);
    }

    void SetDayNight(bool isDay)
    {
        // Set the light color and intensity based on the time of day
        if (isDay)
        {
            directionalLight.color = dayColor;
            directionalLight.intensity = dayIntensity;
        }
        else
        {
            directionalLight.color = nightColor;
            directionalLight.intensity = nightIntensity;
        }
    }
}
