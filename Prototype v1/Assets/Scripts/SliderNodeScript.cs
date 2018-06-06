using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderNodeScript : MonoBehaviour
{
    [SerializeField] private int _score = 5;
    [SerializeField] private bool _goesBack = false;
    [SerializeField] private float _timeOnScreen = 2.5f;
    [SerializeField] private float _rotation = 0.0f;
    [SerializeField] private CustomEvent OnAppear;
    [SerializeField] private CustomEvent OnDone;
    [SerializeField] private CustomEvent OnDisappear;


    private float _distanceReached = 0.0f;
    private Scrollbar _slider;

    private void Start()
    {
        _rotation = Random.Range(0.0f, 360.0f);
        transform.rotation = Quaternion.Euler(0, 0, _rotation);
        _slider = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        _timeOnScreen -= Time.deltaTime;
        if (_timeOnScreen <= 0.0f)
        {
            gameObject.SetActive(false);
            OnDisappear.Invoke();
        }
    }

    public void CheckSlider()
    {
        if (_slider.value > _distanceReached)
        {
            _distanceReached = _slider.value;
            if (_slider.value >= 1.0f)
            {
                MinigameScoreScript.instance.ScorePoints(_score);
                OnDone.Invoke();
                gameObject.SetActive(false);
                return;
            }
        }
    }
}

