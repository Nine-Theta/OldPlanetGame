using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeDelayScript : MonoBehaviour {
    [SerializeField] private float _delay = 0.5f;
    private Button _button;

	private void Start () {
        _button = GetComponentInChildren<Button>();
        _button.interactable = false;
	}
	
	private void Update () {
        _delay -= Time.deltaTime;
        if(_delay <= 0.0f)
        {
            _button.interactable = true;
            this.enabled = false;
        }
	}
}
