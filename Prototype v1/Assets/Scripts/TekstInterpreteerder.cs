using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TekstInterpreteerder : MonoBehaviour {

    private string _name = "";
    [SerializeField] private Text _nameField;
    [SerializeField] private int _maxNameLength = 10;
    [SerializeField] private string _sceneToLoad; 

    void Start () {
		
	}

    public void InputLetter(string pLetter)
    {
        if (pLetter.Length > _maxNameLength) return;
        _name = _name + pLetter;
        _nameField.text = _name;
    }

    public void Delete()
    {
        _name = "";
        _nameField.text = "NAAM";
    }

    public void SetName()
    {
        if (_name == "") _name = "NAAM";
        LeaderboardTracker.Instance.CurrentPlayer.Name = _name;
    }

    public void SetDifficulty(int pDifficulty)
    {
        LeaderboardTracker.Instance.CurrentPlayer.Difficulty = (DifficultyMode)pDifficulty;
    }

    public void LoadLevel()
    {
        //Debug.Log("TODO: Load level here");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoad,UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoad);
    }
}
