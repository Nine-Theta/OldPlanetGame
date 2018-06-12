using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloScript : InteractableScript
{
    [SerializeField] private CustomEvent OnBarrelSelect;
    [SerializeField] private CustomEvent OnBarrelDeselect;

    [SerializeField] private GameObject[] _tiers = new GameObject[0];

    private void Start()
    {

    }

    private void Update()
    {
    }

    public override void RespondSelect()
    {

    }

    public override void RespondDeselect()
    {

    }

    #region oldshite
    //public void Upgrade(bool bypassResearchPoints = false)
    //{
    //    if (affectedCity.ResearchPoints <= 0 && !bypassResearchPoints)
    //        return;
    //    else if (!bypassResearchPoints)
    //        affectedCity.SpendResearch(1);

    //    if (_tier >= _tiers.Length - 1)
    //    {
    //        _tier = _tiers.Length;
    //        return;
    //    }
    //    _tier++;
    //    _storageCapPole.localScale = new Vector3(_tier, 0.05f, 0.05f);

    //    _wasteCapacity = _tier * _upgradeStorageMod;

    //    if (_tiers[_tier] != null) _tiers[_tier].SetActive(true);
    //    if (_wasteStored != 0) _storageMeter.localScale = new Vector3(0.3f, 1.5f * (_wasteStored / _wasteCapacity), 0.3f);
    //}

    //public void RecycleWaste()
    //{
    //    affectedCity.SpendResearch(1);
    //    _wasteStored -= recycleAmount;
    //    if (_wasteStored <= 0)
    //        _wasteStored = 0;
    //}
    #endregion

    public void StoreBarrel(GameObject barrel)
    {
        Destroy(barrel);
        RespondToBarrelDeselection();
    }


    public static void RespondToBarrelSelection()
    {
        GameObject[] silos = GameObject.FindGameObjectsWithTag("Silo");
        foreach (GameObject silo in silos)
        {
            if (silo.GetComponent<SiloScript>().enabled)
                silo.GetComponent<SiloScript>().OnBarrelSelect.Invoke();
        }
    }

    public static void RespondToBarrelDeselection()
    {
        GameObject[] silos = GameObject.FindGameObjectsWithTag("Silo");
        foreach (GameObject silo in silos)
        {
            if (silo.GetComponent<SiloScript>().enabled)
                silo.GetComponent<SiloScript>().OnBarrelDeselect.Invoke();
        }
    }
}
