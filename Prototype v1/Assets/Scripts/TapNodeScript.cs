using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

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

    private static Sprite _oneTapCloud;

    private void Start()
    {
        if (_oneTapCloud == null)
        {
            _oneTapCloud = (Sprite)(Resources.Load("cloud", typeof(Sprite)));
        }

        if (_direction.magnitude != 1.0f)
        {
            _direction.Normalize();
        }
    }

    public virtual void OnTap()
    {
        _tapsToKill--;
        if (gameObject.name == "MinigameDoubleTapable(Clone)" && _tapsToKill == 1)
        {
            //GetComponent<Image>().sprite = (Sprite)(AssetDatabase.LoadAssetAtPath("Assets/Particle Effects/cloud.png", typeof(Sprite)));
            GetComponent<Image>().sprite = _oneTapCloud;
        }
        if (_tapsToKill == 0)
        {
            MinigameScoreScript.instance.ScorePoints(_score);
            MinigameScoreScript.instance.CloudPopped(1);
            OnPopped.Invoke();
            //transform.position += new Vector3(0, 0, 5);
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().raycastTarget = false;
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
