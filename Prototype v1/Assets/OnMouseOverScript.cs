using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CustomEvent OnMouseEnter;
    [SerializeField] private CustomEvent OnMouseExit;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit.Invoke();
    }
}
