using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    float _maxIntensity = 7.0f;
    float _happiness = 0.0f;
    Light cityLight;

    [SerializeField] private float startHappiness = 0.0f;
    [SerializeField] private float happinessPerTick = 0.00f;
    [SerializeField] private float wasteHappinessPenaltyPerTick = 0.01f;

    [SerializeField] private float researchHappinessThreshold = 5.0f;
    private float researchPointsGained;
    private int researchPointsSpend;
    [SerializeField] private int researchPointCap;
    [SerializeField] private float researchPointPerTick;
    [SerializeField] private int recycleThreshold;

    [SerializeField] private Button recycleButton;
    [SerializeField] private Text debugResearchText;
    [SerializeField] private Text debugHappyText;

    public int ResearchPoints
    {
        get
        {
            return Mathf.FloorToInt(researchPointsGained) - researchPointsSpend;
        }
    }

    void Start()
    {
        cityLight = GetComponentInChildren<Light>();
        _happiness = startHappiness;
    }

    void Update()
    {
        ChangeHappiness();
        Research();
        UpdateDebugInfo();
    }

    private void IncreaseLightIntensity(float value)
    {
        cityLight.intensity += value;
        if (cityLight.intensity > _maxIntensity)
            cityLight.intensity = _maxIntensity;
    }

    private void DecreaseLightIntensity(float value)
    {
        cityLight.intensity -= value;
        if (cityLight.intensity < 0)
            cityLight.intensity = 0;
    }

    private void Research()
    {
        if(_happiness >= researchHappinessThreshold && researchPointsGained < researchPointCap)
        {
            researchPointsGained += researchPointPerTick;
        }
    }

    private void UpdateDebugInfo()
    {
        debugResearchText.text = ResearchPoints.ToString();
        debugHappyText.text = _happiness.ToString();
    }

    void ChangeHappiness()
    {
        _happiness += happinessPerTick - (wasteHappinessPenaltyPerTick * BarrelScript.GetBarrelCount());
        if (ResearchPoints >= recycleThreshold)
            recycleButton.enabled = true;
        else
            recycleButton.enabled = false;
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

    /// <summary>
    /// Returns true if upgrade successful
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public bool SpendResearch(int points)
    {
        if(ResearchPoints >= points)
        {
            researchPointsSpend += points;
            return true;
        }
        else
        {
            return false;
        }
    }
}
