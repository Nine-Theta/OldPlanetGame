using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTestScript : MonoBehaviour {

    //[SerializeField] private Transform CameraPos; //3.9 -8.8;
    [SerializeField] private Transform FocusPos;
    //[SerializeField] private Transform RobotPos;

    private Quaternion _oldCamPos;
    private Quaternion _olderCamPos;
    private Quaternion _oldestCamPos;

    //private Rigidbody _body;
    
	private void Start () {
        //_body = GetComponent<Rigidbody>();

    }
	
	private void FixedUpdate () {

        //transform.rotation = _oldestCamPos;
        transform.rotation = _olderCamPos;
        //_oldestCamPos = _olderCamPos;
        _olderCamPos = _oldCamPos;
        _oldCamPos = FocusPos.rotation;


        /*
        float dY = camFocus.y - rotation.y;
        float dX = camFocus.x - rotation.x;
        float dZ = camFocus.z - rotation.z;
        */
        //Debug.Log("[Y]: cam: "+ camFocus.y + " bot: " + rotation.y + " delta: " +dY);
        //Debug.Log("[X]: cam: " + camFocus.x + " bot: " + rotation.x + " delta: " + dX);

        /*if (dX > 0.1f || dX < -0.1f)
        {
            if (dX > 180 || dX < -180)
            {
                if (camFocus.x > rotation.x)
                    dX = (camFocus.x - 360) - rotation.x;
                else
                    dX = (camFocus.x + 360) - rotation.x;
            }
            _body.AddTorque(dX, 0, 0);
        }
        else
        {
            _body.angularVelocity = new Vector3(0, _body.angularVelocity.y, 0);
        }
        
        if (dY > 0.1f || dY < -0.1f)
        {
            if(dY > 180 || dY < -180)
            {
                if (camFocus.y > rotation.y)
                    dY = (camFocus.y - 360) - rotation.y;
                else
                    dY = (camFocus.y +360) - rotation.y;
            }
            _body.AddRelativeTorque(0, dY, 0);
        }
        else
        {
            _body.angularVelocity = new Vector3(_body.angularVelocity.x, 0, 0);
        }*/

        /*
        if (dZ > 0.05f || dZ < -0.05f)
        {
            if (dZ > 180 || dZ < -180)
            {
                if (camFocus.z > rotation.z)
                    dZ = (camFocus.z - 360) - rotation.z;
                else
                    dZ = (camFocus.z + 360) - rotation.z;
            }
            _body.AddRelativeTorque(0, 0, dZ);
        }*/
    }
}
