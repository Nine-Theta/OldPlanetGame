using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTextureScript : MonoBehaviour
{

    [SerializeField] private Texture[] _textures;
    [SerializeField] private Renderer[] _targets;

    public void ChangeTextureForAll(int pIndex)
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].material.mainTexture = _textures[pIndex];
        }
    }

    public void ChangeTexture(int pTargetIndex, int pTextureIndex)
    {
        _targets[pTextureIndex].material.mainTexture = _textures[pTextureIndex];
    }
}
