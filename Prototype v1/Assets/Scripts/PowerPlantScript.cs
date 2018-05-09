using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantScript : MonoBehaviour
{
    private int _tier = 1;
    private float _maxWaste = 30.0f;
    private float _wasteStored = 0;
    private float _wasteGenPerTick = 0.1f;
    private float _degradeRange = 1.0f;
    private float _currentHP = 100; //Current status, broken if 0

    public int maxTier = 3;
    public GameObject tier2Upgrade;
    public CityScript affectedCity;
    public Text debugWasteText;
    //public Text debugWasteText;

    void Start()
    {
    }

    void Update()
    {
        if (_tier < 2)
            Degrade();
        if (_tier < 3)
            GenerateWaste();
        UpdateDebugInfo();
    }

    void UpdateDebugInfo()
    {
        debugWasteText.text = _wasteStored.ToString();
    }

    public void Upgrade()
    {
        if (_currentHP <= 0)
            return;
        if (_tier >= maxTier)
        {
            _tier = maxTier;
            return;
        }
        _tier++;
        if (_tier >= 2)
        {
            tier2Upgrade.SetActive(true);
        }
    }

    void Degrade()
    {
        _currentHP -= Random.Range(0, _degradeRange);
    }

    void GenerateWaste()
    {
        _wasteStored += _wasteGenPerTick;
        if(_wasteStored > _maxWaste)
        {
            //Warn player before this happens, give feedback on how to solve it
            BreakDown();
        }
    }

    void BreakDown()
    {
        affectedCity.RespondToBreakdown();
    }

    public float DisposeOfWaste()
    {
        float tempWaste = _wasteStored;
        _wasteStored = 0;
        return tempWaste;
    }
}
