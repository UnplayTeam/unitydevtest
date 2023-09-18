using System.Collections;
using System.Collections.Generic;
using RPG.UI;
using RPG.UI.Controls;
using UnityEngine;
using UnityEngine.Events;

namespace RPG {
  public class UIBarycentricBlendGroupSlider : MonoBehaviour {
    [SerializeField] private MenuPanelCustomization _MenuPanelCustomization;
    [SerializeField] private string _BlendGroupNameKeyX;
    [SerializeField] private string _BlendGroupNameKeyY;
    [SerializeField] private string _BlendGroupNameKeyZ;
    [SerializeField] private UIBarycentricSlider _Slider;

    private bool _SkipInvokeChanged = false;

    public UnityEvent<string, float> OnValueXChanged = new ();
    public UnityEvent<string, float> OnValueYChanged = new ();
    public UnityEvent<string, float> OnValueZChanged = new ();
    
    public void SetValue (Vector3 value) {
      _Slider.SetValue (value);
    }
    
    // Unity
    private void Awake () {
      Vector3 startingValue = new (
        string.IsNullOrEmpty (_BlendGroupNameKeyX) ? 1f : _MenuPanelCustomization.CharacterPawnAvatar.GetBlendShapeValue (_BlendGroupNameKeyX),
        string.IsNullOrEmpty (_BlendGroupNameKeyY) ? 1f : _MenuPanelCustomization.CharacterPawnAvatar.GetBlendShapeValue (_BlendGroupNameKeyY), 
        string.IsNullOrEmpty (_BlendGroupNameKeyZ) ? 1f : _MenuPanelCustomization.CharacterPawnAvatar.GetBlendShapeValue (_BlendGroupNameKeyZ)
      );
      SetValue (startingValue);
      _Slider.OnValueChanged.AddListener (OnSliderValueChanged);
    }

    private void OnSliderValueChanged (Vector3 barycentricValue) {
      InvokeValueChanged (_BlendGroupNameKeyX, barycentricValue.x);
      InvokeValueChanged (_BlendGroupNameKeyY, barycentricValue.y);
      InvokeValueChanged (_BlendGroupNameKeyZ, barycentricValue.z);
    }

    private void OnDestroy () {
      _Slider.OnValueChanged.RemoveListener (OnSliderValueChanged);
    }

    // Internal
    private void InvokeValueChanged (string blendGroupName, float value) {
      float interpolatedValue = Mathf.Lerp (MeshUtils.BlendShapeWeightMin, MeshUtils.BlendShapeWeightMax, value);
      _MenuPanelCustomization.CharacterPawnAvatar.SetBlendShapeValue (blendGroupName, interpolatedValue);
      OnValueXChanged.Invoke (blendGroupName, interpolatedValue);
    }
  }
}