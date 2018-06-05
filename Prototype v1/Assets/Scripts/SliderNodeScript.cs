using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Randomizer
{
    public bool useRandomizer = true;
    public float minRange = 0.0f;
    public float maxRange = 5.0f;

    public float GetValue
    {
        get
        { return Random.Range(minRange, maxRange); }
    }
}

public class SliderNodeScript : MonoBehaviour
{
    [SerializeField] private int _score = 5;
    [SerializeField] private bool _goesBack = false;
    [SerializeField] private float _timeOnScreen = 2.5f;
    [SerializeField] private float _delayBeforeShowing = 0.5f;
    [SerializeField] private Randomizer _delayRandomizer;
    [SerializeField] private Randomizer _screenTimeRandomizer;
    [SerializeField] private CustomEvent OnAppear;
    [SerializeField] private CustomEvent OnDone;
    [SerializeField] private CustomEvent OnDisappear;


    private float _distanceReached = 0.0f;
    private bool _goingBack = false;
    private Slider _slider;

    private void Start()
    {
        if (_delayRandomizer.useRandomizer)
        {
            _delayBeforeShowing = _delayRandomizer.GetValue;
        }
        if (_screenTimeRandomizer.useRandomizer)
        {
            _timeOnScreen = _screenTimeRandomizer.GetValue;
        }
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
                if (_distanceReached <= _slider.minValue)
                {
                    MinigameScoreScript.instance.ScorePoints(_score);
                    OnDone.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }
        else if (_slider.value > _distanceReached)
        {
            _slider.value = _distanceReached;
            if (_slider.maxValue == _distanceReached)
            {
                if (_goesBack)
                {
                    _goingBack = true;
                    return;
                }
                else
                {
                    MinigameScoreScript.instance.ScorePoints(_score);
                    OnDone.Invoke();
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}

