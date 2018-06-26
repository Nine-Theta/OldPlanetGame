using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum DifficultyMode { EASY, MEDIUM, HARD }

public class PlayerStats : IComparable<PlayerStats>
{
    //name date time difficulty score achievedlevel feedback(?)

    private string _name = "uniquenameosaurus";
    private int _score = 0;
    private DifficultyMode _difficulty = DifficultyMode.MEDIUM;
    private DateTime _date = DateTime.Today;
    private float _timeOne = 0.0f;
    private float _timeTwo = 0.0f;
    private float _timeThree = 0.0f;
    private float _timeTotal = 0.0f;
    private int _achievedLevel = 0;
    private string _feedback = "Needs more salt";

    public PlayerStats (string pName, int pScore, DifficultyMode pDifficulty)
    {
        _name = pName;
        _score = pScore;
        _difficulty = pDifficulty;
        _date = DateTime.Today;
        _timeOne = 0.0f;
        _timeTwo = 0.0f;
        _timeThree = 0.0f;
        _timeTotal = 0.0f;
        _achievedLevel = 0;
        _feedback = "Needs more salt";
    }

    public PlayerStats(string pStream)
    {
        string[] stats = pStream.Split(',');

        Debug.Log("stream: " + pStream);
        for (int i = 0; i < stats.Length; i++)
        {
            Debug.Log("stats [" +i+"] : " +stats[i]);
        }

        _name = stats[0];
        int.TryParse(stats[1], out _score);
        _difficulty =  (DifficultyMode)Enum.Parse(typeof(DifficultyMode), stats[2]);
        _date = DateTime.Parse(stats[3]);
        _timeTotal = float.Parse(stats[4]);
        _achievedLevel = int.Parse(stats[5]);
        _feedback = stats[6];

        Debug.Log("Created PlayerStats with stats: "+ pStream);
    }

    public string Name
    { get { return _name; } set { _name = value; } }

    public int Score
    { get { return _score; } set { _score = value; } }

    public DifficultyMode Difficulty
    { get { return _difficulty; } set { _difficulty = value; } }

    public DateTime Date
    { get { return _date; } set { _date = value; } }

    public string Day
    { get { return _date.ToShortDateString(); } }

    public float Time
    { get { return _timeTotal; } }

    public float TimeOne
    { get { return _timeOne; } set { _timeOne = value; TotalOfTime(); } }

    public float TimeTwo
    { get { return _timeTwo; } set { _timeTwo = value; TotalOfTime(); } }

    public float TimeThree
    { get { return _timeThree; } set { _timeThree = value; TotalOfTime(); } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public string Feedback
    { get { return _feedback; } set { _feedback = value; } }

    public int CompareTo(PlayerStats pOther)
    {
        if (_score == pOther.Score)
            return (_timeTotal.CompareTo(pOther.Time));
        else
            return (_score.CompareTo(pOther.Score));
    }

    private void TotalOfTime()
    {
        _timeTotal = _timeOne + _timeTwo + _timeThree;
    }

    public override string ToString()
    {
        return _name + "," + _score + "," + _difficulty + "," + _date + "," + _timeTotal + "," + _achievedLevel + "," + _feedback;
    }
}

[RequireComponent(typeof(CompactTracker))]
public class LeaderboardTracker : MonoBehaviour {

    private static LeaderboardTracker _instance;
    public static LeaderboardTracker Instance { get { return _instance; } }

    private int _boardSize = 100;

    private PlayerStats _currentPlayer;

    private List<PlayerStats> _dailyBoardEasy;
    private List<PlayerStats> _dailyBoardMedium;
    private List<PlayerStats> _dailyBoardHard;

    private List<PlayerStats> _overallBoardEasy;
    private List<PlayerStats> _overallBoardMedium;
    private List<PlayerStats> _overallBoardHard = new List<PlayerStats>(10);

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);

        _dailyBoardEasy = new List<PlayerStats>(_boardSize);
        _dailyBoardMedium = new List<PlayerStats>(_boardSize);
        _dailyBoardHard = new List<PlayerStats>(_boardSize);

        _overallBoardEasy = new List<PlayerStats>(_boardSize);
        _overallBoardMedium = new List<PlayerStats>(_boardSize);
        _overallBoardHard = new List<PlayerStats>(_boardSize);

        InitializeBoards();

        //TryAddPlayer(new PlayerStats("GARY OAK", 151, DifficultyMode.HARD));
        //SaveBoardToFile(_overallBoardHard, DifficultyMode.HARD);
    }

    public static bool Exists
    { get{ return Instance != null; } }

    private void Start()
    {
        _currentPlayer = new PlayerStats("NAAM", 1, DifficultyMode.MEDIUM);
    }

    public PlayerStats CurrentPlayer
    {
        get { return _currentPlayer; }
    }

    private void InitializeBoards()
    {
        EmptyBoard(_dailyBoardEasy, DifficultyMode.EASY);
        EmptyBoard(_dailyBoardMedium, DifficultyMode.MEDIUM);
        EmptyBoard(_dailyBoardHard, DifficultyMode.HARD);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_"+DateTime.Today.Year+"_Easy.csv"))
        {
            EmptyBoard(_overallBoardEasy, DifficultyMode.EASY);
            SaveBoardToFile(_overallBoardEasy, DifficultyMode.EASY);
        }
        else
            _overallBoardEasy = ReadBoardFromFile(DifficultyMode.EASY);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Medium.csv"))
        {
            EmptyBoard(_overallBoardMedium, DifficultyMode.MEDIUM);
            SaveBoardToFile(_overallBoardMedium, DifficultyMode.MEDIUM);
        }
        else
            _overallBoardMedium = ReadBoardFromFile(DifficultyMode.MEDIUM);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Hard.csv"))
        {
            EmptyBoard(_overallBoardHard, DifficultyMode.HARD);
            SaveBoardToFile(_overallBoardHard, DifficultyMode.HARD);
        }
        else
            _overallBoardHard = ReadBoardFromFile(DifficultyMode.HARD);
    }

    private void EmptyBoard(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
    {
        pBoard.Clear();

        for (int i = 0; i < pBoard.Capacity; i++)
        {
            //Debug.Log("capacity: "+pBoard.Capacity+" count: " +pBoard.Count);
            //Debug.Log("Added playerStat[" + i + "] to board: " + pBoard);
            pBoard.Insert(0, new PlayerStats("NULL", 0, pDifficulty));
        }
    }
    
    private void TestRun()
    {
        PlayerStats defaultPlayerEasy = new PlayerStats("TheLegend27", 42, DifficultyMode.EASY);
        PlayerStats defaultPlayerEasy2 = new PlayerStats("TheLegend26", 40, DifficultyMode.EASY);
        _overallBoardEasy.Add(defaultPlayerEasy);
        _overallBoardEasy.Add(defaultPlayerEasy2);
        Debug.Log("TestRunSave");
        SaveBoardToFile(_overallBoardEasy, DifficultyMode.EASY);
        Debug.Log("TestRunRead");
        _overallBoardEasy = ReadBoardFromFile(DifficultyMode.EASY);
    }

    public PlayerStats GetPlayerInfo(DifficultyMode pDifficulty, int pPlayerRank, bool pIsDaily = false)
    {
        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                if (pIsDaily) return _dailyBoardEasy[pPlayerRank];
                return _overallBoardEasy[pPlayerRank];

            case DifficultyMode.MEDIUM:
                if (pIsDaily) return _dailyBoardMedium[pPlayerRank];
                return _overallBoardMedium[pPlayerRank];

            case DifficultyMode.HARD:
                if (pIsDaily) return _dailyBoardHard[pPlayerRank];
                return _overallBoardHard[pPlayerRank];
            default:
                return new PlayerStats("TheLegend27", 42, DifficultyMode.EASY); //Will never actually get called, just here to please the error CS0161.
        }
    }

    public void TryAddCurrentPlayer()
    {
        TryAddPlayer(_currentPlayer);
    }

    /// <summary>
    /// Checks if a player is eligible for the leaderboard, and adds them to it if so.
    /// </summary>
    public void TryAddPlayer(PlayerStats pCurrentPlayer)
    {
        switch (pCurrentPlayer.Difficulty)
        {
            case DifficultyMode.EASY:
                if (CheckPlayer(pCurrentPlayer, _dailyBoardEasy))
                {
                    AddPlayer(pCurrentPlayer, _dailyBoardEasy);
                    if (CheckPlayer(pCurrentPlayer, _overallBoardEasy))
                    {
                        AddPlayer(pCurrentPlayer, _overallBoardEasy);
                        SaveBoardToFile(_overallBoardEasy, DifficultyMode.EASY);
                    }
                }
                break;

            case DifficultyMode.MEDIUM:
                if (CheckPlayer(pCurrentPlayer, _dailyBoardMedium))
                {
                    AddPlayer(pCurrentPlayer, _dailyBoardMedium);
                    if (CheckPlayer(pCurrentPlayer, _overallBoardMedium))
                    {
                        AddPlayer(pCurrentPlayer, _overallBoardMedium);
                        SaveBoardToFile(_overallBoardMedium, DifficultyMode.MEDIUM);
                    }
                }
                break;

            case DifficultyMode.HARD:
                if (CheckPlayer(pCurrentPlayer, _dailyBoardHard))
                {
                    AddPlayer(pCurrentPlayer, _dailyBoardHard);
                    if (CheckPlayer(pCurrentPlayer, _overallBoardHard))
                    {
                        AddPlayer(pCurrentPlayer, _overallBoardHard);
                        SaveBoardToFile(_overallBoardHard, DifficultyMode.HARD);
                    }
                }
                break;
        }
    }

    private bool CheckPlayer(PlayerStats pPlayer, List<PlayerStats> pBoard)
    {
        return pBoard[pBoard.Count-1].Score < pPlayer.Score;
    }

    private void AddPlayer(PlayerStats pPlayer, List<PlayerStats> pBoard)
    {
        for(int i = 0; i < pBoard.Count; i++)
        {
            if (pBoard[i].Score < pPlayer.Score)
            {
                pBoard.RemoveAt(pBoard.Capacity-1);
                pBoard.Insert(i, pPlayer);
                return;
            }
        }
    }

    private List<PlayerStats> ReadBoardFromFile(DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Easy.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Medium.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Hard.csv";
                break;
        }

        TextReader reader = new StreamReader(filePath);
        string stream = reader.ReadToEnd();
        string[] players = stream.Split('\n');

        //Debug.Log("full stream: " + stream);

        List<PlayerStats> list = new List<PlayerStats>(_boardSize);

        for (int i = 0; i < players.Length-1; i++)
        {
            list.Add(new PlayerStats(players[i]));
        }

        reader.Close();

        return list;
    }

    private void SaveBoardToFile(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Easy.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Medium.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Hard.csv";
                break;
        }

        TextWriter writer = new StreamWriter(filePath, false);

        foreach(PlayerStats player in pBoard)
        {
            writer.Write(player.ToString() + '\n');
            //Debug.Log("writeline: " + player.ToString());
        }
        writer.Close();
    }
}