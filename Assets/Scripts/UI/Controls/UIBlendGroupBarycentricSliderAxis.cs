using UnityEngine;

namespace RPG.UI.Controls {
  /// <summary>
  /// A UI Control that manipulates a single BlendGroup value using 1 of 3 barycentric vector weights
  /// </summary>
  /// <remarks>Done this way to abstract the mutation of the vector value and allow a single base class for both slider types</remarks>
  public class UIBlendGroupBarycentricSliderAxis : UIBlendGroupControl {
    public enum Vector3Component {
      X, 
      Y, 
      Z
    }
    
    [SerializeField] private Vector3Component _BarycentricAxis;
    [SerializeField] private UIBarycentricSlider _Slider;


    public override float Value {
      get {
        switch (_BarycentricAxis) {
          case Vector3Component.X:
            return _Slider.Value.x;
          case Vector3Component.Y:
            return _Slider.Value.y;
          case Vector3Component.Z:
            return _Slider.Value.z;
          default:
            return 0f;
        }
      }
    }
    public override void SetValue (float value) {
      Vector3 currentValue = _Slider.Value;
      switch (_BarycentricAxis) {
        case Vector3Component.X:
          currentValue.x = string.IsNullOrEmpty (BlendGroupNameKey) ? 1f : value;
          break;
        case Vector3Component.Y:
          currentValue.y = string.IsNullOrEmpty (BlendGroupNameKey) ? 1f : value;
          break;
        case Vector3Component.Z:
          currentValue.z = string.IsNullOrEmpty (BlendGroupNameKey) ? 1f : value;
          break;
      }
      _Slider.SetValue (currentValue);
    }
    
    // Unity
    private void Awake () {
      _Slider.OnValueChanged.AddListener (OnSliderValueChanged);
    }
    
    private void OnSliderValueChanged (Vector3 barycentricValue) {
      switch (_BarycentricAxis) {
        case Vector3Component.X:
          InvokeValueChanged (barycentricValue.x);
          break;
        case Vector3Component.Y:
          InvokeValueChanged (barycentricValue.y);
          break;
        case Vector3Component.Z:
          InvokeValueChanged (barycentricValue.z);
          break;
      }
    }
    
    private void InvokeValueChanged (float value) {
      float interpolatedValue = Mathf.Lerp (MeshUtils.BlendShapeWeightMin, MeshUtils.BlendShapeWeightMax, value);
      OnBlendGroupValueChanged.Invoke (BlendGroupNameKey, interpolatedValue);
    }
  }
}
