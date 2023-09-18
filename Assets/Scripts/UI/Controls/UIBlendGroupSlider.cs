using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Controls {
  /// <summary>
  /// A UI control that manipulates a single BlendGroup value using a slider
  /// </summary>
  public class UIBlendGroupSlider : UIBlendGroupControl {
    [SerializeField] private Slider _Slider;

    private bool _SkipInvokeChanged;
    
    public override float Value => _Slider.value;
    
    public override void SetValue (float value) {
      _Slider.value = value;
    }
    
    // Unity
    private void Awake () {
      _Slider.minValue = MeshUtils.BlendShapeWeightMin;
      _Slider.maxValue = MeshUtils.BlendShapeWeightMax;
      _Slider.onValueChanged.AddListener (InvokeValueChanged);
    }
    
    private void OnDestroy () {
      _Slider.onValueChanged.RemoveListener (InvokeValueChanged);
    }

    // Internal
    private void InvokeValueChanged (float value) {
      OnBlendGroupValueChanged.Invoke (BlendGroupNameKey, value);
    }
  }
}
