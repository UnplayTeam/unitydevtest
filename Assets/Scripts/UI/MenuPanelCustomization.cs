using System;
using RPG.Character.Avatar;
using RPG.UI.Controls;
using UnityEngine;

namespace RPG.UI {
  public class MenuPanelCustomization : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _CharacterPawnAvatar;
    
    public CharacterPawnAvatar CharacterPawnAvatar => _CharacterPawnAvatar;
    
    public void SetBlendShapeValue (string blendShapeGroupName, float value) {
      _CharacterPawnAvatar.SetBlendShapeValue (blendShapeGroupName, value);
    }
    
    private void OnBlendShapeGroupSliderValueChanged (string slider, float value) {
      _CharacterPawnAvatar.SetBlendShapeValue (slider, value);
    }
  }
}
