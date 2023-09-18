using System;
using RPG.Character.Avatar;
using RPG.UI.Controls;
using UnityEngine;

namespace RPG.UI {
  public class MenuPanelCustomization : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _CharacterPawnAvatar;
    [SerializeField] private AvatarCustomizationCamera _Camera;
    [SerializeField] private Transform _CameraTarget;
    [SerializeField] private UIBlendGroupControl[] _UIBlendGroupControls = Array.Empty<UIBlendGroupControl> ();
    
    public void FocusOnCameraTarget () {
      _Camera.MoveCameraToPositionTarget(_CameraTarget);
    }
    
    // Unity
    private void Awake () {
      foreach (UIBlendGroupControl blendGroupControl in _UIBlendGroupControls) {
        float startingValue = _CharacterPawnAvatar.GetBlendShapeValue (blendGroupControl.BlendGroupNameKey);
        blendGroupControl.SetValue(startingValue);
        blendGroupControl.OnBlendGroupValueChanged.AddListener (OnBlendGroupValueChanged);
      }
    }

    // Internal
    private void OnBlendGroupValueChanged (string blendGroupName, float value) {
      _CharacterPawnAvatar.SetBlendShapeValue(blendGroupName, value);
    }
  }
}
