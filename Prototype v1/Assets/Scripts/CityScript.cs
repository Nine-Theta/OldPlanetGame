using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    float _maxIntensity = 7.0f;
    float _happiness = 0.0f;
    Light cityLight;

    public float startHappiness = 0.0f;
    public float happinessPerTick = 0.00f;
    public float wasteHappinessPenaltyPerTick = 0.01f;
    public Text debugHappyText;

    void Start()
    {
        cityLight = GetComponentInChildren<Light>();
        _happiness = startHappiness;
    }

    void Update()
    {
        ChangeHappiness();
        
        UpdateDebugInfo();
    }

    public void IncreaseLightIntensity(float value)
    {
        cityLight.intensity += value;
        if (cityLight.intensity > _maxIntensity)
            cityLight.intensity = _maxIntensity;
    }

    void DecreaseLightIntensity(float value)
    {
        cityLight.intensity -= value;
        if (cityLight.intensity < 0)
            cityLight.intensity = 0;
    }

    void UpdateDebugInfo()
    {
        debugHappyText.text = _happiness.ToString("F2");
    }

    void ChangeHappiness()
    {
        _happiness += happinessPerTick - (wasteHappinessPenaltyPerTick * BarrelScript.GetBarrelCount());
        if (_happiness >= 10)
            _happiness = 10;
    }

    public void RespondToUpgrade()
    {
        happinessPerTick += 0.25f;
    }

    public void RespondToBreakdown()
    {
        cityLight.color = Color.black;
        happinessPerTick *= -1;
    }

    public void RespondToRepairs()
    {
        cityLight.color = Color.white;
        happinessPerTick *= -1;
    }
}
