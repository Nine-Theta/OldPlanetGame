using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFPPScript : MonoBehaviour
{
    #region PartOfLevels
    [SerializeField] private int _partOfLevel = 1;
    public int PartOfLevel
    { get { return _partOfLevel; } }
    #endregion
    private float pollutionPerTick = 0.1f;
    private static float currentPollution = 0.0f;
    private bool startActive = false;
    [SerializeField] private CustomEvent OnPollution40;
    [SerializeField] private CustomEvent OnPollution70;
    [SerializeField] private CustomEvent OnPollutionMax;

    [SerializeField] private CustomEvent OnPollutionCleared;
    [SerializeField] private CustomEvent OnPollutionPercentageUp;
    [SerializeField] private CustomEvent OnPollutionPercentageDown;
    private bool isActive = false;
    private bool _eventCalled = false;
    private bool paused = false;

    public bool Paused
    {
        set { paused = value; }
    }

    void Start()
    {
        isActive = startActive;
        if (LevelStatsScript.Exists)
        {
            SetVariables(LevelStatsScript.FossilFuelPowerPlantStatsTier1);
        }
    }

    void Update()
    {
        if (paused)
            return;
        /**/
        //Debug.Log(isActive);
        if (isActive)
        {
            int oldPollution = Mathf.FloorToInt(currentPollution);
            currentPollution += pollutionPerTick;
            if (currentPollution >= 99)
            {
                currentPollution = 99;
                if(!_eventCalled)
                {
                    _eventCalled = true;
                    OnPollutionMax.Invoke();
                }
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
                if(!_eventCalled)
                {
                    _eventCalled = true;
                    OnPollutionCleared.Invoke();
                }
            }
        }
        /**/
    }

    private void SetVariables(FFPPStats stats)
    {
        pollutionPerTick = stats.pollutionPerTick;
        currentPollution = stats.currentPollution;
        isActive = stats.startActive;
    }

    public static float Pollution
    { get { return currentPollution; } }

    public void SetPlantActive(bool value)
    {
        //Debug.Log("I'm being called! I am now " + value);
        isActive = value;
        _eventCalled = false;
    }

    public void SetPollution(float value)
    {
        //currentPollution = value;
    }
}
