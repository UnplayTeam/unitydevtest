using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlendshapeSystem
{

    /// <summary>
    /// A Monobehavior which listens for blendshape updates, and automatically updates the model it's attached to with
    /// the relevant blendshape values if applicable.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class BlendshapeListener : MonoBehaviour
    {
        #region Member Data
        // The root of the blendshape key, omitted in the enum of all blendshape keys for readability
        private string _gameObjectBSIdentifier => "bs_" + gameObject.name + "." + gameObject.name + "_";

        // The skinned mesh renderer for which this script modifies blendshape values (_skinnedMeshRenderer auto-populates and cannot be null)
        private SkinnedMeshRenderer _skinnedMeshRendererBackingField = null;
        private SkinnedMeshRenderer _skinnedMeshRenderer => (_skinnedMeshRendererBackingField = _skinnedMeshRendererBackingField != null ? _skinnedMeshRendererBackingField : GetComponent<SkinnedMeshRenderer>());

        // The mesh for which this script modifies blendshape values
        private Mesh _mesh => _skinnedMeshRenderer.sharedMesh;

        // Maps between the blend keys used elsewhere in scripts and the actual integer indices associated with them for this object
        private Dictionary<BlendKey, int> _blendshapeIndices = new Dictionary<BlendKey, int>();

        #endregion

        #region Functions

        #region Monobehaviour methods

        /// <summary>
        /// OnEnable listens for blendshape updates
        /// </summary>
        private void OnEnable()
        {
            BlendshapeControls.onBlendshapeChanged += SetBlendshapeValue;
        }

        /// <summary>
        /// OnDisable stops listening
        /// </summary>
        private void OnDisable()
        {
            BlendshapeControls.onBlendshapeChanged -= SetBlendshapeValue;
        }

        /// <summary>
        /// Start ensures the script is set up to receive blendshape updates properly
        /// </summary>
        private void Start()
        {
            Initialize();
        }
        #endregion

        /// <summary>
        /// Initialize ensures that the values of the attached skinned mesh renderer are properly accessible and categorized
        /// </summary>
        private void Initialize()
        {
            // Clear the existing dictionaries, in case of re-initialization
            _blendshapeIndices.Clear();

            // Destroy this component if there are no blendshapes on the mesh to modify
            if (_mesh != null && _mesh.blendShapeCount == 0 )
            {
                Destroy(this);
                return;
            }

            // Get all of the named blend shapes on the mesh
            var names = GetAllBlendshapeNames() ?? new string[0];

            // Loop through each blendshape on the mesh and associate it's index with a key
            for (int i = 0; i < names.Length; i++)
            {
                // Remove the excess information from the blendshape name on the mesh
                var name = names[i];
                string shortened = name.Replace(_gameObjectBSIdentifier, "");

                // Try to parse the shortened form to an enum key
                BlendKey parsedKey;
                if (System.Enum.TryParse<BlendKey>(shortened, true, out parsedKey))
                {
                    // Add the key and index to the dictionary/cache
                    _blendshapeIndices.Add(parsedKey, i);

                    // Set the blendshape value if applicable
                    if (BlendshapeControls.instance != null)
                    {
                        SetBlendshapeValue(parsedKey, BlendshapeControls.instance[parsedKey]);
                    }
                }
                else
                {
                    // The name is a blendshape on the mesh that has not been implemented in this solution
                }
            }
        }

        /// <summary>
        /// Retrieves all of the names of blendshape indexes for the attached skinned mesh renderer
        /// </summary>
        /// <returns></returns>
        private string[] GetAllBlendshapeNames()
        {
            string[] results = null;

            // If the mesh is accessible and has blendshapes, iterate through them and find their names
            if (_mesh != null)
            {
                results = new string[_mesh.blendShapeCount];
                for (int i = 0; i < _mesh.blendShapeCount; i++)
                {
                    results[i] = _mesh.GetBlendShapeName(i);
                }
            }
            return results;
        }

        /// <summary>
        /// Sets the value of a particular blendshape index if present
        /// </summary>
        /// <param name="key"> The blendshape key to modify</param>
        /// <param name="value"> The value to set for that key</param>
        private void SetBlendshapeValue(BlendKey key, float value)
        {
            // Ensure the desired value is defined
            if (!float.IsFinite(value) || float.IsNaN(value))
            {
                value = 0;
            }

            // If the updated key exists on this mesh, we have to update the associated blendshape value
            if (_blendshapeIndices.ContainsKey(key))
            {
                // Ensure that there is no attempt to access a null mesh object
                if (_skinnedMeshRenderer == null) { Debug.LogError("No skinned mesh renderer on gameobject"); return; }

                // Find the stored index of the desired key
                int index = _blendshapeIndices[key];

                // If found and valid, set the associated blendshape weight
                if (_mesh != null && _mesh.blendShapeCount > index)
                {
                    _skinnedMeshRenderer.SetBlendShapeWeight(index, value * 100);
                }
            }
            else
            {
                // This mesh does not have that key initialized.
                // Either this object has not been initialized, or lacks the desired key
                return;
            }
        }

        #endregion
    }
}