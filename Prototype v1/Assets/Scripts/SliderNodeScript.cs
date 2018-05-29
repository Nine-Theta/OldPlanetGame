using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNodeScript : MonoBehaviour
{
    [SerializeField] private float _score = 5.0f;
    [SerializeField] private bool _goesBack = false;
    [SerializeField] private float _timeOnScreen = 2.5f;
    [SerializeField] private float _delayBeforeShowing = 0.5f;
    [SerializeField] private CustomEvent OnAppear;
    [SerializeField] private CustomEvent OnDisappear;


    private float _distanceReached = 0.0f;
    private bool _goingBack = false;
    private Slider _slider;

    private void Start()
    {

    }

    private void Update()
    {
        if (_delayBeforeShowing > 0.0f)
        {
            _delayBeforeShowing -= Time.deltaTime;
            if (_delayBeforeShowing <= 0.0f)
            {
                OnAppear.Invoke();
            }
        }
        else
        {
            _timeOnScreen -= Time.deltaTime;
            if (_timeOnScreen <= 0.0f)
            {
                gameObject.SetActive(false);
                OnDisappear.Invoke();
            }
        }
    }

    public void CheckSlider()
    {
        if (_goingBack)
        {
            if (_slider.value < _distanceReached)
            {
                _distanceReached = _slider.value;
            }
        }
        else if (_slider.value > _distanceReached)
        {
            _slider.value = _distanceReached;
            if(_slider.maxValue == _distanceReached && _goesBack)
            {
                _goingBack = true;
                return;
            }
        }
        
    }
}
