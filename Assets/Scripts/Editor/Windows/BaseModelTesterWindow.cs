using System;
using RPG.Character.Avatar;
using UnityEditor;
using UnityEngine;

namespace RPG.Editor {
  /// <summary>
  /// Allows developers to test and debug the base model blend shapes data in the editor
  /// </summary>
  public class AvatarBaseMeshDataTestingWindow : EditorWindow {
    
    [MenuItem ("RPG/AvatarBaseMeshData Tester")]
    public static void ShowWindow () {
      GetWindow<AvatarBaseMeshDataTestingWindow> ("AvatarBaseMeshData Tester");
    }
    
    private AvatarBaseMeshData _BaseMeshData;
    private CharacterPawnAvatar _CharacterPawnAvatar;

    private void OnGUI () {
      _BaseMeshData = (AvatarBaseMeshData) EditorGUILayout.ObjectField ("AvatarBaseMeshData", _BaseMeshData, typeof(AvatarBaseMeshData), false);
      _CharacterPawnAvatar = (CharacterPawnAvatar) EditorGUILayout.ObjectField ("CharacterPawnAvatar", _CharacterPawnAvatar, typeof(CharacterPawnAvatar), true);

      if (_BaseMeshData == null) {
        GUILayout.Label ("Set a base mesh!", EditorStyles.boldLabel);
        return;  
      } 
      
      bool hasCharacterPawnAvatar = _CharacterPawnAvatar != null;
      GUI.enabled = hasCharacterPawnAvatar;
      foreach (AvatarBaseMeshData.BlendShapeGroup blendShapeGroup in _BaseMeshData.BlendShapeGroups) {
        using (new EditorGUILayout.HorizontalScope (GUI.skin.box)) {
          float currentValue = hasCharacterPawnAvatar ? _CharacterPawnAvatar.GetBlendShapeValue (blendShapeGroup) : 0f;
          float sliderValue = EditorGUILayout.Slider (blendShapeGroup.BlendGroupName, currentValue, MeshUtils.BlendShapeWeightMin, MeshUtils.BlendShapeWeightMax);
          if (_CharacterPawnAvatar != null && Math.Abs (currentValue - sliderValue) > 0.01f) {
            _CharacterPawnAvatar.SetBlendShapeValue (blendShapeGroup, sliderValue);
          }

          if (GUILayout.Button ("Log Associated Meshes")) {
            foreach (AvatarBaseMeshData.BlendShapeData blendShape in blendShapeGroup.BlendShapes) {
              if (_CharacterPawnAvatar.TryGetCustomizableMesh (blendShape.MeshName, out SkinnedMeshRenderer customizableMesh)) {
                Debug.Log (customizableMesh.transform.name);
              }
            }
          }
        }
      }
      Repaint ();
    }
  }
}
