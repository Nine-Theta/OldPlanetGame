using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    float _maxIntensity = 7.0f;
    float _happiness = 0.0f;
    List<Light> cityLights = new List<Light>();

    [SerializeField] private float startHappiness = 0.0f;
    [SerializeField] private float maxHappiness = 10.0f;
    [SerializeField] private float happinessPerTick = 0.00f;
    [SerializeField] private float wastePenaltyPerBarrel = 0.01f;
    [SerializeField] private float pollutionPenalty = 0.01f;

    [SerializeField] private float researchHappinessMultiplier = 1.0f;

    [SerializeField] private float researchHappinessThreshold = 5.0f;
    private float researchPointsGained;
    private int researchPointsSpend;
    [SerializeField] private int researchPointCap;
    [SerializeField] private float researchPointPerTick;
    [SerializeField] private int recycleThreshold;

    [SerializeField] private CustomEvent OnResearchpointUp;
    [SerializeField] private CustomEvent OnHappinessUp;
    [SerializeField] private CustomEvent OnResearchThresholdReached;

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
        GetComponentsInChildren<Light>(cityLights);
        _happiness = startHappiness;

        if(LevelStatsScript.Exists)
        {
            SetVariables(LevelStatsScript.CityStats);
        }
    }

    void Update()
    {
        ChangeHappiness();
        Research();
        UpdateDebugInfo();
    }

    public void IncreaseLightIntensity(float value)
    {
        for (int i = 0; i < cityLights.Count; i++)
        {
            Light cityLight = cityLights[i];
            cityLight.intensity += value;
            if (cityLight.intensity > _maxIntensity)
                cityLight.intensity = _maxIntensity;
        }
    }

    public void DecreaseLightIntensity(float value)
    {
        for (int i = 0; i < cityLights.Count; i++)
        {
            Light cityLight = cityLights[i];
            cityLight.intensity -= value;
            if (cityLight.intensity < 0)
                cityLight.intensity = 0;
        }
    }

    private void SetVariables(CityStats stats)
    {
        startHappiness = stats.startHappiness;
        maxHappiness = stats.maxHappiness;
        happinessPerTick = stats.happinessPerTick;
        wastePenaltyPerBarrel = stats.wastePenaltyPerBarrel;
        pollutionPenalty = stats.pollutionPenalty;

        researchHappinessMultiplier = stats.researchHappinessMultiplier;
        researchHappinessThreshold = stats.researchHappinessThreshold;
        researchPointCap = stats.researchPointCap;
        researchPointPerTick = stats.researchPointPerTick;
        recycleThreshold = stats.recycleThreshold;
    }

    private void Research()
    {
        if (_happiness >= researchHappinessThreshold && researchPointsGained < researchPointCap)
        {
            int oldPoints = ResearchPoints;
            researchPointsGained += researchPointPerTick;
            if (oldPoints < ResearchPoints)
            {
                OnResearchpointUp.Invoke();
                if (ResearchPoints == researchHappinessThreshold)
                {
                    OnResearchThresholdReached.Invoke();
                }
            }
        }
    }

    private void UpdateDebugInfo()
    {
        debugResearchText.text = ResearchPoints.ToString();
        debugHappyText.text = Mathf.FloorToInt(_happiness).ToString();
    }

    void ChangeHappiness()
    {
        int oldHappiness = Mathf.FloorToInt(_happiness);
        _happiness += happinessPerTick - (wastePenaltyPerBarrel * BarrelScript.GetBarrelCount()) - (pollutionPenalty * FFPPScript.Pollution);
        if (_happiness < 0)
            _happiness = 0;
        if (ResearchPoints >= recycleThreshold)
            recycleButton.enabled = true;
        else
            recycleButton.enabled = false;
        if (_happiness >= maxHappiness)
            _happiness = maxHappiness;
        if (oldHappiness < Mathf.FloorToInt(_happiness))
        {
            OnHappinessUp.Invoke();
        }
    }

    public void RespondToUpgrade()
    {
        happinessPerTick += 0.25f;
    }

    public void RespondToBreakdown()
    {
        for (int i = 0; i < cityLights.Count; i++)
        {
            Light cityLight = cityLights[i];
            cityLight.color = Color.black;
            happinessPerTick *= -1;
        }
    }

    public void RespondToRepairs()
    {
        for (int i = 0; i < cityLights.Count; i++)
        {
            Light cityLight = cityLights[i];
            cityLight.color = Color.white;
            happinessPerTick *= -1;
        }
    }

    /// <summary>
    /// Returns true if upgrade successful
    /// </summary>
    /// <param name="points"></param>
    /// <returns>True if succesful, false if insufficient points</returns>
    public bool SpendResearch(int points)
    {
        if (ResearchPoints >= points)
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
