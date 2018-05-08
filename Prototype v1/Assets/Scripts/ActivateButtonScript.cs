using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
