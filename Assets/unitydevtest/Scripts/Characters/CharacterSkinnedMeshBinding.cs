using System.Collections.Generic;
using UnityEngine;

namespace JoshBowersDEV.Characters
{
    [System.Serializable]
    public class SkinnedBinding
    {
        public SkinnedBinding()
        {
            BlendShape = "Empty";
            CharacterProperty = "Empty";
            Weight = 1.0f;
        }

        public string BlendShape;
        public string CharacterProperty;
        public float Weight;
    }

    /// <summary>
    /// Used to bind specific <see cref="SkinnedMeshRenderer"/> values to <see cref="CharacterMeshData"/> values based on weighted values.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
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

        [SerializeField]
        private SkinnedMeshRenderer _skinnedMeshRenderer;

        public SkinnedMeshRenderer SkinnedMeshRenderer
        {
            get => _skinnedMeshRenderer;
            set => _skinnedMeshRenderer = value;
        }

        #endregion Properties

        #region Public Variables

        [SerializeField]
        public List<SkinnedBinding> SkinnedBindings;

        #endregion Public Variables

        #region Unity Callbacks

        // Make sure that Variables are assigned immediately
        private void Reset()
        {
            if (SkinnedMeshRenderer == null)
                SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        private void Awake()
        {
            if (SkinnedMeshRenderer == null)
                SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        #endregion Unity Callbacks
    }
}