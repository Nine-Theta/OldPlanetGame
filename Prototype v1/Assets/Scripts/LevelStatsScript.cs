﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPPStats
{
    public float maxWaste = 100.0f;
    public float wasteGenPerTick = 0.5f;
    public float degradeRange = 0.3f;
    public float maxDurability = 200;
    public float repairThreshold = 150;
    public float maintenanceAlertThreshold = 150;
    public int UpgradeCost = 20;
    public float repairPerTap = 10;
}

[System.Serializable]
public class SiloStats
{
    public float wasteStored = 0;
    public float wasteCapacity = 0;
    public float upgradeStorageMod = 100000;
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
    public float maxHappiness = 100.0f;
    public float happinessPerTick = 0.01f;
    public float wastePenaltyPerBarrel = 0.01f;
    public float pollutionPenalty = 0.1f;

    public float researchHappinessMultiplier = 1.0f;
    public float researchHappinessThreshold = 20.0f;
    public int researchPointCap = 100;
    public float researchPointPerTick = 0.1f;
    public int recycleThreshold = 1;
}

[System.Serializable]
public class DifficultyLevel
{
    public NPPStats NuclearPowerPlantTier1;
    public NPPStats NuclearPowerPlantTier2;
    public NPPStats NuclearPowerPlantTier3;
    public SiloStats Silo;
    public FFPPStats FossilFuelPowerPlantTier1;
    public FFPPStats FossilFuelPowerPlantTier2;
    public FFPPStats FossilFuelPowerPlantTier3;
    public CityStats City;
}

[System.Serializable]
public class Level
{
    public DifficultyLevel easy;
    public DifficultyLevel medium;
    public DifficultyLevel hard;
}

public class LevelStatsScript : MonoBehaviour
{
    [SerializeField] private Level level1;
    [SerializeField] private Level level2;
    [SerializeField] private Level level3;
    private int difficultyLevel = 2; //0 = undefined, 1 = easy, 2 = medium, 3 = hard
    private int level = 1; //0 = undefined, etc
    private int _mostRecentTierNPPAccessed = 1;
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

    public static int MostRecentTierNPPAccessed
    { get { return instance._mostRecentTierNPPAccessed; } }

    public static NPPStats NuclearPowerPlantStatsTier1
    {
        get
        {
            instance._mostRecentTierNPPAccessed = 1;
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.NuclearPowerPlantTier1;
                        case 2:
                            return instance.level1.medium.NuclearPowerPlantTier1;
                        case 3:
                            return instance.level1.hard.NuclearPowerPlantTier1;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.NuclearPowerPlantTier1;
                        case 2:
                            return instance.level2.medium.NuclearPowerPlantTier1;
                        case 3:
                            return instance.level2.hard.NuclearPowerPlantTier1;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.NuclearPowerPlantTier1;
                        case 2:
                            return instance.level3.medium.NuclearPowerPlantTier1;
                        case 3:
                            return instance.level3.hard.NuclearPowerPlantTier1;
                    }

            }
        }
    }
    public static NPPStats NuclearPowerPlantStatsTier2
    {
        get
        {
            instance._mostRecentTierNPPAccessed = 2;
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.NuclearPowerPlantTier2;
                        case 2:
                            return instance.level1.medium.NuclearPowerPlantTier2;
                        case 3:
                            return instance.level1.hard.NuclearPowerPlantTier2;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.NuclearPowerPlantTier2;
                        case 2:
                            return instance.level2.medium.NuclearPowerPlantTier2;
                        case 3:
                            return instance.level2.hard.NuclearPowerPlantTier2;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.NuclearPowerPlantTier2;
                        case 2:
                            return instance.level3.medium.NuclearPowerPlantTier2;
                        case 3:
                            return instance.level3.hard.NuclearPowerPlantTier2;
                    }

            }
        }
    }
    public static NPPStats NuclearPowerPlantStatsTier3
    {
        get
        {
            instance._mostRecentTierNPPAccessed = 3;
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.NuclearPowerPlantTier3;
                        case 2:
                            return instance.level1.medium.NuclearPowerPlantTier3;
                        case 3:
                            return instance.level1.hard.NuclearPowerPlantTier3;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.NuclearPowerPlantTier3;
                        case 2:
                            return instance.level2.medium.NuclearPowerPlantTier3;
                        case 3:
                            return instance.level2.hard.NuclearPowerPlantTier3;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.NuclearPowerPlantTier3;
                        case 2:
                            return instance.level3.medium.NuclearPowerPlantTier3;
                        case 3:
                            return instance.level3.hard.NuclearPowerPlantTier3;
                    }

            }
        }
    }


    public static SiloStats SiloStats
    { get { return instance.level1.easy.Silo; } }


    public static CityStats CityStats
    { get { return instance.level1.easy.City; } }


    public static FFPPStats FossilFuelPowerPlantStatsTier1
    {
        get
        {
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.FossilFuelPowerPlantTier1;
                        case 2:
                            return instance.level1.medium.FossilFuelPowerPlantTier1;
                        case 3:
                            return instance.level1.hard.FossilFuelPowerPlantTier1;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.FossilFuelPowerPlantTier1;
                        case 2:
                            return instance.level2.medium.FossilFuelPowerPlantTier1;
                        case 3:
                            return instance.level2.hard.FossilFuelPowerPlantTier1;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.FossilFuelPowerPlantTier1;
                        case 2:
                            return instance.level3.medium.FossilFuelPowerPlantTier1;
                        case 3:
                            return instance.level3.hard.FossilFuelPowerPlantTier1;
                    }

            }
        }
    }
    public static FFPPStats FossilFuelPowerPlantStatsTier2
    {
        get
        {
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.FossilFuelPowerPlantTier2;
                        case 2:
                            return instance.level1.medium.FossilFuelPowerPlantTier2;
                        case 3:
                            return instance.level1.hard.FossilFuelPowerPlantTier2;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.FossilFuelPowerPlantTier2;
                        case 2:
                            return instance.level2.medium.FossilFuelPowerPlantTier2;
                        case 3:
                            return instance.level2.hard.FossilFuelPowerPlantTier2;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.FossilFuelPowerPlantTier2;
                        case 2:
                            return instance.level3.medium.FossilFuelPowerPlantTier2;
                        case 3:
                            return instance.level3.hard.FossilFuelPowerPlantTier2;
                    }

            }
        }
    }
    public static FFPPStats FossilFuelPowerPlantStatsTier3
    {
        get
        {
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    goto case 1;
                case 1:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level1.easy.FossilFuelPowerPlantTier3;
                        case 2:
                            return instance.level1.medium.FossilFuelPowerPlantTier3;
                        case 3:
                            return instance.level1.hard.FossilFuelPowerPlantTier3;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.FossilFuelPowerPlantTier3;
                        case 2:
                            return instance.level2.medium.FossilFuelPowerPlantTier3;
                        case 3:
                            return instance.level2.hard.FossilFuelPowerPlantTier3;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.FossilFuelPowerPlantTier3;
                        case 2:
                            return instance.level3.medium.FossilFuelPowerPlantTier3;
                        case 3:
                            return instance.level3.hard.FossilFuelPowerPlantTier3;
                    }

            }
        }
    }

    public static int Level
    { get { return instance.level; } }

    public static int Difficulty
    { get { return instance.difficultyLevel; } }

    public static void SetLevel(int pLevel)
    {
        instance.level = pLevel;
    }

    public static void SetDifficulty(DifficultyMode pDifficulty)
    {
        instance.difficultyLevel = (int)(pDifficulty) + 1;
    }
}
