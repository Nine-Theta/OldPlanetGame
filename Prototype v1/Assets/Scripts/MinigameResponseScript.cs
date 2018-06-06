using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameResponseScript : MonoBehaviour
{
    [SerializeField] private CustomEvent ResponseEvent;


    void Start()
    {

    }

    void Update()
    {

    }

    public static void MinigameWon()
    {
        foreach(GameObject respondObject in GameObject.FindGameObjectsWithTag("Responder"))
        {
            respondObject.GetComponent<MinigameResponseScript>().ResponseEvent.Invoke();
        }
    }
}
