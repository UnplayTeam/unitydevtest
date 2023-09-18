using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI.Controls {
  /// <summary>
  /// An abstract base class for a UI Control that manipulates a single BlendGroup value
  /// </summary>
  public abstract class UIBlendGroupControl : MonoBehaviour {
    public class BlendGroupValueChangedEvent : UnityEvent<string, float> { }
    
    [SerializeField] private string _BlendGroupNameKey;
    
    /// <summary>
    /// The name of the BlendGroup this control manipulates, can be empty depending on the UI control
    /// </summary>
    public string BlendGroupNameKey => _BlendGroupNameKey;
    
    /// <summary>
    /// Invoked when the underlying value of the control changes, either programmatically or by user interaction
    /// </summary>
    public BlendGroupValueChangedEvent OnBlendGroupValueChanged  = new ();
    
    /// <summary>
    /// The current float value of the control
    /// </summary>
    public abstract float Value { get; }

    /// <summary>
    /// Set the current float value of the control
    /// </summary>
    /// <param name="value">The value to set the control to</param>
    public abstract void SetValue (float value);
  }
}
