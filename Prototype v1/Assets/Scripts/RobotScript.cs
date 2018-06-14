using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum RobotBehaviour { IDLE, FOLLOWCAM, DOTHING, MOVETOCAM, MOVETOIDLE }

public class RobotScript : MonoBehaviour {

    [SerializeField] private Transform _camFocus;
    [SerializeField] private Transform _botFocus;

    [SerializeField] private Vector3 _idleDirection = new Vector3(-1,-1,0);
    [SerializeField] private float _idleDireChange = 0.001f;
    [SerializeField] private float _idleSpeed = 8.0f;
    [SerializeField] private float _idleHeight = 8.0f;
    [SerializeField] private float _moveToIdleSpeed = 0.01f;

    private Quaternion _oldCamPos;
    private Quaternion _olderCamPos;
    private Quaternion _oldestCamPos;

    [SerializeField] private RobotBehaviour _state = RobotBehaviour.FOLLOWCAM;
    
    [SerializeField] private CustomEvent OnIdleReached;

    private void Start () {
        //_botFocus = gameObject.GetComponentInParent<Rigidbody>().GetComponentInParent<Transform>();
        //Debug.Log("f: " +_botFocus.gameObject.name);
	}

    private void FollowCamera()
    {
        _botFocus.rotation = _olderCamPos;
        //transform.rotation = _oldestCamPos;
        //_oldestCamPos = _olderCamPos;
        _olderCamPos = _oldCamPos;
        _oldCamPos = _camFocus.rotation;
    }

    private void Idle()
    {
        _botFocus.GetComponent<Rigidbody>().AddRelativeTorque(_idleDirection.normalized * _idleSpeed);
        _idleDirection.y -= _idleDireChange;
    }

    private void MoveToIdle()
    {
        Debug.Log(transform.position.magnitude);

        if (transform.position.magnitude > _idleHeight)
        {
            transform.position -= transform.position.normalized*_moveToIdleSpeed;
        }
        else
        {
            transform.position = transform.position.normalized * _idleHeight;
            Idle();
            _state = RobotBehaviour.IDLE;
            OnIdleReached.Invoke();
        }
    }

    private void RotateToZero(float pSpeed = 0.5f)
    {
        float x = _botFocus.rotation.eulerAngles.x;
        float y = _botFocus.rotation.eulerAngles.y;
        float z = _botFocus.rotation.eulerAngles.z;

        if (x > 0.05f || x < -0.05f)
        {
            if (x > 180) x += pSpeed;
            else x -= pSpeed;
        }
        else x = 0;

        if (y > 0.05f || y < -0.05f)
        {
            if (y > 180) y += pSpeed;
            else y -= pSpeed;
        }
        else y = 0;

        if (z > 0.05f || z < -0.05f)
        {
            if (z > 180) z += pSpeed;
            else z -= pSpeed;
        }
        else z = 0;

        _botFocus.rotation = Quaternion.Euler(x,y,z);
    }

    private void MoveToPoint(Vector3 pPoint, float pSpeed = 0.05f)
    {
        float dx = pPoint.x - transform.position.x;
        float dy = pPoint.y - transform.position.y;
        float dz = pPoint.z - transform.position.z;

        Vector3 pos = Vector3.zero;

        if (dx > 0.001f || dx < -0.001f) pos.x = dx * pSpeed + transform.position.x;
        else pos.x = pPoint.x;
        if (dy > 0.001f || dy < -0.001f) pos.y = dy * pSpeed + transform.position.y;
        else pos.y = pPoint.y;
        if (dz > 0.001f || dz < -0.001f) pos.z = dz * pSpeed + transform.position.z;
        else pos.z = pPoint.z;

        transform.position = pos;
    }
	
	private void FixedUpdate () {

        switch (_state)
        {
            case RobotBehaviour.IDLE:
                Idle();
                break;

            case RobotBehaviour.FOLLOWCAM:
                FollowCamera();
                break;

            case RobotBehaviour.DOTHING:
                break;

            case RobotBehaviour.MOVETOCAM:
                break;

            case RobotBehaviour.MOVETOIDLE:
                MoveToIdle();
                break;
        }
	}
}
