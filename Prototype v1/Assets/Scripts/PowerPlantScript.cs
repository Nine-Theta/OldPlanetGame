using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantScript : MonoBehaviour
{
    private int _tier = 1;
    private float _energyGenerated = 0;
    private float _energyGenPerTick = 0.1f;

    public GameObject tier2Upgrade;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Upgrade()
    {
        _tier++;
        if (_tier >= 2)
        {
            tier2Upgrade.SetActive(true);
        }
    }
}
