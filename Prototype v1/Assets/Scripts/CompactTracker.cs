using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompactStats
{
    private string _name = "uniquenameosaurus";
    private int _score = 0;
    private DifficultyMode _difficulty = DifficultyMode.MEDIUM;
    private int _achievedLevel = 0;
    private DateTime _date = new DateTime(1984,1,1);

    public CompactStats(string pName, int pScore, DifficultyMode pDifficulty)
    {
        _name = pName;
        _score = pScore;
        _difficulty = pDifficulty;
        _date = DateTime.Today;
    }

    public CompactStats(string pStream)
    {
        string[] stats = pStream.Split(',');

        _name = stats[0];
        int.TryParse(stats[1], out _score);
        _difficulty = (DifficultyMode)Enum.Parse(typeof(DifficultyMode), stats[2]);
        _achievedLevel = int.Parse(stats[3]);
        _date = DateTime.Parse(stats[4]);

        //Do (Game); If(DoesNotWork) DoWork();
    }

    public CompactStats(PlayerStats pPlayer)
    {
        _name = pPlayer.Name;
        _score = pPlayer.Score;
        _difficulty = pPlayer.Difficulty;
        _date = pPlayer.Date;
    }

    public string Name
    { get { return _name; } set { _name = value; } }

    public int Score
    { get { return _score; } set { _score = value; } }

    public DifficultyMode Difficulty
    { get { return _difficulty; } set { _difficulty = value; } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public DateTime Date
    { get { return _date; } set { _date = value; } }

    public string Day
    { get { return _date.ToShortDateString(); } }

    public override string ToString()
    {
        return _name + "," + _score + "," + _difficulty + "," + _achievedLevel;
    }
}

public class CompactTracker : MonoBehaviour
{
    private static CompactTracker _instance;
    public static CompactTracker Instance { get { return _instance; } }

    private int _queueMaxSize = 400;

    private Queue<CompactStats> _easyQueue = new Queue<CompactStats>();
    private Queue<CompactStats> _mediumQueue = new Queue<CompactStats>();
    private Queue<CompactStats> _hardQueue = new Queue<CompactStats>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        InitializeQueues();
    }

    private void InitializeQueues()
    {
        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Easy.csv"))
            SaveQueueToFile(_easyQueue, DifficultyMode.EASY);
        else
            _easyQueue = ReadQueueFromFile(DifficultyMode.EASY);

        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Medium.csv"))
            SaveQueueToFile(_mediumQueue, DifficultyMode.MEDIUM);
        else
            _mediumQueue = ReadQueueFromFile(DifficultyMode.MEDIUM);

        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Hard.csv"))
            SaveQueueToFile(_hardQueue, DifficultyMode.HARD);
        else
            _hardQueue = ReadQueueFromFile(DifficultyMode.HARD);
    }

    public int GetAverageScore(DifficultyMode pDifficulty)
    {
        int x = 0;

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                if (_easyQueue.Count == 0)
                    return 0;
                foreach(CompactStats player in _easyQueue)
                    x += player.Score;
                return (x / _easyQueue.Count);

            case DifficultyMode.MEDIUM:
                if (_mediumQueue.Count == 0)
                    return 0;
                foreach (CompactStats player in _mediumQueue)
                    x += player.Score;
                return (x / _mediumQueue.Count);

            case DifficultyMode.HARD:
                if (_hardQueue.Count == 0)
                    return 0;
                foreach (CompactStats player in _hardQueue)
                    x += player.Score;
                return (x / _hardQueue.Count);
            default:
                return x;
        }
    }

    public void AddPlayer(CompactStats pPlayer)
    {
        switch (pPlayer.Difficulty)
        {
            case DifficultyMode.EASY:
                if (_easyQueue.Count == _queueMaxSize)
                    _easyQueue.Dequeue();
                _easyQueue.Enqueue(pPlayer);
                SaveQueueToFile(_easyQueue, DifficultyMode.EASY);
                break;
            case DifficultyMode.MEDIUM:
                if (_mediumQueue.Count == _queueMaxSize)
                    _mediumQueue.Dequeue();
                _mediumQueue.Enqueue(pPlayer);
                SaveQueueToFile(_mediumQueue, DifficultyMode.MEDIUM);
                break;
            case DifficultyMode.HARD:
                if (_hardQueue.Count == _queueMaxSize)
                    _hardQueue.Dequeue();
                _hardQueue.Enqueue(pPlayer);
                SaveQueueToFile(_hardQueue, DifficultyMode.HARD);
                break;
        }
    }

    private Queue<CompactStats> ReadQueueFromFile(DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/PowerPlanet_RecentPlayers_Easy.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/PowerPlanet_RecentPlayers_Medium.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/PowerPlanet_RecentPlayers_Hard.csv";
                break;
        }

        TextReader reader = new StreamReader(filePath);
        string stream = reader.ReadToEnd();
        string[] players = stream.Split('\n');

        Queue<CompactStats> queue = new Queue<CompactStats>();

        for (int i = 0; i < players.Length - 1; i++)
        {
            queue.Enqueue(new CompactStats(players[i]));
        }

        reader.Close();

        return queue;
    }

    private void SaveQueueToFile(Queue<CompactStats> pQueue, DifficultyMode pDifficulty)
    {
        string filePath = @"/Default.csv";

        switch (pDifficulty)
        {
            case DifficultyMode.EASY:
                filePath = @"/PowerPlanet_RecentPlayers_Easy.csv";
                break;
            case DifficultyMode.MEDIUM:
                filePath = @"/PowerPlanet_RecentPlayers_Medium.csv";
                break;
            case DifficultyMode.HARD:
                filePath = @"/PowerPlanet_RecentPlayers_Hard.csv";
                break;
        }

        TextWriter writer = new StreamWriter(filePath, false);

        foreach (CompactStats player in pQueue)
        {
            writer.Write(player.ToString() + '\n');
            //Debug.Log("Queue writeline: " + player.ToString());
        }
        writer.Close();
    }
}