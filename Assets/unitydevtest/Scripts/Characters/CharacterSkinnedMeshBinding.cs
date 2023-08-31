using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    [Binding]
    [System.Serializable]
    public class SkinnedBinding : BindableBase
    {
        public SkinnedBinding()
        {
            BlendShape = "Empty";
            CharacterProperty = " ";
            Weight = 1.0f;
            CurrentValue = 0;
        }

        public string BlendShape;
        public string CharacterProperty;
        public float Weight;

        [SerializeField]
        private float _currentValue;

        [Binding]
        public float CurrentValue
        {
            get => _currentValue;
            set { SetProperty(ref _currentValue, value); }
        }

        [Tooltip("Use true if the property is -1,1 in order to combine properties")]
        public bool IsInvertedValue;
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
                // Assign the new character data
                _characterMeshData = value;
                try
                {
                    // Clear previous event handler if there is one.
                    _characterMeshData.PropertyChangedEvent = null;

                    // Subscribe to the property changed event.
                    value.PropertyChangedEvent += HandlePropertyChange;

                    // Go ahead and update each Blend Shape to the new character.
                    for (int i = 0; i < SkinnedBindings.Count; i++)
                    {
                        SetBlendShapeWeight(SkinnedBindings[i].BlendShape, SkinnedBindings[i].CurrentValue);
                    }

                    Debug.Log("CharacterMeshData was set successfully.");
                }
                catch (System.Exception)
                {
                    Debug.Log("CharacterMeshData was null");
                }
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
        [Binding]
        public List<SkinnedBinding> SkinnedBindings { get; set; }

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

        #region Private Methods

        private void HandlePropertyChange(string propertyName, float newValue)
        {
            foreach (var skin in SkinnedBindings)
            {
                if (propertyName == skin.BlendShape)
                {
                    // Update the current value.
                    skin.CurrentValue = newValue;

                    // Set the blend shape weight.
                    SetBlendShapeWeight(skin.BlendShape, skin.CurrentValue);
                }
            }
        }

        private void SetBlendShapeWeight(string blendShape, float currentValue)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape), currentValue);
        }

        #endregion Private Methods
    }
}