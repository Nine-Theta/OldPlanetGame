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
        int score = 0;
        if (!LeaderboardTracker.Exists)
        {
            Debug.LogWarning("No LeaderboardTracker present in scene");
            return;
        }

        if (_timer <= pTimeForMaxScore)
        {
            score = _maxScore;
        }
        else if(_timer > pTimeForNoScore)
        {
            score = 0;
        }
        else
        {
            float deltaStarTime = pTimeForNoScore - pTimeForMaxScore;
            score = Mathf.CeilToInt(((deltaStarTime - _timer) / deltaStarTime) * (_maxScore - 1));
        }

        switch (_currentLevel)
        {
            case 1:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreOne += score;
                break;
            case 2:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreTwo += score;
                break;
            case 3:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreThree += score;
                break;
            default:
                Debug.LogError("Incorrect level for ScoreTimer");
                break;
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
                LeaderboardTracker.Instance.CurrentPlayer.TimeOne = _timer;
                break;
            case 2:
                AddScore(_timeForMaxScoreLevelTwo, _timeForNoScoreLevelTwo);
                LeaderboardTracker.Instance.CurrentPlayer.TimeTwo = _timer;
                break;
            case 3:
                AddScore(_timeForMaxScoreLevelThree, _timeForNoScoreLevelThree);
                LeaderboardTracker.Instance.CurrentPlayer.TimeThree = _timer;
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
