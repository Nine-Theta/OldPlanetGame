using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelectorScript : MonoBehaviour
{
    private GameObject _selection = null;

    private bool _hasSelection = false;

    public bool HasSelection
    { get { return _hasSelection; } }

    public string SelectionTag
    { get { return _selection.tag; } }

    public void TestCollider(RaycastHit pHit)
    {
        if(_hasSelection && pHit.collider.tag == "Deselector")
        {
            Deselect();
            return;
        }
        if (pHit.collider.GetComponent<InteractableScript>() != null)
        {
            if (_hasSelection && _selection.tag != "Dragable") //If selecting a new selectable, deselect the old one and select the next
            {
                Deselect();
            }
            _selection = pHit.collider.gameObject;
            _selection.GetComponent<InteractableScript>().RespondSelect();
            _hasSelection = true;        
        }
    }

    public void DragGameObject(RaycastHit pHit)
    {
        _selection.GetComponent<BarrelScript>().SetPosition(pHit.point);
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

    public void Deselect()
    {
        _selection.GetComponent<InteractableScript>().RespondDeselect();
        _hasSelection = false;
        _selection = null;
    }
}
