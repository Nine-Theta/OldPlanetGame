using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSpawnerScript : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 3.0f;
    [SerializeField] private bool _randomizeSpawnTime = false;
    [SerializeField] private bool _randomizeDirection = true;
    [Tooltip("Sets spawned nodes' speed to this variable, or randomizes them if 0")]
    [SerializeField] private float _speed = 0.0f; 
    [SerializeField] private GameObject _prefab;

    private float _currentSpawnTime;

    void Start()
    {
        _currentSpawnTime = _spawnTime;
    }

    void Update()
    {
        _currentSpawnTime -= Time.deltaTime;
        if (_currentSpawnTime <= 0.0f)
        {
            if (_randomizeSpawnTime)
                _currentSpawnTime = Random.Range(0.0f, _spawnTime);
            else
                _currentSpawnTime = _spawnTime;
            GameObject newInstance = Instantiate(_prefab, transform);
            if (_randomizeDirection)
                newInstance.GetComponent<TapNodeScript>().RandomizeVariables();
            if (_speed != 0.0f)
                newInstance.GetComponent<TapNodeScript>().SetSpeed(_speed);
        }
    }
}
