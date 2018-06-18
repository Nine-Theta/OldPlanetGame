using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConditionScript : MonoBehaviour
{
    [SerializeField] private CustomEvent OnLevel1Complete;
    [SerializeField] private CustomEvent OnLevel1Lost;
    [SerializeField] private CustomEvent OnLevel2Complete;
    [SerializeField] private CustomEvent OnLevel2Lost;
    [SerializeField] private CustomEvent OnLevel3Complete;
    [SerializeField] private CustomEvent OnLevel3Lost;

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

    public static void WinLevel()
    {
        switch (LevelStatsScript.Level)
        {
            //goto defaults for debugging and previously for setlevel, which is now to be called from the button
            case 1:
                instance.OnLevel1Complete.Invoke();
                goto default;
            case 2:
                instance.OnLevel2Complete.Invoke();
                goto default;
            case 3:
                instance.OnLevel3Complete.Invoke();
                goto default;

            default:
                //LevelStatsScript.SetLevel();
                //Debug.Log("Level up!");
                break;
        }
    }


    public static void LoseLevel()
    {
        switch (LevelStatsScript.Level)
        {
            case 1:
                instance.OnLevel1Lost.Invoke();
                goto default;
            case 2:
                instance.OnLevel2Lost.Invoke();
                goto default;
            case 3:
                instance.OnLevel3Lost.Invoke();
                goto default;

            default:
                break;
        }
    }
}
