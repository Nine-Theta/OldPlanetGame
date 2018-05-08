using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightingScript : MonoBehaviour
{
    float maxIntensity = 7.0f;
    float _happiness = 0.0f;
    Light cityLight;

    public float startHappiness = 1.0f;

    void Start()
    {
        cityLight = GetComponentInChildren<Light>();
        _happiness = startHappiness;
    }

    void Update()
    {

    }

    public void IncreaseLightIntensity(float value)
    {
        cityLight.intensity += value;
        if (cityLight.intensity > maxIntensity)
            cityLight.intensity = maxIntensity;
    }
}
