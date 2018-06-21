using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelScript : InteractableScript
{
    private Collider _collider;
    [SerializeField] private Color _decayColor = Color.red;
    [SerializeField] private float _decayTimer = 7.0f;
    private float _currentTimer = 7.0f;
    [SerializeField] private CustomEvent OnDecay;
    private static List<BarrelScript> _activeBarrels = new List<BarrelScript>();
    private Image _image;

    private bool Decaying
    { get { return _decayTimer <= 0.0f; } }

    void Start()
    {
        _currentTimer = _decayTimer;
        _collider = GetComponent<Collider>();
        _image = GetComponentInChildren<Image>();
        _activeBarrels.Add(this);
    }

    void Update()
    {
        if (!Decaying)
        {
            _currentTimer -= Time.deltaTime;
            _image.fillAmount = (_currentTimer / _decayTimer);
            if (_currentTimer <= 0.0f)
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
        _image.color = _decayColor;
        _image.fillAmount = 1.0f;
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

    public static int GetDecayingBarrelCount()
    {
        int number = 0;
        foreach(BarrelScript barrel in _activeBarrels)
        {
            if(barrel.Decaying)
            {
                number++;
            }
        }
        return number;
    }

}
