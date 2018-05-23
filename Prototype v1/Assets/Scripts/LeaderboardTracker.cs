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
        _feedback = "";
    }

    public string Name
    { get { return _name; } set { _name = value; } }

    public int Score
    { get { return _score; } set { _score = value; } }

    public DifficultyMode Difficulty
    { get { return _difficulty; } set { _difficulty = value; } }

    public DateTime Date
    { get { return _date; } set { _date = value; } }

    public float Time
    { get { return _time; } set { _time = value; } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public string Feedback
    { get { return _feedback; } set { _feedback = value; } }

    public int CompareTo(PlayerStats pOther)
    { return (_score.CompareTo(pOther.Score)); }

    public override string ToString()
    {
        return _name + ";" + _score + ";" + _difficulty + ";" + _date + ";" + _time + ";" + _achievedLevel + ";" + _feedback;
    }
}

public class LeaderboardTracker : MonoBehaviour {

    private List<PlayerStats> _dailyBoardEasy = new List<PlayerStats>();
    private List<PlayerStats> _dailyBoardMed = new List<PlayerStats>(); 
    private List<PlayerStats> _dailyBoardHard = new List<PlayerStats>();

    private List<PlayerStats> _overallBoardEasy = new List<PlayerStats>();
    private List<PlayerStats> _overallBoardMed = new List<PlayerStats>();
    private List<PlayerStats> _overallBoardHard = new List<PlayerStats>();

    private void Start()
    {
        Debug.Log("start");
        TestRun();

        /*
        PlayerStats defaultPlayerEasy = new PlayerStats("TheLegend27", DifficultyMode.EASY, 42);
        PlayerStats defaultPlayerMed = new PlayerStats("TheLegend27", DifficultyMode.MEDIUM, 42);
        PlayerStats defaultPlayerHard = new PlayerStats("TheLegend27", DifficultyMode.HARD, 42);

        if (_dailyBoardEasy.Count == 0)
            _dailyBoardEasy.Add(defaultPlayerEasy);
        if (_dailyBoardMed.Count == 0)
            _dailyBoardMed.Add(defaultPlayerMed);
        if (_dailyBoardHard.Count == 0)
            _dailyBoardHard.Add(defaultPlayerHard);

        if (_overallBoardEasy.Count == 0)
            _overallBoardEasy.Add(defaultPlayerEasy);
            */

    }
    
    private void TestRun()
    {
        PlayerStats defaultPlayerEasy = new PlayerStats("TheLegend27", DifficultyMode.EASY, 42);
        PlayerStats defaultPlayerEasy2 = new PlayerStats("TheLegend26", DifficultyMode.EASY, 40);
        _overallBoardEasy.Add(defaultPlayerEasy);
        _overallBoardEasy.Add(defaultPlayerEasy2);
        Debug.Log("TestRun");
        SaveBoardToFile(_overallBoardEasy, DifficultyMode.EASY);
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
                pBoard.Insert(i, pPlayer);
                return;
            }
        }
    }

    private void ReadBoardFromFileList(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/EasyBoard.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/MediumBoard.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/HardBoard.csv";
                break;
        }

        TextReader reader = new StreamReader(filePath);
        //string[] players = reader.

    }

    private void SaveBoardToFile(List<PlayerStats> pBoard, DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        Debug.Log("filepath");

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/EasyBoard.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/MediumBoard.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/HardBoard.csv";
                break;
        }

        TextWriter writer = new StreamWriter(filePath, false);

        foreach(PlayerStats player in pBoard)
        {
            writer.WriteLine(player.ToString()+"|");
            Debug.Log("writeline: " + player.ToString());
        }

        writer.Close();
        //File.WriteAllText(filePath, nom + ";" + prenom + ";" + age + ";" + classe,);
    }
	
	private void Update () {
		
	}
}
