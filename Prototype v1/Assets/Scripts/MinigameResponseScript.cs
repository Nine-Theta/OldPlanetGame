using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameResponseScript : MonoBehaviour
{
    [SerializeField] private CustomEvent ResponseEvent0;
    [SerializeField] private CustomEvent ResponseEvent1;
    [SerializeField] private CustomEvent ResponseEvent2;
    [SerializeField] private CustomEvent ResponseEvent3;


    [SerializeField] private CustomEvent ResponseFailedEvent0;
    [SerializeField] private CustomEvent ResponseFailedEvent1;
    [SerializeField] private CustomEvent ResponseFailedEvent2;
    [SerializeField] private CustomEvent ResponseFailedEvent3;

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
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseEvent0.Invoke();
                }
                break;
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

    public static void MinigameLost()
    {
        switch (LevelStatsScript.Level)
        {
            case 0:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseFailedEvent0.Invoke();
                }
                break;
            case 1:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseFailedEvent1.Invoke();
                }
                break;
            case 2:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseFailedEvent2.Invoke();
                }
                break;
            case 3:
                foreach (GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
                {
                    respondObject.GetComponent<MinigameResponseScript>().ResponseFailedEvent3.Invoke();
                }
                break;
        }
    }
}
