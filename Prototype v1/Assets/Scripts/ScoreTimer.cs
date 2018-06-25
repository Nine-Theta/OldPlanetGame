using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTimer : MonoBehaviour {
    [Header("Level 1")]
    [SerializeField, Tooltip("In seconds")] private float _timeForThreeStarsLevelOne = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoStarsLevelOne = 300; //5min

    [Header("Level 2")]
    [SerializeField, Tooltip("In seconds")] private float _timeForThreeStarsLevelTwo = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoStarsLevelTwo = 300; //5min

    [Header("Level 3")]
    [SerializeField, Tooltip("In seconds")] private float _timeForThreeStarsLevelThree = 60;
    [SerializeField, Tooltip("In seconds")] private float _timeForNoStarsLevelThree = 300; //5min

    private float _timer = 0;

    private int _maxScore = 30;

    private bool _timerRunning = false;

    private int _currentLevel = 1;

    private void Start () {
		
	}

    private void AddScore(float pTimeForThreeStars, float pTimeForNoStars)
    {
        if (_timer <= pTimeForThreeStars)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += _maxScore;
        }
        else if(_timer > pTimeForNoStars)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += 0;
        }
        else
        {
            float deltaStarTime = pTimeForNoStars - pTimeForThreeStars;
            int score = Mathf.CeilToInt(((deltaStarTime - (_timer - pTimeForThreeStars)) / (deltaStarTime)) * (_maxScore - 1));
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
                AddScore(_timeForThreeStarsLevelOne, _timeForNoStarsLevelOne);
                break;
            case 2:
                AddScore(_timeForThreeStarsLevelTwo, _timeForNoStarsLevelTwo);
                break;
            case 3:
                AddScore(_timeForThreeStarsLevelThree, _timeForNoStarsLevelThree);
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
