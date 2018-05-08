using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelectorScript : MonoBehaviour
{
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
        if (Input.GetMouseButtonDown(0))
        {
            if (_selection != null && _selection.GetComponent<InteractableScript>() != null)
                _selection.GetComponent<InteractableScript>().RespondDeselect();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _selection = hit.collider.gameObject;
                if (_selection.GetComponent<InteractableScript>() != null)
                {
                    _selection.GetComponent<InteractableScript>().RespondSelect();
                }
            }
        }
    }
}
