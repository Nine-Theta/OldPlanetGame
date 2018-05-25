using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPPStats
{
    public float maxWaste = 30.0f;
    public float wasteGenPerTick = 0.03f;
    public float degradeRange = 0.3f;
    public float maxDurability = 200;
    public int UpgradeCost = 1;
    public float repairPerTap = 5;
}

[System.Serializable]
public class SiloStats
{
    public float wasteStored = 0;
    public float wasteCapacity = 1000000;
    public float upgradeStorageMod = 100;
    public float recycleAmount = 30;
}

[System.Serializable]
public class FFPPStats
{
    public float pollutionPerTick = 0.1f;
    public float currentPollution = 0.0f;
    public int upgradeCost = 1;
    public bool startActive = false;
}

[System.Serializable]
public class CityStats
{
    public float startHappiness = 0.0f;
    public float maxHappiness = 10.0f;
    public float happinessPerTick = 0.25f;
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
    [SerializeField] private NPPStats NuclearPowerPlantTier1;
    [SerializeField] private NPPStats NuclearPowerPlantTier2;
    [SerializeField] private NPPStats NuclearPowerPlantTier3;
    [SerializeField] private SiloStats Silo;
    [SerializeField] private FFPPStats FossilFuelPowerPlantTier1;
    [SerializeField] private FFPPStats FossilFuelPowerPlantTier2;
    [SerializeField] private FFPPStats FossilFuelPowerPlantTier3;
    [SerializeField] private CityStats City;
    private static LevelStatsScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    public static bool Exists
    { get { return instance != null; } }

    public static NPPStats NuclearPowerPlantStatsTier1
    { get { return instance.NuclearPowerPlantTier1; } }
    public static NPPStats NuclearPowerPlantStatsTier2
    { get { return instance.NuclearPowerPlantTier2; } }
    public static NPPStats NuclearPowerPlantStatsTier3
    { get { return instance.NuclearPowerPlantTier3; } }


    public static SiloStats SiloStats
    { get { return instance.Silo; } }


    public static CityStats CityStats
    { get { return instance.City; } }


    public static FFPPStats FossilFuelPowerPlantStatsTier1
    { get { return instance.FossilFuelPowerPlantTier1; } }
    public static FFPPStats FossilFuelPowerPlantStatsTier2
    { get { return instance.FossilFuelPowerPlantTier2; } }
    public static FFPPStats FossilFuelPowerPlantStatsTier3
    { get { return instance.FossilFuelPowerPlantTier3; } }
}
