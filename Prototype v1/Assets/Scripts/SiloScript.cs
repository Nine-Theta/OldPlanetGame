using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiloScript : MonoBehaviour {

    private int _tier = 0;

    [SerializeField] private float _wasteStored = 0;
    [SerializeField] private float _wasteCapacity = 0;
    [SerializeField] private float _upgradeStorageMod = 100;

    [SerializeField] private PowerPlantScript _powerPlantScript;
    [SerializeField] private Transform _storageMeter;
    [SerializeField] private Transform _storageCapPole;

    [SerializeField] private GameObject[] _tiers = new GameObject[0];

    private void Start()
    {
    }

    private void Update()
    {

    }

    public void StoreWaste()
    {
        _wasteStored += _powerPlantScript.DisposeOfWaste(_wasteCapacity-_wasteStored);
        if (_wasteStored != 0) _storageMeter.localScale = new Vector3(0.3f, 0.5f * _wasteStored, 0.3f);
    }

    public void Upgrade()
    {
        if (_tier >= _tiers.Length-1)
        {
            _tier = _tiers.Length;
            return;
        }
        _tier++;
        _storageCapPole.localScale = new Vector3(_tier, 0.05f, 0.05f);

        _wasteCapacity = _tier * _upgradeStorageMod;

        _tiers[_tier].SetActive(true);
    }
}
