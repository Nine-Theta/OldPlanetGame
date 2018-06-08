using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapNodeScript : MonoBehaviour
{
    [SerializeField] private int _score = 5;
    [SerializeField] private float _timeOnScreen = 2.5f;
    [SerializeField] private float _delayBeforeShowing = 0.5f;
    [SerializeField] private float _randomMin = 2.0f;
    [SerializeField] private float _randomMax = 5.0f;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private Vector2 _direction = new Vector2(1, 0);
    [SerializeField] private CustomEvent OnAppear;
    [SerializeField] private CustomEvent OnDisappear;

    private Button _button;
    private Image _image;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    public void OnTap()
    {
        MinigameScoreScript.instance.ScorePoints(_score);
    }

    private void Update()
    {
        if(_delayBeforeShowing > 0.0f)
        {
            _delayBeforeShowing -= Time.deltaTime;
            if(_delayBeforeShowing <= 0.0f)
            {
                _button.enabled = true;
                _image.enabled = true;
                OnAppear.Invoke();
            }
        }
        else
        {
            _timeOnScreen -= Time.deltaTime;
            if(_timeOnScreen <= 0.0f)
            {
                gameObject.SetActive(false);
                OnDisappear.Invoke();
            }
        }
        transform.position += (Vector3)((_direction * _speed));
    }

    public void RandomizeDirection(bool noDelay = true)
    {
        if (noDelay)
            _delayBeforeShowing = 0.001f;
        Vector3 randomDir = Random.onUnitSphere;
        randomDir.z = 0;
        _direction = randomDir.normalized;
        _speed = Random.Range(_randomMin, _randomMax);
    }

    public void SetDirection(Vector3 pDir)
    {
        _direction = pDir;
    }

    public void SetSpeed(float pSpeed)
    {
        _speed = pSpeed;
    }
}
