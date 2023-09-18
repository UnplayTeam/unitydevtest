using System;
using RPG.Character.Avatar;
using RPG.UI.Controls;
using UnityEngine;

namespace RPG.UI {
  public class MenuPanelCustomization : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _CharacterPawnAvatar;
    [SerializeField] private UIBlendGroupControl[] _UIBlendGroupControls = Array.Empty<UIBlendGroupControl> ();
    
    public CharacterPawnAvatar CharacterPawnAvatar => _CharacterPawnAvatar;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blendShapeGroupName"></param>
    /// <param name="value"></param>
    public void SetPawnBlendGroupValue (string blendShapeGroupName, float value) {
      _CharacterPawnAvatar.SetBlendShapeValue (blendShapeGroupName, value);
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
