using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlendshapeSystem
{
    /// <summary>
    /// Enum to store the key values for all implemented blendshapes
    /// </summary>
    [System.Serializable]
    public enum BlendKey
    {
        // Sex and Race Keys
        species_orc_fem,
        species_orc_masc,
        species_elf_fem,
        species_elf_masc,
        gender_fem,

        // Body Weight
        body_weight_thin,
        body_weight_heavy,
        body_weight_thin_head,

        // Muscle
        body_muscular_heavy,
        body_muscular_mid,

        // Bust
        iso_bust_large,

        // Face
        facial_cheeks_gaunt
    }

    /// <summary>
    /// Monobehaviour which sends calls to change blendshape values on relevant meshes, wihtout needing references to them
    /// </summary>
    public class BlendshapeControls : MonoBehaviour
    {
        #region Member data
        // Static reference for ease of access
        public static BlendshapeControls instance;

        // A static delegate to be called whenever any blendshape key has a new value assigned to it
        public delegate void OnBlendshapeChanged(BlendKey key, float value);
        public static OnBlendshapeChanged onBlendshapeChanged = delegate { };

        // A dictionary that holds all the blendshape data in a more convenient format to access
        [SerializeField] private Dictionary<BlendKey, float> _blendshapes = new Dictionary<BlendKey, float>();

        // Accessor for blendshape values
        public float this[BlendKey key]
        {
            get // Gets the value stored in _blendshapes for the desired key, or adds 0 to the dictionary for that key
            {
                if (_blendshapes != null && _blendshapes.ContainsKey(key))
                {
                    if (_blendshapes.ContainsKey(key))
                    {
                        return _blendshapes[key];
                    }
                    else
                    {
                        _blendshapes.Add(key, 0);
                        return 0;
                    }
                }
                else
                {
                    return float.NaN;
                }
            }
            set // Sets the blendshape value by setting it in _blendshapes and calling the delegate to notify listeners of the change
            {
                if (_blendshapes != null)
                {
                    float original = this[key];
                    _blendshapes[key] = value;
                    if (original != value)
                    {
                        onBlendshapeChanged.Invoke(key, value);
                    }
                }
            }
        }

        #endregion

        #region Methods

        #region Monobehaviour Functions
        /// <summary>
        /// Ensure this is the only Blendshape controller in the scene - otherwise static references break
        /// Could be modified to support multiple models, but I don't see the point for this exercise
        /// </summary>
        private void Awake()
        {
            instance = instance == null ? this : instance;
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Initialize the controls on start
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Reset the blendshape data and update all the relevant meshes
        /// </summary>
        private void Initialize()
        {
            Reset();

            ForceUpdate();
        }

        /// <summary>
        /// Force all meshes and all blendshape keys to update to correct values
        /// </summary>
        public void ForceUpdate()
        {
            foreach (BlendKey key in System.Enum.GetValues(typeof(BlendKey)))
            {
                onBlendshapeChanged.Invoke(key, this[key]);
            }
        }

        /// <summary>
        /// Reset all blendshape weights to 0
        /// </summary>
        public void Reset()
        {
            foreach (BlendKey key in System.Enum.GetValues(typeof(BlendKey)))
            {
                this[key] = 0;
            }
        }

        #endregion

        #endregion
    }
}
