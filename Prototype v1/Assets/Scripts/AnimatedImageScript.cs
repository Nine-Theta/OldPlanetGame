﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImageScript : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float _timePerFrame = 0.125f;
    private float _currentTimer;
    private int _currentFrame = 0;

    void Start()
    {
        _currentTimer = _timePerFrame;
    }

    void Update()
    {
        _currentTimer -= Time.deltaTime;
        if(_currentTimer <= 0.0f)
        {
            _currentTimer = _timePerFrame;
            _currentFrame++;
            if (_currentFrame >= sprites.Length)
                _currentFrame = 0;
            GetComponent<Image>().sprite = sprites[_currentFrame]; 
        }
    }
}