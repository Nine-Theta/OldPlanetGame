using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteHeresyScript : MonoBehaviour
{
    [SerializeField] private CustomEvent[] _events;

    public void CallEventByIndex(int index)
    {
        if (_events[index] != null)
        {
            Debug.Log("Calling event #" + index.ToString() + " and it is " + _events[index].GetPersistentEventCount().ToString() + " actions long");
            _events[index].Invoke();
        }
        else
        {
            Debug.LogWarning("Your heresy shall not continue beyond your own defined heresy!");
            Debug.LogError("AbsoluteHeresyScript's CallEventByIndex has received an index that is out of bounds");
        }
    }
}
