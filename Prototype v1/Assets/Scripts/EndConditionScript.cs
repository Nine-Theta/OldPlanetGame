using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConditionScript : MonoBehaviour
{
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    private static EndConditionScript instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Update()
    {

    }

    public static void WinGame()
    {
        instance.WinScreen.SetActive(true);
    }

    public static void LoseGame()
    {
        instance.LoseScreen.SetActive(true);
    }
}
