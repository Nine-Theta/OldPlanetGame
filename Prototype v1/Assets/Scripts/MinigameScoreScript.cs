using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScoreScript : MonoBehaviour
{
    [SerializeField] private float _minigameDuration = 60.0f;
    [SerializeField] private string _sceneName = "Minigame";
    [SerializeField] private Text _debugText;
    [SerializeField] private RectTransform _cloudBar;

    private float _minigameTimeLeft;
    private int _score = 0;
    private int _totalCloudsSpawned = 0;
    private int _totalCloudsPopped = 0;
    [SerializeField] private int _minCloudsSpawned = 32; //magic value for now, should be made to be gotten from some other script at some later date.
    [SerializeField] private float _screenStartPos = 454.5f;

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

        if (_minCloudsSpawned >= _totalCloudsPopped)
        {
            _cloudBar.position = new Vector3(_cloudBar.position.x, (((float)(_minCloudsSpawned - _totalCloudsPopped) / _minCloudsSpawned) * (_screenStartPos * 2)) - _screenStartPos, 0);
            //Debug.Log("y: "+_cloudBar.position.y+" mincloudspawned: " + _minCloudsSpawned + " ScreenStartPos: " + _screenStartPos + " totalcloudspopped: " + _totalCloudsPopped + "Where it should be: " + ((((_minCloudsSpawned - _totalCloudsPopped) / _minCloudsSpawned) * (_screenStartPos * 2)) - _screenStartPos));
        }

        //_debugText.text = "Time left: " + Mathf.FloorToInt(_minigameTimeLeft).ToString();
    }

    private void EndMinigame()
    {
        if (_totalCloudsPopped >= _minCloudsSpawned)
            _score = 20;
        else if (_totalCloudsPopped > _minPoppedForClear)
            _score = 10 + ((_totalCloudsPopped - _minPoppedForClear) / (_minCloudsSpawned - _minPoppedForClear) * 10);
        else
            _score = 0;
        AddScoreToPlayer();
        if (GameObject.Find("Clouds_PS") != null)
            GameObject.Find("Clouds_PS").SetActive(false);
        Time.timeScale = 1;
        if (MinimumCloudsCleared())
            MinigameResponseScript.MinigameWon();
        else
            MinigameResponseScript.MinigameLost();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_sceneName);
    }

    public int Score
    {
        get { return _score; }
    }

    public float GetPercentageCloudsCleared()
    {
        return ((float)_totalCloudsPopped / (float)_totalCloudsSpawned) * 100;
    }

    public bool MinimumCloudsCleared()
    {
        return _totalCloudsSpawned > _minPoppedForClear;
    }

    public void ScorePoints(int value)
    {
        Debug.Log("[MinigameScoreScript]: ScorePoints is discontinued");
        //_score += value;
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
        if (!LeaderboardTracker.Exists)
        {
            Debug.LogWarning("No LeaderboardTracker present in scene");
            return;
        }

        if (!LevelStatsScript.Exists)
        {
            Debug.LogWarning("No LevelStatsScript present in scene");
            return;
        }

        switch (LevelStatsScript.Level)
        {
            case 0:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreOne += _score;
                break;
            case 1:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreTwo += _score;
                break;
            case 2:
                LeaderboardTracker.Instance.CurrentPlayer.ScoreThree += _score;
                break;
            default:
                break;
        }
    }

    public void SkipMinigame()
    {
        EndMinigame();
    }
}
