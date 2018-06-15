using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloScript : InteractableScript
{
    #region PartOfLevels
    [SerializeField] private int _partOfLevel = 1;
    public int PartOfLevel
    { get { return _partOfLevel; } }
    #endregion

    [SerializeField] private CustomEvent OnBarrelSelect;
    [SerializeField] private CustomEvent OnBarrelDeselect;

    [SerializeField] private GameObject[] _tiers = new GameObject[0];
    

    public override void RespondSelect()
    {

    }

    public override void RespondDeselect()
    {

    }

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
