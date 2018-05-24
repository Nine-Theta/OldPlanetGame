using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPPStats
{
    public float maxWaste = 30.0f;
    public float wasteGenPerTick = 0.03f;
    public float degradeRange = 3.0f;
    public float maxDurability = 200;
    public int Tier2UpgradeCost = 1;
    public int Tier3UpgradeCost = 1;
    public float repairPerTap = 5;
}

[System.Serializable]
public class SiloStats
{
    public float wasteStored = 0;
    public float wasteCapacity = 0;
    public float upgradeStorageMod = 100;
    public float recycleAmount = 30;
}

[System.Serializable]
public class FFPPStats
{
    public float pollutionPerTick = 0.1f;
    public float currentPollution = 0.0f;
    public bool startActive = false;
}

[System.Serializable]
public class CityStats
{
    public float startHappiness = 0.0f;
    public float maxHappiness = 10.0f;
    public float happinessPerTick = 0.00f;
    public float wastePenaltyPerBarrel = 0.01f;
    public float pollutionPenalty = 0.01f;

    public float researchHappinessMultiplier = 1.0f;
    public float researchHappinessThreshold = 5.0f;
    public int researchPointCap = 100;
    public float researchPointPerTick = 0.1f;
    public int recycleThreshold = 1;
}

public class LevelStatsScript : MonoBehaviour
{
    [SerializeField] private NPPStats NuclearPowerPlant;
    [SerializeField] private SiloStats Silo;
    [SerializeField] private FFPPStats FossilFuelPowerPlant;
    [SerializeField] private CityStats City;
    [HideInInspector] private static LevelStatsScript instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public static bool Exists
    { get { return instance != null; } }

    public static NPPStats NuclearPowerPlantStats
    { get { return instance.NuclearPowerPlant; } }


    public static SiloStats SiloStats
    { get { return instance.Silo; } }


    public static CityStats CityStats
    { get { return instance.City; } }


    public static FFPPStats FossilFuelPowerPlantStats
    { get { return instance.FossilFuelPowerPlant; } }
}
