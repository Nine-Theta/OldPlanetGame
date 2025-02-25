using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameScript : MonoBehaviour {
	
	private void Update () {
        if (Input.GetKey(KeyCode.Escape)) ExitGame();
	}

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadLevel(int pSceneToLoad)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pSceneToLoad);
    }
}
