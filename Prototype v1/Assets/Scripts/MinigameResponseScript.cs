using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameResponseScript : MonoBehaviour
{
    [SerializeField] private CustomEvent ResponseEvent1;
    [SerializeField] private CustomEvent ResponseEvent2;
    [SerializeField] private CustomEvent ResponseEvent3;

    void Start()
    {

    }

    void Update()
    {

    }

    public static void MinigameWon()
    {
        switch (LevelStatsScript.Level)
        {
            case 0:
                goto case 1;
            case 1:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseEvent1.Invoke();
                }
                break;
            case 2:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseEvent2.Invoke();
                }
                break;
            case 3:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseEvent3.Invoke();
                }
                break;
        }
    }
}
