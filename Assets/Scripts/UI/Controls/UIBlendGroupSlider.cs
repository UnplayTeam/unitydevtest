using System.Collections;
using System.Collections.Generic;
using RPG.Character.Avatar;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UI.Controls {
  public class UIBlendGroupSlider : MonoBehaviour {
    [SerializeField] private MenuPanelCustomization _MenuPanelCustomization;
    [SerializeField] private string _BlendGroupNameKey;
    [SerializeField] private Slider _Slider;

    private bool _SkipInvokeChanged = false;
    
    public string BlendGroupNameKey => _BlendGroupNameKey;
    public float Value => _Slider.value;
    
    public UnityEvent<string, float> OnValueChanged  = new ();

    public void SetValue (float value, bool invokeChanged = true) {
      _SkipInvokeChanged = !invokeChanged;
      _Slider.value = value;
      _SkipInvokeChanged = false;
    }
    
    // Unity
    private void Awake () {
      SetValue (_MenuPanelCustomization.CharacterPawnAvatar.GetBlendShapeValue (_BlendGroupNameKey), false);
      _Slider.minValue = MeshUtils.BlendShapeWeightMin;
      _Slider.maxValue = MeshUtils.BlendShapeWeightMax;
      _Slider.onValueChanged.AddListener (OnSliderValueChanged);
    }
    
    private void OnDestroy () {
      _Slider.onValueChanged.RemoveListener (OnSliderValueChanged);
    }

    // Internal
    private void OnSliderValueChanged (float value) {
      if (_SkipInvokeChanged) {
        return;
      }
      _MenuPanelCustomization.CharacterPawnAvatar.SetBlendShapeValue (_BlendGroupNameKey, value);
      OnValueChanged.Invoke (_BlendGroupNameKey, value);
    }
  }
}
