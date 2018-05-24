using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] protected CustomEvent OnTap;

    void Start()
    {

    }

    void Update()
    {

    }

    public virtual void RespondSelect()
    {
        Debug.Log("Selected " + gameObject.name);
        OnTap.Invoke();
    }

    public virtual void RespondDeselect()
    {
        Debug.Log("Deselected");
    }
}
