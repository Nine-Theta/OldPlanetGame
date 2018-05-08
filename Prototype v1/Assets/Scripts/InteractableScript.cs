using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public virtual void RespondSelect()
    {
        Debug.Log("Selected");
    }

    public virtual void RespondDeselect()
    {
        Debug.Log("Deselected");
    }
}
