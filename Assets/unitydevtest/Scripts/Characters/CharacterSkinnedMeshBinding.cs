using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    /// <summary>
    /// Simple class for containing data for a specific BlendShape target.
    /// </summary>
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
    /// Used to bind specific <see cref="SkinnedMeshRenderer"/> BlendShape values to <see cref="CharacterMeshData"/> values.
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
                    Init();
                }
                catch (System.Exception e)
                {
                    // Do nothing
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

        /// <summary>
        /// Allows editor access for automatically filling out the <see cref="SkinnedBindings"/> list with the current BlendShapes.
        /// </summary>
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

        private void SetBlendShapeWeight(string blendShape, float currentValue)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape), currentValue);
        }

        #endregion Private Methods

        #region Interface Methods

        /// <summary>
        /// Async void that allows <see cref="InitializeDataValues"/> to be used within properties.
        /// </summary>
        public async void Init()
        {
            await InitializeDataValues();
        }

        /// <summary>
        /// Initialize the current data values by using reflection, which doesn't require us to hardcode ourselves to specific Character Data property names just in case properties are added/removed/changed
        /// </summary>
        /// <returns></returns>
        [ExecuteInEditMode]
        public async Task InitializeDataValues()
        {
            PropertyInfo[] properties = CharacterMeshData.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            List<Tuple<string, float>> keyValues = new List<Tuple<string, float>>();

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
                    keyValues.Add(new Tuple<string, float>(propertyName, convertedValue));
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
            int index = keyValues.Count;
            await new WaitForUpdate(); // Back to main thread for Unity behaviours.

            for (int i = 0; i < index; i++)
            {
                HandlePropertyChange(keyValues[i].Item1, keyValues[i].Item2);
            }
        }

        /// <summary>
        /// Check's each <see cref="SkinnedBinding"/> and searches for a propertyName match, then updates the corresponding Blend Shape with the new value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        [ExecuteInEditMode]
        public async void HandlePropertyChange(string propertyName, float newValue)
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
                        val = (newValue > 0f) ? newValue : 0;

                    // Todo: Finish two-way communication between other objects and the current value, as of now the weighted blendshape updates that are supposed to update based on other features being changed, does not update the other sliders and assets that have direct control of that blendshape/property.
                    // Updates the current value, multiplie times weight in case minor adjustments need to be made.
                    //skin.CurrentValue = val * skin.Weight;

                    skin.CurrentValue = val;

                    // Set the blend shape weight.
                    SetBlendShapeWeight(skin.BlendShape, skin.CurrentValue);
                    await Task.Delay(1);
                }
            }
        }

        #endregion Interface Methods
    }
}