using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightingScript : MonoBehaviour
{
    Light cityLight;

    void Start()
    {
        cityLight = GetComponentInChildren<Light>();
    }

    void Update()
    {

    }

    public void IncreaseLightIntensity(float value)
    {
        cityLight.intensity += value;
    }
}
