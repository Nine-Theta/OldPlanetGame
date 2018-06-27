using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBoardPlayer : MonoBehaviour {

    [SerializeField] private Text[] _playerDisplays = new Text[10];

    [SerializeField] private DifficultyMode _difficulty = 0;
    [SerializeField, Range(0,9)] private int _playerRank = 0;
    [SerializeField] private bool _isDaily = false;

    private void Start()
    {
        ContentSizeFitter[] fitters = gameObject.GetComponentsInChildren<ContentSizeFitter>();
        Debug.Log("fitters length: " + fitters.Length);
        
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("playerdisplays length: " + _playerDisplays.Length);
            _playerDisplays[i] = fitters[i].GetComponentInChildren<Text>();
            PlayerStats p = LeaderboardTracker.Instance.GetPlayerInfo(_difficulty, i, _isDaily);
            _playerDisplays[i].text = (p.Name + " : [" + p.ScoreTotal +"]\nOp: " +p.Day);// LeaderboardTracker.Instance.GetPlayerInfo(_difficulty, i, _isDaily).ToString();
        }


        //PlayerStats p = LeaderboardTracker.Instance.GetPlayerInfo(_difficulty, _playerRank, _isDaily);
        //_playerDisplay.text = p.Name+": ["+p.Score+"]";
        //Debug.Log("dispplay txt: " + p.ToString());
    }

}
