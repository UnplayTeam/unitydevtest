using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace JoshBowersDEV.Characters
{
    [Serializable]
    public class SkinnedBindingListWrapper
    {
        public List<SkinnedBinding> Bindings;
    }

    [CustomEditor(typeof(CharacterSkinnedMeshBinding))]
    public class CharacterSkinnedMeshBindingEditor : Editor
    {
        private bool _showBindings = true; // Whether to show the bindings list.

        public override void OnInspectorGUI()
        {
            CharacterSkinnedMeshBinding binding = (CharacterSkinnedMeshBinding)target;

            binding.SkinnedMeshRenderer = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Skinned Mesh Renderer", binding.SkinnedMeshRenderer, typeof(SkinnedMeshRenderer), true);
            if (binding.CharacterMeshData == null)
            {
                AsyncOperationHandle<CharacterMeshData> reference = Addressables.LoadAssetAsync<CharacterMeshData>("Assets/unitydevtest/ScriptableObjects/Data/DefaultMale.asset");
                binding.CharacterMeshData = reference.Result;
            }

            EditorGUILayout.LabelField("Bindings", EditorStyles.boldLabel);

            _showBindings = EditorGUILayout.Foldout(_showBindings, "Skinned Bindings");

            Rect labelRect = GUILayoutUtility.GetLastRect();

            // Check for a right-click context menu on the "Skinned Bindings" label.
            if (Event.current.type == EventType.ContextClick && labelRect.Contains(Event.current.mousePosition))
            {
                SkinnedBindingsContextMenu(binding);
                Event.current.Use();
            }

            if (_showBindings)
            {
                if (binding.SkinnedBindings == null)
                    binding.SkinnedBindings = new List<SkinnedBinding>();

                if (binding.SkinnedBindings.Count == 0)
                {
                    binding.SkinnedBindings.Add(new SkinnedBinding());
                }

                // Display a list of SkinnedBinding elements.
                for (int i = 0; i < binding.SkinnedBindings.Count; i++)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField("Binding " + (i + 1), EditorStyles.boldLabel);

                    // Display the BlendShape enum popup based on available blend shapes.
                    string[] blendShapeNames = GetBlendShapeNames(binding.SkinnedMeshRenderer);
                    int selectedBlendShapeIndex = -1; // Default value, indicating no selection

                    if (binding.SkinnedBindings[i] != null)
                    {
                        selectedBlendShapeIndex = ArrayUtility.IndexOf(blendShapeNames, binding.SkinnedBindings[i].BlendShape);

                        // Check if the selectedBlendShapeIndex is out of bounds
                        if (selectedBlendShapeIndex < 0 || selectedBlendShapeIndex >= blendShapeNames.Length)
                        {
                            // Handle the out-of-bounds case, for example, by resetting to a default value.
                            selectedBlendShapeIndex = 0; // Set to the first element or another valid default.
                        }
                    }

                    selectedBlendShapeIndex = EditorGUILayout.Popup("BlendShape", selectedBlendShapeIndex, blendShapeNames);
                    binding.SkinnedBindings[i].BlendShape = blendShapeNames[selectedBlendShapeIndex];

                    // Display the CharacterProperty enum popup based on available float properties in CharacterMeshData.
                    string[] propertyNames = GetCharacterPropertyNames(binding.CharacterMeshData);
                    if (propertyNames.Length < 1)
                        Debug.Log("No property names found.");

                    int selectedPropertyIndex = -1; // Default value, indicating no selection

                    if (binding.SkinnedBindings[i] != null)
                    {
                        selectedPropertyIndex = ArrayUtility.IndexOf(propertyNames, binding.SkinnedBindings[i].CharacterProperty);

                        // Check if the selectedPropertyIndex is out of bounds
                        if (selectedPropertyIndex < 0 || selectedPropertyIndex >= propertyNames.Length)
                        {
                            // Handle the out-of-bounds case, for example, by resetting to a default value.
                            selectedPropertyIndex = 0; // Set to the first element or another valid default.
                        }
                    }

                    selectedPropertyIndex = EditorGUILayout.Popup("CharacterProperty", selectedPropertyIndex, propertyNames);

                    binding.SkinnedBindings[i].CharacterProperty = propertyNames[selectedPropertyIndex];

                    // Display the Weight field.
                    binding.SkinnedBindings[i].Weight = EditorGUILayout.FloatField("Weight", binding.SkinnedBindings[i].Weight);

                    // Add a button to remove this binding.
                    if (GUILayout.Button("Remove Binding"))
                    {
                        binding.SkinnedBindings.RemoveAt(i);
                        i--; // Adjust the index as the list size decreased.
                    }

                    EditorGUILayout.EndVertical();
                }

                // Add a button to add a new binding.
                if (GUILayout.Button("Add Binding"))
                {
                    binding.SkinnedBindings.Add(new SkinnedBinding());
                }
            }

            EditorUtility.SetDirty(binding);

            // Apply modifications to the serialized object.
            serializedObject.ApplyModifiedProperties();
        }

        #region Property Name Functionality

        // Helper method to get the names of all blend shapes in the SkinnedMeshRenderer.
        private string[] GetBlendShapeNames(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            string[] blendShapeNames = new string[skinnedMeshRenderer.sharedMesh.blendShapeCount];
            for (int i = 0; i < blendShapeNames.Length; i++)
            {
                blendShapeNames[i] = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            }
            return blendShapeNames;
        }

        // Helper method to get the names of all public float properties in CharacterMeshData.
        private string[] GetCharacterPropertyNames(CharacterMeshData characterMeshData)
        {
            List<string> propertyNames = new List<string>();

            if (characterMeshData != null)
            {
                System.Reflection.PropertyInfo[] properties = characterMeshData.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(float))
                    {
                        propertyNames.Add(property.Name);
                    }
                }
            }
            else
            {
                Debug.LogWarning("CharacterMeshData is null.");
            }

            if (propertyNames.Count == 0)
            {
                Debug.LogWarning("No public float properties found in CharacterMeshData.");
            }

            return propertyNames.ToArray();
        }

        #endregion Property Name Functionality

        #region Context Menu Functionality

        private void SkinnedBindingsContextMenu(CharacterSkinnedMeshBinding binding)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Copy Skinned Bindings"), false, () => CopySkinnedBindings(binding));
            menu.AddItem(new GUIContent("Paste Skinned Bindings"), false, () => PasteSkinnedBindings(binding));
            menu.AddItem(new GUIContent("Reset Skinned Bindings"), false, () => ResetSkinnedBindings(binding));
            menu.ShowAsContext();
        }

        private void CopySkinnedBindings(CharacterSkinnedMeshBinding binding)
        {
            // Ensure you have a reference to your list.
            List<SkinnedBinding> skinnedBindings = binding.SkinnedBindings;

            // Serialize the list to a JSON string.
            string json = JsonUtility.ToJson(new SkinnedBindingListWrapper { Bindings = skinnedBindings });

            // Set the JSON string to the system clipboard.
            EditorGUIUtility.systemCopyBuffer = json;

            // Optionally, display a message in the console.
            Debug.Log("Skinned Bindings copied to clipboard: \n" + json);
        }

        private void PasteSkinnedBindings(CharacterSkinnedMeshBinding binding)
        {
            string clipboardData = EditorGUIUtility.systemCopyBuffer;

            if (!string.IsNullOrEmpty(clipboardData))
            {
                try
                {
                    // Deserialize and overwrite the existing list.
                    var deserializedBindings = JsonUtility.FromJson<SkinnedBindingListWrapper>(clipboardData);
                    if (deserializedBindings != null)
                    {
                        binding.SkinnedBindings = deserializedBindings.Bindings;
                        EditorGUIUtility.systemCopyBuffer = null;
                    }
                    else
                    {
                        Debug.LogError("Error pasting Skinned Bindings: Invalid clipboard data format.");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Error pasting Skinned Bindings: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Clipboard is empty or does not contain valid data for Skinned Bindings.");
            }
        }

        private void ResetSkinnedBindings(CharacterSkinnedMeshBinding binding)
        {
            binding.SkinnedBindings = new List<SkinnedBinding>(1);
        }

        #endregion Context Menu Functionality
    }
}