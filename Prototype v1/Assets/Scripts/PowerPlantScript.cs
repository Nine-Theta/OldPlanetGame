﻿using System.Collections;
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
    [SerializeField] private float _maxDurability = 200;
    private float _currentDurability = 200; //Current status, broken if 0

    [SerializeField] private int maxTier = 3;
    [SerializeField] private GameObject tier2Upgrade;
    [SerializeField] private CityScript affectedCity;
    [SerializeField] private Text debugWasteText;
    [SerializeField] private GameObject _wasteBarrelPrefab;
    //public Text debugWasteText;

    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (_currentDurability <= 0)
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
        if (_currentDurability <= 0)
        {
            Repair();
        }
        else
        {
            Upgrade();
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
        if (affectedCity.ResearchPoints <= 0)
            return;
        affectedCity.SpendResearch(1);
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
        _wasteStored = 0;
    }

    void Degrade()
    {
        _currentDurability -= Random.Range(0, _degradeRange);
        if (_currentDurability <= 0)
        {
            BreakDown();
        }
    }

    void GenerateWaste()
    {
        _wasteStored += _wasteGenPerTick;
        if (_wasteStored > _maxWaste)
        {
            _wasteStored = 0;
            //Degrade(); //Breaks down faster
            Vector3 offset = Random.onUnitSphere; //Multiply onUnitSphere by planet radius once it is known
            offset.y = 0; //TODO: Remove this
            offset.Normalize(); //TODO: Remove this
            offset *= 3.0f;
            Vector3 worldPos = ((transform.localPosition + offset));
            worldPos.x *= transform.localScale.x;
            worldPos.y *= transform.localScale.y;
            worldPos.z *= transform.localScale.z;
            GameObject barrelRef = Instantiate(_wasteBarrelPrefab, GetComponent<BoxCollider>().center + worldPos, transform.rotation);

            //Set Object position offset from the plant at surface of planet
            //Set Object's proper rotation
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
        _currentDurability = _maxDurability;
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
