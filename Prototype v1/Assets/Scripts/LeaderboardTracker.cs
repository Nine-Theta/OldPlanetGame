using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyMode { EASY, MEDIUM, HARD }

public class PlayerStats : MonoBehaviour, IComparable<PlayerStats>
{
    //name date time difficulty score achievedlevel feedback(?)

    private string _name = "uniquenameosaurus";
    private DateTime _date = DateTime.Today;
    private float _time = 0.0f;
    private DifficultyMode _difficulty = DifficultyMode.MEDIUM;
    private int _score = 0;
    private int _achievedLevel;
    private string _feedback = "Needs more salt";

    public string Name
    { get { return _name; } set { _name = value; } }

    public DateTime Date
    { get { return _date; } set { _date = value; } }

    public float Time
    { get { return _time; } set { _time = value; } }

    public DifficultyMode Difficulty
    { get { return _difficulty; } set { _difficulty = value; } }

    public int Score
    { get { return _score; } set { _score = value; } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public string Feedback
    { get { return _feedback; } set { _feedback = value; } }

    public int CompareTo(PlayerStats pOther)
    { return (_score.CompareTo(pOther.Score)); }
}

public class LeaderboardTracker : MonoBehaviour {

    private List<PlayerStats> _dailyBoardEasy = new List<PlayerStats>();
    private List<PlayerStats> _dailyBoardMedium = new List<PlayerStats>(); 
    private List<PlayerStats> _dailyBoardHard = new List<PlayerStats>();

    private List<PlayerStats> _OverallBoardEasy = new List<PlayerStats>();
    private List<PlayerStats> _OverallBoardMedium = new List<PlayerStats>();
    private List<PlayerStats> _OverallBoardHard = new List<PlayerStats>();
    
    /// <summary>
    /// Checks if a player is eligible for the leaderboard, and adds them to it if so.
    /// </summary>
    public void AddPlayer(PlayerStats pCurrentPlayer)
    {
        switch (pCurrentPlayer.Difficulty)
        {
            case DifficultyMode.EASY:

                break;

            case DifficultyMode.MEDIUM:
                break;

            case DifficultyMode.HARD:
                break;
        }
    }

    private bool CheckPlayer(PlayerStats pPlayer, List<PlayerStats> pBoard)
    {
        return pBoard[pBoard.Count-1].Score < pPlayer.Score;
    }



    private void Start () {
		
	}
	
	private void Update () {
		
	}
}
