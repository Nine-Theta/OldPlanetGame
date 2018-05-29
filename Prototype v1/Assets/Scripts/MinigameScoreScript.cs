using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScoreScript : MonoBehaviour
{
    [SerializeField] private float _minigameDuration = 60.0f;
    [SerializeField] private Text _debugText;

    private float _minigameTimeLeft;
    private int _score;

    public static MinigameScoreScript instance;

    private void Awake()
    {
        instance = this;
        _minigameTimeLeft = _minigameDuration;
    }

    private void Update()
    {
        _minigameTimeLeft -= Time.deltaTime;
        if(_minigameTimeLeft <= 0.0f)
        {
            
        }

        _debugText.text = "Time left: " + Mathf.FloorToInt(_minigameTimeLeft).ToString();
    }

    public int Score
    {
        get { return _score; }
    }

    public void ScorePoints(int value)
    {
        _score += value;
    }
}
