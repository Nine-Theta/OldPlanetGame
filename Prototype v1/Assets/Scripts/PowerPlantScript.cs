using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;


[System.Serializable]
public class CustomEvent : UnityEvent { }

public class PowerPlantScript : InteractableScript
{
    private int _tier = 1;

    #region PartOfLevels
    [SerializeField] private int _partOfLevel = 1;
    public int PartOfLevel
    { get { return _partOfLevel; } }
    #endregion

    private float _maxWaste = 30.0f;
    private float _wasteStored = 0;
    private float _wasteGenPerTick = 0.03f;
    private float _degradeRange = 3.0f;
    private float _maxDurability = 200;
    private int _upgradeCost = 1;
    private float _repairPerTap = 5;
    private float _currentDurability = 0; //Current status, broken if >= 0
    private float _repairThreshold = 150;
    private float _maintenanceAlertThreshold = 150;

    private bool _isBroken = false;
    private bool _winConditionMet = false;
    private bool _upgradeEventCalled = false; //For OnUpgradeAvailable

    private int maxTier = 3;
    [SerializeField] private CityScript affectedCity;
    [SerializeField] private GameObject _wasteBarrelPrefab;
    [SerializeField] private Transform[] _wasteBarrelSpawns;
    [SerializeField] private CustomEvent OnBreakdown;
    [SerializeField] private CustomEvent OnRepair;
    [SerializeField] private CustomEvent OnMaintained;
    [SerializeField] private CustomEvent OnMaintenanceAlert;
    [SerializeField] private CustomEvent OnTier2Upgrade;
    [SerializeField] private CustomEvent OnTier3Upgrade;
    [SerializeField] private CustomEvent OnUpgradeAvailable;
    [SerializeField] private CustomEvent OnInitialBarrelSpawn;

    private static bool _InitialBarrelSpawned = false;


    //public Text debugWasteText;

    private ParticleSystem _particleSystem;


    public bool IsFunctional
    { get { return _currentDurability >= 0; } }

    void Start()
    {
        //_particleSystem = GetComponentInChildren<ParticleSystem>();
        BreakDown();
        if (LevelStatsScript.Exists)
        {
            SetVariables(LevelStatsScript.NuclearPowerPlantStatsTier1);
        }
    }

    void Update()
    {
        if (_currentDurability <= 0)
            return;
        if (!_isBroken)
            Degrade();
        if (!_isBroken)
            GenerateWaste();
        if(!_upgradeEventCalled && affectedCity.ResearchPoints >= _upgradeCost)
        {
            _upgradeEventCalled = true;
            OnUpgradeAvailable.Invoke();
        }

        if (!_winConditionMet && CheckWinConditions())
        {
            Debug.Log(gameObject.name + " is done, sending win signal");
            EndConditionScript.SignalNPPDone();
            enabled = false;
            _winConditionMet = true;
        }
    }

    #region interactableScript
    public override void RespondSelect()
    {
        //Debug.Log("Selected PP");
        OnTap.Invoke();
        if (_currentDurability <= _maxDurability - 5)
        {
            Repair(_repairPerTap);
        }
        else
        {
            Upgrade(_upgradeCost);
        }
    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected PP");
    }
    #endregion

    private void SetVariables(NPPStats stats)
    {
        _maxWaste = stats.maxWaste;
        _wasteGenPerTick = stats.wasteGenPerTick;
        _degradeRange = stats.degradeRange;
        _maxDurability = stats.maxDurability;
        _upgradeCost = stats.UpgradeCost;
        _repairPerTap = stats.repairPerTap;
        _repairThreshold = stats.repairThreshold;
        _maintenanceAlertThreshold = stats.maintenanceAlertThreshold;
        _upgradeEventCalled = false;

        affectedCity.SetUpgradeCost(_upgradeCost);
        //Debug.Log(_upgradeCost);
    }

    /// <summary>
    /// Upgrades the powerplant if the cost is met, exits early otherwise. Calls OnUpgrade() when cost is met
    /// </summary>
    /// <param name="cost">Research points that are spend (and need to be met to successfully execute)</param>
    private void Upgrade(int cost = 1)
    {
        if (affectedCity.ResearchPoints <= cost)
            return;
        if (_tier >= maxTier)
        {
            _tier = maxTier;
            return;
        }

        _tier++;
        if (_tier == 2)
        {
            OnTier2Upgrade.Invoke();
            if (LevelStatsScript.Exists)
                SetVariables(LevelStatsScript.NuclearPowerPlantStatsTier2);
        }
        else if (_tier == 3)
        {
            OnTier3Upgrade.Invoke();
            if (LevelStatsScript.Exists)
                SetVariables(LevelStatsScript.NuclearPowerPlantStatsTier3);
        }
        affectedCity.SpendResearch(cost);
    }

    void Degrade()
    {
        _currentDurability -= Random.Range(0, _degradeRange);
        if (_currentDurability <= _maintenanceAlertThreshold)
        {
            OnMaintenanceAlert.Invoke();
        }
        if (_currentDurability <= 0)
        {
            BreakDown();
        }
    }

    void GenerateWaste()
    {
        _wasteStored += Random.Range(0.0f, _wasteGenPerTick);
        if (_wasteStored > _maxWaste)
        {
            _wasteStored = 0;
            if (_wasteBarrelSpawns != null)
            {
                int index = Mathf.FloorToInt(Random.Range(0.0f, _wasteBarrelSpawns.Length - 0.1f));
                GameObject barrelRef = Instantiate(_wasteBarrelPrefab, _wasteBarrelSpawns[index].position, _wasteBarrelSpawns[index].rotation, _wasteBarrelSpawns[index]);
                //barrelRef.transform.rotation = transform.rotation;
                if(!_InitialBarrelSpawned)
                {
                    OnInitialBarrelSpawn.Invoke():
                    _InitialBarrelSpawned = true;
                }
            }
            else
            {
                Debug.LogError("BARRELSPAWN IS NULL! USING BAD SPAWN MECHANIC INSTEAD! FIX ASAP!");
                #region oldspawn
                /**/
                Vector3 offset = Random.onUnitSphere; //Multiply onUnitSphere by planet radius once it is known
                GameObject barrelRef = Instantiate(_wasteBarrelPrefab, offset, transform.rotation, transform);
                barrelRef.transform.localScale = new Vector3(0.2f / transform.localScale.x, 0.2f / transform.localScale.y, 0.2f / transform.localScale.z);
                offset = barrelRef.transform.localPosition;
                offset.y += 0.05f;
                barrelRef.transform.localPosition = offset;
                /**/
                #endregion
            }
        }
    }

    void BreakDown()
    {
        OnBreakdown.Invoke();
        _isBroken = true;
    }

    void Repair()
    {
        _isBroken = false;
        OnRepair.Invoke();
        _currentDurability = _maxDurability;
    }

    void Repair(float amount)
    {
        _currentDurability += amount;
        if (_currentDurability >= _repairThreshold)
        {
            _currentDurability = _maxDurability;
            if (_isBroken)
            {
                OnRepair.Invoke();
            }
            else
            {
                OnMaintained.Invoke();
            }
            _isBroken = false;
            _upgradeEventCalled = false;
        }
    }

    public void ParticleRepairCheck(ParticleSystem pPsystem)
    {
        if (_currentDurability <= _repairThreshold)
        {
            pPsystem.Play();
        }
    }

    private bool CheckWinConditions()
    {
        return (_tier == maxTier && affectedCity.EndConditionMet());
    }
}
