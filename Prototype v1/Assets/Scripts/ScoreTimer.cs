using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTimer : MonoBehaviour {
    [Header("Level 1")]
    [SerializeField, Tooltip("In seconds")] private float _timeForMaxScoreLevelOne = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoScoreLevelOne = 300; //5min

    [Header("Level 2")]
    [SerializeField, Tooltip("In seconds")] private float _timeForMaxScoreLevelTwo = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoScoreLevelTwo = 300; //5min

    [Header("Level 3")]
    [SerializeField, Tooltip("In seconds")] private float _timeForMaxScoreLevelThree = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoScoreLevelThree = 300; //5min

    private float _timer = 0;

    private int _maxScore = 30;

    private bool _timerRunning = false;

    private int _currentLevel = 1;

    private void AddScore(float pTimeForMaxScore, float pTimeForNoScore)
    {
        if (_timer <= pTimeForMaxScore)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += _maxScore;
        }
        else if(_timer > pTimeForNoScore)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += 0;
        }
        else
        {
            float deltaStarTime = pTimeForNoScore - pTimeForMaxScore;
            int score = Mathf.CeilToInt(((deltaStarTime - (_timer - pTimeForMaxScore)) / (deltaStarTime)) * (_maxScore - 1));
            LeaderboardTracker.Instance.CurrentPlayer.Score += score;
        }
    }

    public void StartTimer(int pLevel)
    {
        _currentLevel = pLevel;
        _timer = 0;
        _timerRunning = true;
    }
	
    public void EndTimer()
    {
        _timerRunning = false;
        switch (_currentLevel)
        {
            case 1:
                AddScore(_timeForMaxScoreLevelOne, _timeForNoScoreLevelOne);
                break;
            case 2:
                AddScore(_timeForMaxScoreLevelTwo, _timeForNoScoreLevelTwo);
                break;
            case 3:
                AddScore(_timeForMaxScoreLevelThree, _timeForNoScoreLevelThree);
                break;
            default:
                Debug.LogError("Incorrect level for ScoreTimer");
                break;
        }
    }
    
	private void Update () {

        if (_timerRunning)
        {
            _timer += Time.deltaTime;
        }
	}
}
