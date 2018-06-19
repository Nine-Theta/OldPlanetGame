using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScoreScript : MonoBehaviour
{
    [SerializeField] private float _minigameDuration = 60.0f;
    [SerializeField] private string _sceneName = "Minigame";
    [SerializeField] private Text _debugText;

    private float _minigameTimeLeft;
    private int _score;
    private int _totalCloudsSpawned = 0;
    private int _totalCloudsPopped = 0;
    private int _minCloudsSpawned = 20; //magic value for now, should be made to be gotten from some other script at some later date.

    [SerializeField, Tooltip("The Minimum amount of Clouds that need to be popped for the minigame to be won")] private int _minPoppedForClear = 15;

    public static MinigameScoreScript instance;

    private void Awake()
    {
        instance = this;
        _minigameTimeLeft = _minigameDuration;
    }

    private void Update()
    {
        _minigameTimeLeft -= Time.unscaledDeltaTime;
        if (_minigameTimeLeft <= 0.0f)
        {
            EndMinigame();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipMinigame();
        }

        _debugText.text = "Time left: " + Mathf.FloorToInt(_minigameTimeLeft).ToString();
    }

    private void EndMinigame()
    {
        AddScoreToPlayer();
        if (GameObject.Find("Clouds_PS") != null)
            GameObject.Find("Clouds_PS").SetActive(false);
        Time.timeScale = 1;
        MinigameResponseScript.MinigameWon();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_sceneName);
    }

    public int Score
    {
        get { return _score; }
    }

    public float GetPercentageCloudsCleared()
    {
        return ((float)_totalCloudsPopped/(float)_totalCloudsSpawned)*100;
    }

    public bool MinimumCloudsCleared()
    {
        return _totalCloudsSpawned > _minPoppedForClear;
    }

    public void ScorePoints(int value)
    {
        _score += value;
    }

    public void CloudSpawned(int pCloudValue)
    {
        _totalCloudsSpawned += pCloudValue;
    }

    public void CloudPopped(int pCloudValue)
    {
        _totalCloudsPopped += pCloudValue;
    }

    public void AddScoreToPlayer()
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.Score += _score;
        }
    }

    public void SkipMinigame()
    {
        EndMinigame();
    }
}
