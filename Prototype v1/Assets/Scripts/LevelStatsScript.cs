using System.Collections;
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
    public float decayingBarrelPenalty = 1.0f;
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
    //public SiloStats Silo;
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
    private int level = 0; //0 = undefined, etc
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
            if (LeaderboardTracker.Exists)
                SetDifficulty(LeaderboardTracker.Instance.CurrentPlayer.Difficulty);
        }
    }

    public static bool Exists
    { get { return instance != null; } }


    public static NPPStats NuclearPowerPlantStatsTier1
    {
        get
        {
            switch (instance.level)
            {
                default:
                    Debug.Log("instance.level is not 1, 2 or 3, falling through to case 1");
                    Debug.Log(instance.level);
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


    //public static SiloStats SiloStats
    //{ get { return instance.level1.easy.Silo; } }


    public static CityStats CityStats
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
                            return instance.level1.easy.City;
                        case 2:
                            return instance.level1.medium.City;
                        case 3:
                            return instance.level1.hard.City;
                    }
                case 2:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level2.easy.City;
                        case 2:
                            return instance.level2.medium.City;
                        case 3:
                            return instance.level2.hard.City;
                    }
                case 3:
                    switch (instance.difficultyLevel)
                    {
                        default:
                            Debug.Log("instance.Difficultylevel is not 1, 2 or 3, falling through to case 1");
                            goto case 1;
                        case 1:
                            return instance.level3.easy.City;
                        case 2:
                            return instance.level3.medium.City;
                        case 3:
                            return instance.level3.hard.City;
                    }

            }
        }
    }


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

    public void LevelUp()
    {
        instance.level++;
        bool LEVELONEFIX = false;

        PowerPlantScript[] NPPS = (PowerPlantScript[])(Resources.FindObjectsOfTypeAll(typeof(PowerPlantScript)));
        EndConditionScript.NPPCount = 0;
        foreach (PowerPlantScript NPP in NPPS)
        {
            if (LEVELONEFIX)
            {
                break;
            }
            NPP.enabled = (NPP.PartOfLevel == instance.level);
            if (NPP.enabled)
            {
                if (instance.level == 1)
                    LEVELONEFIX = true;
                Debug.Log("Level" + instance.level + ", " + NPP.PartOfLevel);
                EndConditionScript.NPPCount++;
            }
        }
        SiloScript[] silos = (SiloScript[])(Resources.FindObjectsOfTypeAll(typeof(SiloScript)));
        foreach (SiloScript silo in silos)
        {
            silo.enabled = (silo.PartOfLevel == instance.level);
        }
        FFPPScript[] FFPPS = (FFPPScript[])(Resources.FindObjectsOfTypeAll(typeof(FFPPScript)));
        foreach (FFPPScript FFPP in FFPPS)
        {
            FFPP.enabled = (FFPP.PartOfLevel == instance.level);
        }
        CityScript[] cities = (CityScript[])(Resources.FindObjectsOfTypeAll(typeof(CityScript)));
        foreach (CityScript city in cities)
        {
            city.enabled = (city.PartOfLevel == instance.level);
        }
        //Debug.Log(EndConditionScript.NPPCount);
    }

    public static void SetDifficulty(DifficultyMode pDifficulty)
    {
        instance.difficultyLevel = (int)(pDifficulty) + 1;
    }
}
