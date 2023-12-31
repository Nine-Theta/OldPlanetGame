﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToStarScript : MonoBehaviour {

    [SerializeField] private RectTransform _starOne;
    [SerializeField] private RectTransform _starTwo;
    [SerializeField] private RectTransform _starThree;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;

    [SerializeField] int _level = 1;

    private void Start()
    {
        CalculateStars();
        SetText();
    }

    private void CalculateStars()
    {
        int score = 0;

        if (LeaderboardTracker.Exists) {
            switch (_level)
            {
                case 1:
                    score = LeaderboardTracker.Instance.CurrentPlayer.ScoreOne;
                    break;
                case 2:
                    score = LeaderboardTracker.Instance.CurrentPlayer.ScoreTwo;
                    break;
                case 3:
                    score = LeaderboardTracker.Instance.CurrentPlayer.ScoreThree;
                    break;
                default:
                    Debug.LogError("Incorrect level for StarScript");
                    break;
            }
        }

        if(score >= 0 && score <= 5) //No stars for you;
        {
            _starOne.gameObject.SetActive(false);
            _starTwo.gameObject.SetActive(false);
            _starThree.gameObject.SetActive(false);
        }
        else if (score > 5 && score <= 25) //Because one is not enough...
        {
            _starOne.gameObject.SetActive(true);
            _starTwo.gameObject.SetActive(false);
            _starThree.gameObject.SetActive(false);
        }
        else if (score > 25 && score <= 45)//And two is too low...
        {
            _starOne.gameObject.SetActive(true);
            _starTwo.gameObject.SetActive(true);
            _starThree.gameObject.SetActive(false);
        }
        else if (score > 50)//It is I, Three Stars.
        {
            _starOne.gameObject.SetActive(true);
            _starTwo.gameObject.SetActive(true);
            _starThree.gameObject.SetActive(true);
        }
    }

    private void SetText()
    {
        if (!LeaderboardTracker.Exists)
        {
            Debug.LogWarning("No LeaderboardTracker present in scene");
            return;
        }
        
        switch (_level)
        {
            case 1:
                _scoreText.text = LeaderboardTracker.Instance.CurrentPlayer.ScoreOne.ToString();
                _timeText.text = Mathf.Round(LeaderboardTracker.Instance.CurrentPlayer.TimeOne).ToString();
                break;
            case 2:
                _scoreText.text = LeaderboardTracker.Instance.CurrentPlayer.ScoreTwo.ToString();
                _timeText.text = Mathf.Round(LeaderboardTracker.Instance.CurrentPlayer.TimeTwo).ToString();
                break;
            case 3:
                _scoreText.text = LeaderboardTracker.Instance.CurrentPlayer.ScoreThree.ToString();
                _timeText.text = Mathf.Round(LeaderboardTracker.Instance.CurrentPlayer.TimeThree).ToString();
                break;
            default:
                Debug.LogError("Incorrect level for StarScript");
                break;
        }
    }
}
