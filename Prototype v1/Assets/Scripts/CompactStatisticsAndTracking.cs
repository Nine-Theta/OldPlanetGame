using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompactStats : MonoBehaviour
{
    private string _name = "uniquenameosaurus";
    private int _score = 0;
    private DifficultyMode _difficulty = DifficultyMode.MEDIUM;
    private int _achievedLevel = 0;

    public CompactStats(string pName, int pScore, DifficultyMode pDifficulty)
    {
        _name = pName;
        _score = pScore;
        _difficulty = pDifficulty;
    }

    public CompactStats(string pStream)
    {
        //Do (Game); If(DoesNotWork) DoWork();
    }

    public string Name
    { get { return _name; } set { _name = value; } }

    public int Score
    { get { return _score; } set { _score = value; } }

    public DifficultyMode Difficulty
    { get { return _difficulty; } set { _difficulty = value; } }

    public int AchievedLevel
    { get { return _achievedLevel; } set { _achievedLevel = value; } }

    public override string ToString()
    {
        return _name + "," + _score + "," + _difficulty + "," + _achievedLevel;
    }
}

public class CompactTracker : MonoBehaviour
{
    private int _queueMaxSize = 420;

    private Queue<CompactStats> _easyQueue = new Queue<CompactStats>();
    private Queue<CompactStats> _mediumQueue = new Queue<CompactStats>();
    private Queue<CompactStats> _hardQueue = new Queue<CompactStats>();

    private void InitializeQueues()
    {
        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Easy.csv"))
        {
            SaveQueueToFile(_easyQueue, DifficultyMode.EASY);
        }
        else
            _easyQueue = ReadQueueFromFile(DifficultyMode.EASY);

        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Medium.csv"))
        {
            SaveQueueToFile(_mediumQueue, DifficultyMode.MEDIUM);
        }
        else
            _mediumQueue = ReadQueueFromFile(DifficultyMode.MEDIUM);

        if (!File.Exists(@"/PowerPlanet_RecentPlayers_Hard.csv"))
        {
            SaveQueueToFile(_hardQueue, DifficultyMode.HARD);
        }
        else
            _hardQueue = ReadQueueFromFile(DifficultyMode.HARD);
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

        Debug.Log("full stream: " + stream);

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
            writer.WriteLine(player.ToString());
            Debug.Log("Queue writeline: " + player.ToString());
        }
        writer.Close();
    }
}