using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFPPScript : MonoBehaviour
{
    [SerializeField] private float pollutionPerTick = 0.1f;
    [SerializeField] private static float currentPollution = 0.0f;
    [SerializeField] private bool startActive = false;
    [SerializeField] private CustomEvent OnPollutionPercentageUp;
    [SerializeField] private CustomEvent OnPollutionPercentageDown;
    private bool isActive = false;

    void Start()
    {
        isActive = startActive;
        if (LevelStatsScript.Exists)
        {
            SetVariables(LevelStatsScript.FossilFuelPowerPlantStats);
        }
    }

    void Update()
    {
        if (isActive)
        {
            int oldPollution = Mathf.FloorToInt(currentPollution);
            currentPollution += pollutionPerTick;
            if (currentPollution >= 100)
            {
                currentPollution = 100;
            }
            if (oldPollution < Mathf.FloorToInt(currentPollution))
            {
                OnPollutionPercentageUp.Invoke();
            }
        }
        else
        {
            int oldPollution = Mathf.FloorToInt(currentPollution);
            currentPollution -= pollutionPerTick;
            if (currentPollution < 0)
            {
                currentPollution = 0;
            }
            if (oldPollution > Mathf.FloorToInt(currentPollution))
            {
                OnPollutionPercentageDown.Invoke();
            }
        }
    }

    private void SetVariables(FFPPStats stats)
    {
        pollutionPerTick = stats.pollutionPerTick;
        currentPollution = stats.currentPollution;
        startActive = stats.startActive;
    }

    public static float pollution
    { get { return currentPollution; } }

    public void SetPlantActive(bool value)
    {
        isActive = value;
    }
}
