using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapNodeScript : MonoBehaviour
{
    [SerializeField] private int _score = 5;
    [SerializeField] private float _timeOnScreen = 2.5f;
    [SerializeField] private float _randomMin = 2.0f;
    [SerializeField] private float _randomMax = 5.0f;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] protected int _tapsToKill = 1;
    [SerializeField] private Vector2 _direction = new Vector2(1, 0);
    [SerializeField] private CustomEvent OnPopped;
    [SerializeField] private CustomEvent OnDisappear;


    private void Start()
    {
        if (_direction.magnitude != 1.0f)
        {
            _direction.Normalize();
        }
    }

    public virtual void OnTap()
    {
        _tapsToKill--;
        if (_tapsToKill == 0)
        {
            MinigameScoreScript.instance.ScorePoints(_score);
            MinigameScoreScript.instance.CloudPopped(1);
            OnPopped.Invoke();
            TapNodeScript[] children = gameObject.GetComponentsInChildren<TapNodeScript>(true);
            if (children.Length > 0)
            {
                foreach (TapNodeScript child in children)
                {
                    child.gameObject.SetActive(true);
                    child.transform.SetParent(transform.parent);
                }
                MinigameScoreScript.instance.CloudSpawned(children.Length);
            }
        }
    }

    protected virtual void Update()
    {
        _timeOnScreen -= Time.deltaTime;
        if (_timeOnScreen <= 0.0f)
        {
            gameObject.SetActive(false);
            OnDisappear.Invoke();
        }

        transform.position += (Vector3)((_direction * _speed));
    }

    public void RandomizeDirection()
    {
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
