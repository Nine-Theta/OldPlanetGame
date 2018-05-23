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
    private Light _light;
    [SerializeField] private float _maxWaste = 30.0f;
    private float _wasteStored = 0;
    [SerializeField] private float _wasteGenPerTick = 0.03f;
    [Tooltip("A random floating point number between 0 and this variable will be substracted from hp every update call")]
    [SerializeField] private float _degradeRange = 3.0f;
    [SerializeField] private float _maxDurability = 200;
    private float _currentDurability = 200; //Current status, broken if >= 0

    [SerializeField] private int maxTier = 3;
    [SerializeField] private GameObject tier2Upgrade;
    [SerializeField] private CityScript affectedCity;
    [SerializeField] private Text debugWasteText;
    [SerializeField] private GameObject _wasteBarrelPrefab;
    [SerializeField] private Transform _wasteBarrelSpawn;
    [SerializeField] private CustomEvent OnBreakdown;
    [SerializeField] private CustomEvent OnRepair;
    [SerializeField] private CustomEvent OnUpgrade;
    //public Text debugWasteText;

    public bool IsFunctional
    { get { return _currentDurability >= 0; } }

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

    /// <summary>
    /// Upgrades the powerplant if the cost is met, exits early otherwise. Calls OnUpgrade() when cost is met
    /// </summary>
    /// <param name="cost">Research points that are spend (and need to be met to successfully execute)</param>
    public void Upgrade(int cost = 1)
    {
        if (affectedCity.ResearchPoints <= cost)
            return;
        affectedCity.SpendResearch(cost);
        OnUpgrade.Invoke();
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
        _wasteStored += Random.Range(0.0f, _wasteGenPerTick);
        if (_wasteStored > _maxWaste)
        {
            _wasteStored = 0;
            if (_wasteBarrelSpawn != null)
            {
                GameObject barrelRef = Instantiate(_wasteBarrelPrefab, _wasteBarrelSpawn.position, _wasteBarrelSpawn.rotation, _wasteBarrelSpawn);
            }
            else
            {
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
            //Set Object position offset from the plant at surface of planet
            //Set Object's proper rotation
        }
    }

    void BreakDown()
    {
        OnBreakdown.Invoke();
        affectedCity.RespondToBreakdown();
        _light.color = Color.red;
    }

    void Repair()
    {
        OnRepair.Invoke();
        affectedCity.RespondToRepairs();
        _light.color = Color.white;
        _currentDurability = _maxDurability;
    }

    //public float DisposeOfWaste(float remainingCapacity)
    //{
    //    float tempWaste = _wasteStored;
    //    if (remainingCapacity >= _wasteStored)
    //    {
    //        _wasteStored = 0;
    //        return tempWaste;
    //    }
    //    else
    //    {
    //        _wasteStored -= remainingCapacity;
    //        return remainingCapacity;
    //    }
    //}
}
