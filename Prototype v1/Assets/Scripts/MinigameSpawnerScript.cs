using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSpawnerScript : MonoBehaviour
{

    [SerializeField] private float _spawnMinTime = 3.0f;
    [SerializeField] private float _spawnMaxTime = 3.0f;
    [SerializeField] private bool _randomizeDirection = true;
    [Tooltip("Sets spawned nodes' speed to this variable, or randomizes them if 0")]
    [SerializeField]
    private float _speed = 0.0f;
    [SerializeField] private GameObject _prefab;

    private float _currentSpawnTime;

    void Start()
    {
        _currentSpawnTime = _spawnMaxTime;
    }

    private void Update()
    {
        _currentSpawnTime -= Time.unscaledDeltaTime;
        if (_currentSpawnTime <= 0.0f)
        {
            _currentSpawnTime = Random.Range(_spawnMinTime, _spawnMaxTime);
            GameObject newInstance = Instantiate(_prefab, transform);
            if (_randomizeDirection)
            {
                if (newInstance.GetComponent<TapNodeScript>() != null)
                    newInstance.GetComponent<TapNodeScript>().RandomizeVariables();
            }
            if (_speed != 0.0f)
            {
                if (newInstance.GetComponent<TapNodeScript>() != null)
                    newInstance.GetComponent<TapNodeScript>().SetSpeed(_speed);
            }
        }
    }
}
