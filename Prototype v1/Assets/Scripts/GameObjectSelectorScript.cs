using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelectorScript : MonoBehaviour
{
    bool _hasSelection = false;
    GameObject _selection = null;

    void Start()
    {

    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        //TODO: Make a system that differentiates single click and click and drag
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (_hasSelection && hit.collider.tag == "Deselector")
                {
                    Deselect();
                }
                if (hit.collider.GetComponent<InteractableScript>() != null)
                {
                    //if (_selection == hit.collider.gameObject) //Early exit, no need for clicking on the same object again
                    //    return; 
                    if (_hasSelection && _selection.tag != "Dragable") //If selecting a new selectable, deselect the old one and select the next
                    {
                        Deselect();
                    }
                    _selection = hit.collider.gameObject;
                    _selection.GetComponent<InteractableScript>().RespondSelect();
                    _hasSelection = true;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_selection.tag == "Dragable")
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    _selection.GetComponent<BarrelScript>().SetPosition(hit.point);
                }
                else
                {
                    Deselect();
                }
            }
        }
        else if(Input.GetMouseButtonUp(0) && _hasSelection)
        {
            Deselect();
        }
    }

    void Deselect()
    {
        _selection.GetComponent<InteractableScript>().RespondDeselect();
        _hasSelection = false;
        _selection = null;
    }
}
