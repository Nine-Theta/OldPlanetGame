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
        _minigameTimeLeft -= Time.unscaledDeltaTime;
        if(_minigameTimeLeft <= 0.0f)
        {
            AddScoreToPlayer();
            GameObject.Find("Cloud_PS").SetActive(false);
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Minigame");
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

    public void AddScoreToPlayer()
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += _score;
        }
    }
}
