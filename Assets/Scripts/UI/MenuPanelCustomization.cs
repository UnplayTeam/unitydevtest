using System;
using RPG.Character.Avatar;
using RPG.UI.Controls;
using UnityEngine;

namespace RPG.UI {
  public class MenuPanelCustomization : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _CharacterPawnAvatar;
    [SerializeField] private FocusCamera _Camera;
    [SerializeField] private FocusCameraTarget _CameraTarget;
    [SerializeField] private UIBlendGroupControl[] _UIBlendGroupControls = Array.Empty<UIBlendGroupControl> ();
    
    public void FocusOnCameraTarget () {
      _Camera.SetFocusCameraTarget(_CameraTarget);
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
