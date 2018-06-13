using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : InteractableScript
{
    private Collider _collider;
    [SerializeField] private float _decayTimer = 7.0f;
    [SerializeField] private CustomEvent OnDecay;
    private static List<BarrelScript> _activeBarrels = new List<BarrelScript>();

    private bool Decaying
    { get { return _decayTimer <= 0.0f; } }

    void Start()
    {
        _collider = GetComponent<Collider>();
        _activeBarrels.Add(this);
    }

    void Update()
    {
        if (!Decaying)
        {
            _decayTimer -= Time.deltaTime;
            if (_decayTimer <= 0.0f)
            {
                Decay();
            }
        }
    }

    private void OnDestroy()
    {
        _activeBarrels.Remove(this);
    }
    
    private void Decay()
    {
        OnDecay.Invoke();
        //Extra logic here
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    private void SetRotation(Vector3 newRot)
    {
        //transform.rotation = Quaternion.Euler(newRot);
    }

    public override void RespondSelect()
    {
        //Debug.Log("Selected");
        _collider.enabled = false;
        SiloScript.RespondToBarrelSelection();
    }

    public override void RespondDeselect()
    {
        //Debug.Log("Deselected");
        _collider.enabled = true;
        SiloScript.RespondToBarrelDeselection();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The amount of active barrels in the scene</returns>
    public static int GetBarrelCount()
    {
        return _activeBarrels.Count;
    }

}
