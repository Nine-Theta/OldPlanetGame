using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum RobotBehaviour { IDLE, FOLLOWCAM, NARRATE, MOVETOCAM, MOVETOIDLE }

public class RobotScript : InteractableScript {

    [SerializeField] private Transform _camFocus;
    [SerializeField] private Transform _botFocus;

    [SerializeField] private float _idleSpeed = 8.0f;
    [SerializeField] private float _idleHeight = 8.0f;
    [SerializeField] private float _moveToIdleSpeed = 0.01f;

    [SerializeField] private string _tutorialMinigameName = "Minigame Tutorial";
    [SerializeField] private string _minigameLevel1Name = "Minigame Level 1";
    [SerializeField] private string _minigameLevel2Name = "Minigame Level 2";
    [SerializeField] private string _minigameLevel3Name = "Minigame Level 3";


    private Quaternion _oldCamPos;
    private Quaternion _olderCamPos;
    private Quaternion _oldestCamPos;

    [SerializeField] private RobotBehaviour _state = RobotBehaviour.FOLLOWCAM;
    
    [SerializeField] private CustomEvent OnIdleReached;
    [SerializeField] private CustomEvent OnPollutionMax;
    [SerializeField] private CustomEvent OnMinigameReady;

    private void Start () {
	}

    public void SetStateIDLE() { _state = RobotBehaviour.IDLE; }
    public void SetStateFOLLOWCAM() { _state = RobotBehaviour.FOLLOWCAM; }
    public void SetStateNARRATE() { _state = RobotBehaviour.NARRATE; }
    public void SetStateMOVETOCAM() { _state = RobotBehaviour.MOVETOCAM; }
    public void SetStateMOVETOIDLE() { _state = RobotBehaviour.MOVETOIDLE; }

    private void FollowCamera()
    {
        _botFocus.rotation = _olderCamPos;
        _olderCamPos = _oldCamPos;
        _oldCamPos = _camFocus.rotation;
    }

    private void Idle()
    {
        _botFocus.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(0, _idleSpeed, 0));
        transform.rotation = Quaternion.Euler(_botFocus.rotation.eulerAngles.x, _botFocus.rotation.eulerAngles.y + 90, 0);
        
        /**
        if(_idleDirection.y >= 1)
        {
            if (_idleDirection.x < 1)
                _idleDirection.x += _idleDirChange;
            else
                _idleDirection.y -= _idleDirChange;
        }
        else if (_idleDirection.y <= -1)
        {
            if (_idleDirection.x > -1)
                _idleDirection.x -= _idleDirChange;
            else
                _idleDirection.y += _idleDirChange;
        }
        else
        {
            if (_idleDirection.x >= 1)
                _idleDirection.y -= _idleDirChange;
            else
                _idleDirection.y += _idleDirChange;
        }
        /**/
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

        if (FFPPScript.Pollution == 100) OnPollutionMax.Invoke();

            switch (_state)
        {
            case RobotBehaviour.IDLE:
                Idle();
                break;

            case RobotBehaviour.FOLLOWCAM:
                FollowCamera();
                break;

            case RobotBehaviour.NARRATE:
                break;

            case RobotBehaviour.MOVETOCAM:
                break;

            case RobotBehaviour.MOVETOIDLE:
                MoveToIdle();
                break;
        }
	}

    public override void RespondSelect()
    {
        //Debug.Log("Selected " + gameObject.name);

        if (FFPPScript.Pollution != 100)
            OnTap.Invoke();
        else
        {
            OnMinigameReady.Invoke();
            switch (LevelStatsScript.Level)
            {
                case 0:
                LoadSceneTest.LoadSceneAdditive(_tutorialMinigameName);
                    break;
                case 1:
                    LoadSceneTest.LoadSceneAdditive(_minigameLevel1Name);
                    break;
                case 2:
                    LoadSceneTest.LoadSceneAdditive(_minigameLevel2Name);
                    break;
                case 3:
                    LoadSceneTest.LoadSceneAdditive(_minigameLevel3Name);
                    break;
                default:

                    Debug.Log("Shit broke yo");
                    break;
            }
        }
    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected");
    }
}