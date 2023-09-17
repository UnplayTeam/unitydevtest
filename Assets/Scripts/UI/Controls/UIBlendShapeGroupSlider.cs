using System.Collections;
using System.Collections.Generic;
using RPG.Character.Avatar;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UI.Controls {
  public class UIBlendShapeGroupSlider : MonoBehaviour {
    [SerializeField] private TMP_Text _GroupNameLabel;
    [SerializeField] private Slider _Slider;

    public AvatarBaseMeshData.BlendShapeGroup Group { get; private set; }
    
    public UnityEvent<UIBlendShapeGroupSlider, float> OnValueChanged  = new ();
    
    public void Initialize (AvatarBaseMeshData.BlendShapeGroup blendShapeGroup, float startingValue) {
      Group = blendShapeGroup;
      _GroupNameLabel.text = blendShapeGroup.GroupName;
      _Slider.minValue = MeshUtils.BlendShapeWeightMin;
      _Slider.maxValue = MeshUtils.BlendShapeWeightMax;
      _Slider.value = startingValue;
    }
    
    // Unity
    private void Awake () {
      _Slider.onValueChanged.AddListener (OnSliderValueChanged);
    }
    
    private void OnDestroy () {
      _Slider.onValueChanged.RemoveListener (OnSliderValueChanged);
    }

    // Internal
    private void OnSliderValueChanged (float value) {
      OnValueChanged.Invoke (this, value);
    }
  }
}
