using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectSelectorScript), typeof(Camera))]
public class MouseInputInterpreter : MonoBehaviour
{

    private Rigidbody _focusbody;
    private Camera _mainCamera;
    private GameObjectSelectorScript _selectorScript;

    private void Start()
    {
        _focusbody = GetComponentInParent<Rigidbody>();
        _mainCamera = GetComponent<Camera>();
        _selectorScript = GetComponent<GameObjectSelectorScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _selectorScript.HasSelection)
        {
            _selectorScript.Deselect();
            return;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(_mainCamera.transform.position, ray.direction * 10, Color.red, 2.0f);
            if (Physics.Raycast(ray, out hit))
            {
                _selectorScript.TestCollider(hit);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_selectorScript.HasSelection && _selectorScript.SelectionTag == "Dragable")
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                    _selectorScript.DragGameObject(hit);
                else
                    _selectorScript.Deselect();
            }
            else
            {
                _focusbody.AddRelativeTorque(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            }
        }


    }
}
