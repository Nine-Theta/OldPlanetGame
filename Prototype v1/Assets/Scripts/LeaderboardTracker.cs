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
    private float _time = 0.0f;
    private int _achievedLevel = 0;
    private string _feedback = "Needs more salt";

    public PlayerStats (string pName, int pScore, DifficultyMode pDifficulty)
    {
        _name = pName;
        _score = pScore;
        _difficulty = pDifficulty;
        _date = DateTime.Today;
        _time = 0.0f;
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
        _time = float.Parse(stats[4]);
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
    { get { return _time; } set { _time = value; } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public string Feedback
    { get { return _feedback; } set { _feedback = value; } }

    public int CompareTo(PlayerStats pOther)
    {
        if (_score == pOther.Score)
            return (_time.CompareTo(pOther.Time));
        else
            return (_score.CompareTo(pOther.Score));
    }

    public override string ToString()
    {
        return _name + "," + _score + "," + _difficulty + "," + _date + "," + _time + "," + _achievedLevel + "," + _feedback;
    }
}

public class LeaderboardTracker : MonoBehaviour {

    private static LeaderboardTracker _instance;
    public static LeaderboardTracker Instance { get { return _instance; } }

    private int _boardSize = 10;

    private PlayerStats _currentPlayer;

    private List<PlayerStats> _dailyBoardEasy = new List<PlayerStats>(10);
    private List<PlayerStats> _dailyBoardMed = new List<PlayerStats>(10);
    private List<PlayerStats> _dailyBoardHard = new List<PlayerStats>(10);

    private List<PlayerStats> _overallBoardEasy = new List<PlayerStats>(10);
    private List<PlayerStats> _overallBoardMed = new List<PlayerStats>(10);
    private List<PlayerStats> _overallBoardHard = new List<PlayerStats>(10);

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeBoards();

        TryAddPlayer(new PlayerStats("GARY OAK", 9001, DifficultyMode.HARD));
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
        EmptyBoard(_dailyBoardMed, DifficultyMode.MEDIUM);
        EmptyBoard(_dailyBoardHard, DifficultyMode.HARD);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_"+DateTime.Today.Year+"_Easy.csv"))
        {
            EmptyBoard(_overallBoardEasy, DifficultyMode.EASY);
            SaveBoardToFile(_overallBoardEasy, DifficultyMode.EASY);
        }
        else
            ReadBoardFromFile(_overallBoardEasy, DifficultyMode.EASY);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Medium.csv"))
        {
            EmptyBoard(_overallBoardMed, DifficultyMode.MEDIUM);
            SaveBoardToFile(_overallBoardMed, DifficultyMode.MEDIUM);
        }
        else
            ReadBoardFromFile(_overallBoardMed, DifficultyMode.MEDIUM);

        if (!File.Exists(@"/PowerPlanet_Leaderboard_" + DateTime.Today.Year + "_Hard.csv"))
        {
            EmptyBoard(_overallBoardHard, DifficultyMode.HARD);
            SaveBoardToFile(_overallBoardHard, DifficultyMode.HARD);
        }
        else
            ReadBoardFromFile(_overallBoardHard, DifficultyMode.HARD);
    }

    private void EmptyBoard(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
    {
        pBoard.Clear();

        for (int i = 0; i < pBoard.Capacity; i++)
        {
            Debug.Log("capacity: "+pBoard.Capacity+" count: " +pBoard.Count);
            Debug.Log("Added playerStat[" + i + "] to board: " + pBoard);
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
        ReadBoardFromFile(_overallBoardEasy, DifficultyMode.EASY);
    }

    public PlayerStats GetPlayerInfo(DifficultyMode pDifficulty, int pPlayerRank, bool pIsDaily = false)
    {
        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                if (pIsDaily) return _dailyBoardEasy[pPlayerRank];
                return _overallBoardEasy[pPlayerRank];

            case DifficultyMode.MEDIUM:
                if (pIsDaily) return _dailyBoardMed[pPlayerRank];
                return _overallBoardMed[pPlayerRank];

            case DifficultyMode.HARD:
                if (pIsDaily) return _dailyBoardHard[pPlayerRank];
                return _overallBoardHard[pPlayerRank];
            default:
                return new PlayerStats("TheLegend27", 42, DifficultyMode.EASY); //Will never actually get called, just here to please the error CS0161.
        }
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
                        AddPlayer(pCurrentPlayer, _overallBoardEasy);
                }
                break;

            case DifficultyMode.MEDIUM:
                if (CheckPlayer(pCurrentPlayer, _dailyBoardMed))
                {
                    AddPlayer(pCurrentPlayer, _dailyBoardMed);
                    if (CheckPlayer(pCurrentPlayer, _overallBoardMed))
                        AddPlayer(pCurrentPlayer, _overallBoardMed);
                }
                break;

            case DifficultyMode.HARD:
                if (CheckPlayer(pCurrentPlayer, _dailyBoardHard))
                {
                    AddPlayer(pCurrentPlayer, _dailyBoardHard);
                    if (CheckPlayer(pCurrentPlayer, _overallBoardHard))
                        AddPlayer(pCurrentPlayer, _overallBoardHard);
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
            if(pBoard[i].Score < pPlayer.Score)
            {
                pBoard.RemoveAt(pBoard.Capacity-1);
                pBoard.Insert(i, pPlayer);
                return;
            }
        }
    }

    private void ReadBoardFromFile(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
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
        string[] players = stream.Split(';');

        List<PlayerStats> list = new List<PlayerStats>();

        pBoard.Clear();

        for (int i = 0; i < players.Length-1; i++)
        {
            pBoard.Add(new PlayerStats(players[i]));
        }

        reader.Close();
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
            writer.Write(player.ToString()+";");
            Debug.Log("writeline: " + player.ToString());
        }

        writer.Close();
    }
}
