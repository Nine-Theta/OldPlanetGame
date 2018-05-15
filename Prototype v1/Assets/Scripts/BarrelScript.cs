﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : InteractableScript
{
    private Collider _collider;
    [SerializeField] private float _wasteStored = 30.0f;
    static List<BarrelScript> _activeBarrels = new List<BarrelScript>();

    public float WasteStored
    {
        get { return _wasteStored; }
    }


    void Start()
    {
        _collider = GetComponent<Collider>();
        _activeBarrels.Add(this);
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        _activeBarrels.Remove(this);
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public override void RespondSelect()
    {
        //Debug.Log("Selected");
        _collider.enabled = false;
    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected");
        _collider.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The amount of active barrels in the scene</returns>
    public static int GetBarrelCount()
    {
        return _activeBarrels.Count;
    }

}
