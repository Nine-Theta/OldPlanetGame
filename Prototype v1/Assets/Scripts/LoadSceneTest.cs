﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTest : MonoBehaviour {
    
    public string SceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
	}

    public void LoadScene()
    {
        //Early return to prevent duplicate scenes
        //if (SceneManager.GetSceneByName(SceneName) != null)
        //    return;
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }

    public static void StaticLoadSceneAdditive(string sceneName)
    {
        //Early return to prevent duplicate scenes
        //if (SceneManager.GetSceneByName(SceneName) != null)
        //    return;
        Time.timeScale = 0;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        //Early return to prevent duplicate scenes
        //if (SceneManager.GetSceneByName(SceneName) != null)
        //    return;
        Time.timeScale = 0;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneNonAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
