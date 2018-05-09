using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //The orbiting body also requires a RigidBody in most cases.
public class OrbitScript : MonoBehaviour {

    private Rigidbody _focusBody;

    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Vector3 _direction = Vector3.zero;

	private void Start () {
        _focusBody = GetComponent<Rigidbody>();
        _focusBody.AddRelativeTorque(_direction.normalized * _speed);
    }
}
