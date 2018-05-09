using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantScript : InteractableScript
{
    private int _tier = 1;
    private Light _light;
    private float _maxWaste = 30.0f;
    private float _wasteStored = 0;
    [SerializeField] private float _wasteGenPerTick = 0.03f;
    [Tooltip("A random floating point number between 0 and this variable will be substracted from hp every update call")]
    [SerializeField] private float _degradeRange = 3.0f;
    [SerializeField] private float _maxHP = 200;
    private float _currentHP = 200; //Current status, broken if 0

    public int maxTier = 3;
    public GameObject tier2Upgrade;
    public CityScript affectedCity;
    public Text debugWasteText;
    //public Text debugWasteText;

    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (_currentHP <= 0)
            return;
        if (_tier < 2)
            Degrade();
        if (_tier < 3)
            GenerateWaste();
        UpdateDebugInfo();
    }

    public override void RespondSelect()
    {
        //Debug.Log("Selected PP");
        if(_currentHP <= 0)
        {
            Repair();
        }
    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected PP");
    }

    void UpdateDebugInfo()
    {
        debugWasteText.text = Mathf.Floor(_wasteStored).ToString();
    }

    public void Upgrade()
    {
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
        if (_currentHP <= 0)
        {
            BreakDown();
        }
    }

    void GenerateWaste()
    {
        _wasteStored += _wasteGenPerTick;
        if (_wasteStored > _maxWaste)
        {
            //Warn player before this happens, give feedback on how to solve it
            _wasteStored = _maxWaste;
            Degrade(); //Breaks down faster
        }
    }

    void BreakDown()
    {
        affectedCity.RespondToBreakdown();
        _light.color = Color.red;
    }

    void Repair()
    {
        affectedCity.RespondToRepairs();
        _light.color = Color.white;
        _currentHP = _maxHP;
    }

    public float DisposeOfWaste(float remainingCapacity)
    {
        float tempWaste = _wasteStored;
        if (remainingCapacity >= _wasteStored)
        {
            _wasteStored = 0;
            return tempWaste;
        }
        else
        {
            _wasteStored -= remainingCapacity;
            return remainingCapacity;
        }
    }
}
