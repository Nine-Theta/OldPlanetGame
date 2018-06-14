using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectSelectorScript), typeof(Camera))]
public class MouseInputInterpreter : MonoBehaviour
{
    [SerializeField] private float _mouseCameraSpeed = 1.0f;
    [SerializeField] private float _touchCameraSpeed = 1.0f;

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
                //Debug.Log("RayHit Name: " + hit.collider.name);
            }
        }
        else if (Input.GetMouseButton(0) || (Input.touchSupported && Input.touchCount > 0))
        {
            if (_selectorScript.HasSelection && _selectorScript.SelectionTag == "Dragable")
            {
                Ray ray;

                if (Input.touchSupported && Input.touchCount > 0)
                    ray = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                else
                    ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                    _selectorScript.DragGameObject(hit);
                else { }
                //_selectorScript.Deselect();
            }
            else
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(_mainCamera.transform.position, ray.direction * 50, new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)), 2.0f);
                if (Physics.Raycast(ray, out hit))
                {
                    return;
                }
                else if (Input.touchSupported && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPos = Input.GetTouch(0).deltaPosition;
                    _focusbody.AddRelativeTorque(-touchDeltaPos.y * _touchCameraSpeed, touchDeltaPos.x * _touchCameraSpeed, 0);
                }
                else
                {
                    _focusbody.AddRelativeTorque(-Input.GetAxis("Mouse Y") * _mouseCameraSpeed, Input.GetAxis("Mouse X") * _mouseCameraSpeed, 0);
                }
            }
        }
    }
}
