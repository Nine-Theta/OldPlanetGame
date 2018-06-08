using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameGrowerScript : TapNodeScript
{
    [SerializeField] private float growthRate = 0.01f;
    void Start()
    {

    }

    protected override void Update()
    {
        base.Update();
        transform.localScale += new Vector3(growthRate, growthRate, 0);
        _tapsToKill = Mathf.FloorToInt(transform.localScale.x);
    }

    public override void OnTap()
    {
        base.OnTap();
        transform.localScale -= new Vector3(1, 1, 0);
    }
}
