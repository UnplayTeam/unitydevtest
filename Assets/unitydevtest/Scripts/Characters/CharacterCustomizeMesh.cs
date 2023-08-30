using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    /// <summary>
    /// Bindable mono for exposing current Mesh information.
    /// </summary>
    public class CharacterCustomizeMesh : BindableBehaviourBase
    {
        #region Properties

        [SerializeField]
        private CharacterMeshData _characterMeshData;

        [Binding]
        public CharacterMeshData CharacterMeshData
        {
            get => _characterMeshData;
            set => _characterMeshData = value;
        }

        #endregion Properties
    }
}