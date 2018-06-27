using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverScript : MonoBehaviour
{
    [SerializeField] private CustomEvent OnMouseOverEvent;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseOver()
    {
        OnMouseOverEvent.Invoke();
    }
}
