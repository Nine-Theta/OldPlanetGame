using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    float maxIntensity = 7.0f;
    float _happiness = 0.0f;
    Light cityLight;

    public float startHappiness = 0.0f;
    public float happinessPerTick = 0.00f;
    public Text debugHappyText;

    void Start()
    {
        cityLight = GetComponentInChildren<Light>();
        _happiness = startHappiness;
    }

    void Update()
    {
        if(cityLight.intensity >= 5.0f)
        {
            IncreaseHappiness();
        }
        UpdateDebugInfo();
    }

    public void IncreaseLightIntensity(float value)
    {
        cityLight.intensity += value;
        if (cityLight.intensity > maxIntensity)
            cityLight.intensity = maxIntensity;
    }

    void DecreaseLightIntensity(float value)
    {
        cityLight.intensity -= value;
        if (cityLight.intensity < 0)
            cityLight.intensity = 0;
    }

    void UpdateDebugInfo()
    {
        debugHappyText.text = Mathf.Floor(_happiness).ToString();
    }

    void IncreaseHappiness()
    {
        _happiness += happinessPerTick;
        if (_happiness >= 100)
            _happiness = 100;
    }

    public void RespondToUpgrade()
    {
        happinessPerTick += 0.05f;
    }

    public void RespondToBreakdown()
    {
        cityLight.color = Color.black;
    }

    public void RespondToRepairs()
    {
        cityLight.color = Color.white;
    }
}
