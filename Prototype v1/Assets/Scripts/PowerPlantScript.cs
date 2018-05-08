using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantScript : MonoBehaviour
{
    private int _tier = 1;
    private float _energyGenerated = 0;
    private float _energyGenPerTick = 0.1f;


    public int maxTier = 3;
    public GameObject tier2Upgrade;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Upgrade()
    {
        if(_tier >= maxTier)
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
}
