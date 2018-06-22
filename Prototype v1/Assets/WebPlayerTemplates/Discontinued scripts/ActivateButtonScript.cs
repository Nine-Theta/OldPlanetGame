using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script holding a reference to a button, setting the gameobject to active using RespondSelect and RespondDeselect
/// </summary>
public class ActivateButtonScript : InteractableScript
{
    public Button button;

    void Start()
    {

    }

    void Update()
    {

    }

    public override void RespondSelect()
    {
        button.gameObject.SetActive(true);
    }

    public override void RespondDeselect()
    {
        button.gameObject.SetActive(false);
    }
}
