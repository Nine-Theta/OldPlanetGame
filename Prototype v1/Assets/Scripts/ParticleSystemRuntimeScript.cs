using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemRuntimeScript : MonoBehaviour
{
    private ParticleSystem _system;

    private void Start()
    {
        _system = GetComponent<ParticleSystem>();
    }

    public void SetLooping(bool looping)
    {
        //ParticleSystem.MainModule sysMain = _system.main;
        //sysMain.loop = looping;   
#pragma warning disable CS0618 // Type or member is obsolete
        _system.loop = looping;
#pragma warning restore CS0618 // Type or member is obsolete
    }

    public void SetStartColor(Color color)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        _system.startColor = color;
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
