using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour
{

    #region PartOfLevels
    [SerializeField] private int _partOfLevel = 1;
    public int PartOfLevel
    { get { return _partOfLevel; } }
    #endregion

    float _happiness = 0.0f;

    private float startHappiness = 0.0f;
    private float maxHappiness = 10.0f;
    private float happinessPerTick = 0.00f;
    private float wastePenaltyPerBarrel = 0.01f;
    private float decayingBarrelPenalty = 1.0f;
    private float pollutionPenalty = 0.01f;

    private float researchHappinessMultiplier = 1.0f;

    private float researchHappinessThreshold = 5.0f;
    private float researchPointsGained;
    private int researchPointsSpend;
    private int researchPointCap;
    private float researchPointPerTick;
    private int recycleThreshold;

    private int currentUpgradeCost = 20;


    [SerializeField] private ParticleSystem particleSystem1;
    [SerializeField] private ParticleSystem particleSystem2;
    [SerializeField] private ParticleSystem particleSystem3;

    [SerializeField] private ParticleSystem sadParticleSystem;

    [SerializeField] private float particle1Threshold = 0;
    [SerializeField] private float particle2Threshold = 30;
    [SerializeField] private float particle3Threshold = 70;

    [SerializeField] private CustomEvent OnResearchpointUp;
    [SerializeField] private CustomEvent OnHappinessUp;
    [SerializeField] private CustomEvent OnHappinessDown;
    [SerializeField] private CustomEvent OnResearchThresholdReached;
    [SerializeField] private CustomEvent OnUpgradeAvailable;
    [SerializeField] private CustomEvent OnResearchSpend;

    [SerializeField] private Text debugResearchText;
    [SerializeField] private Text debugHappyText;

    public void SetUpgradeCost(int value)
    {
        currentUpgradeCost = value;
    }

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
        decayingBarrelPenalty = stats.decayingBarrelPenalty;
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
                if (ResearchPoints >= currentUpgradeCost)
                {
                    OnUpgradeAvailable.Invoke();
                }
            }
        }
    }

    private void UpdateDebugInfo()
    {
        if (debugResearchText != null)
            debugResearchText.text = ResearchPoints.ToString();
        if (debugHappyText != null)
            debugHappyText.text = Mathf.FloorToInt(_happiness).ToString();
    }

    void ChangeHappiness()
    {
        int oldHappiness = Mathf.FloorToInt(_happiness);
        _happiness += (happinessPerTick) - (wastePenaltyPerBarrel * BarrelScript.GetBarrelCount()) - (decayingBarrelPenalty * BarrelScript.GetDecayingBarrelCount()) - (pollutionPenalty * FFPPScript.Pollution);
        //Debug.Log("_happiness: "+ _happiness + ", happinessPerTick: " + happinessPerTick + ", BarrelReduction: " + (wastePenaltyPerBarrel * BarrelScript.GetBarrelCount()) + ", PollutionPenalty: " + (pollutionPenalty * FFPPScript.Pollution));
        if (_happiness < 0)
            _happiness = 0;
        if (_happiness >= maxHappiness)
            _happiness = maxHappiness;
        if (oldHappiness < Mathf.FloorToInt(_happiness))
        {
            OnHappinessUp.Invoke();
        }
        if (oldHappiness > Mathf.FloorToInt(_happiness))
        {
            OnHappinessDown.Invoke();
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
            OnResearchSpend.Invoke();
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

    public void CheckParticleThresholds()
    {
        if (_happiness >= particle1Threshold && _happiness < particle2Threshold) //particle 1
        {
            particleSystem1.Play();
        }
        else if (_happiness < particle3Threshold) // Particle 2
        {
            particleSystem2.Play();
        }
        else // particle 3
        {
            particleSystem3.Play();
        }
    }

    public void CheckSadParticleThreshold()
    {
        if (_happiness <= 0.0f)
        {
            sadParticleSystem.Play();
        }
    }
}
