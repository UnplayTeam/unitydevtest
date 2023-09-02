using System.Collections.Generic;
using System.Reflection;
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
            CharacterProperty = CharacterProperty.IsHybrid;
            Weight = 1.0f;
            CurrentValue = 0;
        }

        public string BlendShape;
        public CharacterProperty CharacterProperty;
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
    public class CharacterSkinnedMeshBinding : MonoBehaviour, ICharacterCustomizeListener
    {
        #region Properties

        private CharacterMeshData _characterMeshData;

        [Binding]
        public CharacterMeshData CharacterMeshData
        {
            get => _characterMeshData;
            set
            {
                // Remove itself as a listener if we're changing data assets.
                if (_characterMeshData != null)
                    _characterMeshData.RemoveListener(this);

                // Assign the new character data
                _characterMeshData = value;

                try
                {
                    // Subscribe to the property changed event.
                    _characterMeshData.AddListener(this);

                    // Go ahead and update each Blend Shape to the new character.
                    InitializeDataValues();

                    Debug.Log("CharacterMeshData was set successfully.");
                }
                catch (System.Exception e)
                {
                    Debug.Log("CharacterMeshData was null: \n" + e.ToString());
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

        #region Editor Methods

        public void FillSkinnedBindings()
        {
            if (_skinnedMeshRenderer == null)
            {
                Debug.LogError("Skinned Mesh Renderer is missing.", this);
                return; // Return early to avoid further errors.
            }

            int index = _skinnedMeshRenderer.sharedMesh.blendShapeCount;

            // Initialize the SkinnedBindings list with empty SkinnedBinding objects.
            SkinnedBindings = new List<SkinnedBinding>(index);

            for (int i = 0; i < index; i++)
            {
                // Create a new SkinnedBinding instance for each element in the list.
                SkinnedBinding binding = new SkinnedBinding();
                binding.BlendShape = _skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
                SkinnedBindings.Add(binding); // Add the new instance to the list.
            }
        }

        #endregion Editor Methods

        #region Public Variables

        [SerializeField]
        public List<SkinnedBinding> SkinnedBindings = new List<SkinnedBinding>();

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

        // By using reflection, we can don't have to hardcode ourselves to the Character Data objects just in case properties are added/removed/changed
        private void InitializeDataValues()
        {
            PropertyInfo[] properties = CharacterMeshData.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (PropertyInfo property in properties)
            {
                // Get the name of the property
                string propertyName = property.Name;

                // Get the value of the property
                object propertyValue = property.GetValue(CharacterMeshData);
                float convertedValue;
                try
                {
                    convertedValue = (float)propertyValue;
                    HandlePropertyChange(propertyName, convertedValue);
                }
                catch (System.Exception)
                {
                    continue;
                }

                Debug.Log($"Property Name: {propertyName}, Value: {propertyValue}");
            }
        }

        private void SetBlendShapeWeight(string blendShape, float currentValue)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape), currentValue);
        }

        #endregion Private Methods

        #region Interface Methods

        public void HandlePropertyChange(string propertyName, float newValue)
        {
            foreach (var skin in SkinnedBindings)
            {
                if (propertyName == skin.CharacterProperty.ToString())
                {
                    float val;
                    // If it's a two-way slider, make sure we feed the absolute value for sliders meant to be inverted.
                    if (skin.IsInvertedValue)
                        val = (newValue < 0f) ? Mathf.Abs(newValue) : 0;
                    else
                        val = newValue;

                    // Update the current value.
                    skin.CurrentValue = val;

                    // Set the blend shape weight.
                    SetBlendShapeWeight(skin.BlendShape, skin.CurrentValue);
                }
            }
        }

        #endregion Interface Methods
    }
}