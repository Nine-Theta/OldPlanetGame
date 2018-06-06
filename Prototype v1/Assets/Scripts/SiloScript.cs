﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloScript : InteractableScript
{
    private int _tier = 0;

    [SerializeField] private float _wasteStored = 0;
    [SerializeField] private float _wasteCapacity = 0;
    [SerializeField] private float _upgradeStorageMod = 100;
    [SerializeField] private float recycleAmount = 30;

    [SerializeField] private CityScript affectedCity;
    [SerializeField] private Transform _storageMeter;
    [SerializeField] private Transform _storageCapPole;

    [SerializeField] private Text _debugWasteText;

    [SerializeField] private CustomEvent OnBarrelSelect;
    [SerializeField] private CustomEvent OnBarrelDeselect;

    [SerializeField] private GameObject[] _tiers = new GameObject[0];

    private void Start()
    {
        if (LevelStatsScript.Exists)
        {
            SetVariables(LevelStatsScript.SiloStats);
        }
        Upgrade(true);
    }

    private void Update()
    {
        if (_debugWasteText != null)
            UpdateDebugInfo();
    }

    private void SetVariables(SiloStats stats)
    {
        _wasteStored = stats.wasteStored;
        _wasteCapacity = stats.wasteCapacity;
        _upgradeStorageMod = stats.upgradeStorageMod;
        recycleAmount = stats.recycleAmount;
    }

    public override void RespondSelect()
    {
        //Debug.Log("Selected");
        //StoreWaste();
        Upgrade();

    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected");

    }

    //public void StoreWaste()
    //{
    //    //_wasteStored += _powerPlantScript.DisposeOfWaste(_wasteCapacity - _wasteStored);
    //    if (_wasteStored != 0) _storageMeter.localScale = new Vector3(0.3f, 1.5f * (_wasteStored / _wasteCapacity), 0.3f);
    //}

    public void Upgrade(bool bypassResearchPoints = false)
    {
        if (affectedCity.ResearchPoints <= 0 && !bypassResearchPoints)
            return;
        else if (!bypassResearchPoints)
            affectedCity.SpendResearch(1);

        if (_tier >= _tiers.Length - 1)
        {
            _tier = _tiers.Length;
            return;
        }
        _tier++;
        _storageCapPole.localScale = new Vector3(_tier, 0.05f, 0.05f);

        _wasteCapacity = _tier * _upgradeStorageMod;

        if (_tiers[_tier] != null) _tiers[_tier].SetActive(true);
        if (_wasteStored != 0) _storageMeter.localScale = new Vector3(0.3f, 1.5f * (_wasteStored / _wasteCapacity), 0.3f);
    }

    public void RecycleWaste()
    {
        affectedCity.SpendResearch(1);
        _wasteStored -= recycleAmount;
        if (_wasteStored <= 0)
            _wasteStored = 0;
    }

    public void StoreBarrel(GameObject barrel)
    {
        Destroy(barrel);
        RespondToBarrelDeselection();
    }

    private void UpdateDebugInfo()
    {
        _debugWasteText.text = Mathf.Floor(_wasteStored).ToString();
    }

    public static void RespondToBarrelSelection()
    {
        GameObject[] silos = GameObject.FindGameObjectsWithTag("Silo");
        foreach (GameObject silo in silos)
        {
            silo.GetComponent<SiloScript>().OnBarrelSelect.Invoke();
        }
    }

    public static void RespondToBarrelDeselection()
    {
        GameObject[] silos = GameObject.FindGameObjectsWithTag("Silo");
        foreach (GameObject silo in silos)
        {
            silo.GetComponent<SiloScript>().OnBarrelDeselect.Invoke();
        }
    }
}
