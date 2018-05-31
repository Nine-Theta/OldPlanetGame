using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{
    float _happiness = 0.0f;

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
    [SerializeField] private CustomEvent OnUpgradeAvailable;

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
        _happiness = startHappiness;

        if (LevelStatsScript.Exists)
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
        if (_happiness >= researchHappinessThreshold && ResearchPoints < researchPointCap)
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
                switch (LevelStatsScript.MostRecentTierNPPAccessed)
                {
                    default:
                    case 1:
                        if (ResearchPoints >= LevelStatsScript.NuclearPowerPlantStatsTier1.UpgradeCost)
                        {
                            OnUpgradeAvailable.Invoke();
                        }
                        break;
                    case 2:
                        if (ResearchPoints >= LevelStatsScript.NuclearPowerPlantStatsTier2.UpgradeCost)
                        {
                            OnUpgradeAvailable.Invoke();
                        }
                        break;
                    case 3:
                        if (ResearchPoints >= LevelStatsScript.NuclearPowerPlantStatsTier3.UpgradeCost)
                        {
                            OnUpgradeAvailable.Invoke();
                        }
                        break;
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
        _happiness += (happinessPerTick) - (wastePenaltyPerBarrel * BarrelScript.GetBarrelCount()) - (pollutionPenalty * FFPPScript.Pollution);
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


    public bool EndConditionMet()
    {
        return (researchPointsGained >= researchPointCap);
    }

    public void CheckParticleThreshold50(ParticleSystem pSystem)
    {
        if (_happiness >= 50.0f)
        {
            pSystem.Play();
        }
    }
}
