using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragTest : MonoBehaviour{

    private Transform _focusTransform;
    private Rigidbody _focusbody;
    
	private void Start (){

        _focusbody = GetComponentInParent<Rigidbody>();
        _focusTransform = _focusbody.GetComponent<Transform>();
	}
	
	private void Update () {
        if (Input.GetMouseButton(0))
        {
            //_focusTransform.rotation = Quaternion.Euler(Input.GetAxis("Mouse Y") + _focusTransform.rotation.eulerAngles.x, Input.GetAxis("Mouse X") + _focusTransform.rotation.eulerAngles.y, 0);
            _focusbody.AddRelativeTorque(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }

        
        //if (_focusTransform.rotation.eulerAngles.z != 0)
            //_focusTransform.rotation = Quaternion.Euler(_focusTransform.rotation.eulerAngles.x, _focusTransform.rotation.eulerAngles.y, 0);

    }
}
