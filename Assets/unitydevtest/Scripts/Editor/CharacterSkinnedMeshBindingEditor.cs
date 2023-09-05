using UnityEditor;
using UnityEngine;

namespace JoshBowersDEV.Characters.Editor
{
    [CustomEditor(typeof(CharacterSkinnedMeshBinding))]
    public class CharacterSkinnedMeshBindingEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Target the script.
            CharacterSkinnedMeshBinding binding = (CharacterSkinnedMeshBinding)target;

            EditorGUILayout.LabelField("(This will clear existing bindings)");
            // Add a button above the SkinnedBindings property.
            if (GUILayout.Button("Fill Skinned Bindings"))
            {
                // Call the FillSkinnedBindings() function on the target script.
                binding.FillSkinnedBindings();
            }

            // Display the rest of the inspector properties.
            DrawDefaultInspector();
        }
    }
}