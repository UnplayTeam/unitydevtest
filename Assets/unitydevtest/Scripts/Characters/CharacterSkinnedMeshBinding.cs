using JoshBowersDEV.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinnedMeshBinding : MonoBehaviour
{
    #region Properties

    private CharacterMeshData _characterMeshData;

    public CharacterMeshData CharacterMeshData
    {
        get => _characterMeshData;
        set
        {
            _characterMeshData = value;
        }
    }

    #endregion Properties
}