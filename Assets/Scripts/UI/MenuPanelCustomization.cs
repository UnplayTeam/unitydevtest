using System.Collections;
using System.Collections.Generic;
using RPG.Character.Avatar;
using RPG.UI.Controls;
using UnityEngine;

namespace RPG.UI {
  public class MenuPanelCustomization : MonoBehaviour {
    [SerializeField] private CharacterPawnAvatar _CharacterPawnAvatar;
    [SerializeField] private Transform _ContentRoot;
    [SerializeField] private UIBlendShapeGroupSlider _BlendShapeGroupSliderPrefab;
    private void Awake () {
      foreach (AvatarBaseMeshData.BlendShapeGroup blendShapeGroup in _CharacterPawnAvatar.BaseMeshData.BlendShapeGroups) {
        UIBlendShapeGroupSlider slider = Instantiate (_BlendShapeGroupSliderPrefab, _ContentRoot);
        slider.Initialize (blendShapeGroup, _CharacterPawnAvatar.GetBlendShapeValue (blendShapeGroup));
        slider.OnValueChanged.AddListener (OnBlendShapeGroupSliderValueChanged);
      }
    }

    private void OnBlendShapeGroupSliderValueChanged (UIBlendShapeGroupSlider slider, float value) {
      _CharacterPawnAvatar.SetBlendShapeValue (slider.Group, value);
    }
  }
}
