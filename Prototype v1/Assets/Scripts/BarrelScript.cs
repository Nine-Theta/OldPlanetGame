using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : InteractableScript
{
    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {

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
}
