using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTextureScript : MonoBehaviour
{

    [SerializeField] private Texture[] _textures;
    [SerializeField] private Renderer _target;

    public void ChangeTexture(int pIndex)
    {
        _target.material.mainTexture = _textures[pIndex];
    }
}
