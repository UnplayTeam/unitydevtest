using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
  public class AvatarCustomizationCamera : MonoBehaviour {
    [SerializeField] private Transform _FullBodyTarget;
    [SerializeField] private Transform _HeadTarget;
    
    private void Awake () {
      transform.AlignWith (_FullBodyTarget);
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor (typeof(AvatarCustomizationCamera))]
    public class AvatarCustomizationCameraEditor : UnityEditor.Editor {
      public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        if (!(target is AvatarCustomizationCamera customizationCamera)) {
          return;
        }
        using (new UnityEditor.EditorGUILayout.VerticalScope (GUI.skin.box)) {
          DrawAlignButtons (customizationCamera, customizationCamera._FullBodyTarget, "Full Body");
          DrawAlignButtons (customizationCamera, customizationCamera._HeadTarget, "Head");
        }
      }

      private void DrawAlignButtons (AvatarCustomizationCamera camera, Transform transform, string label) {
        using (new UnityEditor.EditorGUILayout.HorizontalScope ()) {
          float labelWidth = Mathf.Clamp (UnityEditor.EditorGUIUtility.currentViewWidth * 0.3f, 10f, 200f);
          GUILayout.Label (label, GUILayout.Width(labelWidth));
          if (GUILayout.Button ("Camera->Target")) {
            camera.transform.AlignWith (transform);
          }
          if (GUILayout.Button ("Target->Camera")) {
            transform.AlignWith (camera.transform);
          }
        }
      }
    }
#endif
  }
}
