using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractableScript : InteractableScript
{
    Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
    }

    void Update()
    {

    }

    public override void RespondSelect()
    {
        thisButton.onClick.Invoke();
        Debug.Log("Activated");
    }

    public override void RespondDeselect()
    {

    }
}
