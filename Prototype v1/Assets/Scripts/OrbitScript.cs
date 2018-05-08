using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour {

    public Rigidbody _focusBody;
    public float speed = 0.1f;

	private void Awake () {
        //_focusBody = gameObject.GetComponent<Rigidbody>();

        //GetComponent<Rigidbody>().AddTorque(0, 1, 0);
        //_focusBody.AddTorque(0,1,0);
        //_focusBody.
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, speed, 0));
    }
}
