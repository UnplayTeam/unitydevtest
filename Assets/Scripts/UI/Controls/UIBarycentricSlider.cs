using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RPG.UI.Controls {
  // https://www.reddit.com/r/Unity3D/comments/b1qn5e/slider_for_three_values_d_code_included/
  // https://gist.github.com/Wokarol/fcb424148cb08c1dbbdf02d72a52a4dc

  /// <summary>
  /// UI Slider Control for a Barycentric Triangle shape, where each corner is a separate blendable value
  /// </summary>
  public class UIBarycentricSlider : MonoBehaviour, IPointerDownHandler, IDragHandler {
    public class OnBarycentricWeightsChanged : UnityEvent<Vector3> { }

    [SerializeField] private RectTransform _Handle = null;
    [SerializeField] private float _TriangleSize = 50f;
    
    private Vector3 _TriangleCornerA => new (0f, _TriangleSize, 0);
    private Vector3 _TriangleCornerAGlobal => transform.position + _TriangleCornerA;
    private Vector3 _TriangleCornerB => new (-_TriangleSize, -_TriangleSize, 0);
    private Vector3 _TriangleCornerBGlobal => transform.position + _TriangleCornerB;
    private Vector3 _TriangleCornerC => new (_TriangleSize, -_TriangleSize, 0);
    private Vector3 _TriangleCornerCGlobal => transform.position + _TriangleCornerC;
    
    /// <summary>
    /// Invoked when the current Value of the slider changes, either programmatically or by user interaction
    /// </summary>
    public OnBarycentricWeightsChanged OnValueChanged = new ();

    /// <summary>
    /// The current weights value of the slider
    /// </summary>
    public Vector3 Value => CalculateBarycentricCoordinates (_Handle.position);

    /// <summary>
    /// Set the value of the slider, where each axis of the Vector3 represents the weight of the corresponding corner
    /// </summary>
    /// <param name="value">The Vector3 weights to apply, will be normalized</param>
    public void SetValue (Vector3 value) {
      value.Normalize ();
      _Handle.position = CalculatePositionFromBarycentricCoordinates (value, _TriangleCornerAGlobal, _TriangleCornerBGlobal, _TriangleCornerCGlobal);
      InvokeValueChanged ();
    }
    
    // Internal
    private void InvokeValueChanged () {
      Vector3 result = CalculateBarycentricCoordinates (_Handle.position);
      OnValueChanged?.Invoke (result);
    }

    void IPointerDownHandler.OnPointerDown (PointerEventData eventData) {
      MoveHandleToPointer (eventData.position);
    }

    void IDragHandler.OnDrag (PointerEventData eventData) {
      MoveHandleToPointer (eventData.position);
    }

    private void MoveHandleToPointer (Vector2 pointerPosition) {
      _Handle.position = FindClosestPointInTriangle (pointerPosition, _TriangleCornerAGlobal, _TriangleCornerBGlobal, _TriangleCornerCGlobal);
      InvokeValueChanged ();
    }

    private Vector2 FindClosestPointInTriangle (Vector2 point, Vector3 a, Vector3 b, Vector3 c) {
      // Find the closest point in the triangle to the given point
      // This is a simplified solution and might not work correctly for all kinds of triangles
      Vector2 ab = b - a;
      Vector2 ac = c - a;
      Vector2 ap = point - (Vector2)a;

      float d1 = Vector2.Dot (ab, ap);
      float d2 = Vector2.Dot (ac, ap);

      if (d1 <= 0f && d2 <= 0f)
        return a;

      Vector2 bp = point - (Vector2)b;
      float d3 = Vector2.Dot (ab, bp);
      float d4 = Vector2.Dot (ac, bp);

      if (d3 >= 0f && d4 <= d3)
        return b;

      Vector2 cp = point - (Vector2)c;
      float d5 = Vector2.Dot (ab, cp);
      float d6 = Vector2.Dot (ac, cp);

      if (d6 >= 0f && d5 <= d6)
        return c;

      float vc = d1 * d4 - d3 * d2;

      if (vc <= 0f && d1 >= 0f && d3 <= 0f) {
        float v = d1 / (d1 - d3);
        return (Vector2)a + v * ab;
      }

      float vb = d5 * d2 - d1 * d6;

      if (vb <= 0f && d2 >= 0f && d6 <= 0f) {
        float w = d2 / (d2 - d6);
        return (Vector2)a + w * ac;
      }

      float va = d3 * d6 - d5 * d4;

      if (va <= 0f && (d4 - d3) >= 0f && (d5 - d6) >= 0f) {
        float w = (d4 - d3) / ((d4 - d3) + (d5 - d6));
        return (Vector2)b + w * ((Vector2)c - (Vector2)b);
      }

      float denom = 1f / (va + vb + vc);
      float v2 = vb * denom;
      float w2 = vc * denom;

      return (Vector2)a + ab * v2 + ac * w2;
    }
    
    private Vector3 CalculatePositionFromBarycentricCoordinates (Vector3 weights) => CalculateBarycentricCoordinates (weights,
      _TriangleCornerAGlobal, _TriangleCornerBGlobal, _TriangleCornerCGlobal);

    private Vector3 CalculatePositionFromBarycentricCoordinates (Vector3 weights, Vector3 a, Vector3 b, Vector3 c) {
      if (weights == Vector3.zero) {
        return (a + b + c) / 3;
      }
      weights /= weights.x + weights.y + weights.z;
      return weights.x * a + weights.y * b + weights.z * c;
    }

    private Vector3 CalculateBarycentricCoordinates (Vector3 p) =>
      CalculateBarycentricCoordinates (p, _TriangleCornerAGlobal, _TriangleCornerBGlobal, _TriangleCornerCGlobal);

    private Vector3 CalculateBarycentricCoordinates (Vector3 p, Vector3 a, Vector3 b, Vector3 c) {
      Vector3 v0 = b - a, v1 = c - a, v2 = p - a;
      float d00 = Vector3.Dot (v0, v0);
      float d01 = Vector3.Dot (v0, v1);
      float d11 = Vector3.Dot (v1, v1);
      float d20 = Vector3.Dot (v2, v0);
      float d21 = Vector3.Dot (v2, v1);
      float denom = d00 * d11 - d01 * d01;

      float v = (d11 * d20 - d01 * d21) / denom;
      float w = (d00 * d21 - d01 * d20) / denom;
      float u = 1.0f - v - w;

      // Create the derived Vector3 using normalized distances
      Vector3 derivedVector3 = new Vector3 (u, v, w);
      return derivedVector3;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos() {
      Vector3 position = transform.position;
      // Draw lines connecting the corners to form a triangle
      Gizmos.color = Color.green;
      Gizmos.DrawLine(position + _TriangleCornerA, position + _TriangleCornerB);
      Gizmos.DrawLine(position + _TriangleCornerB, position + _TriangleCornerC);
      Gizmos.DrawLine(position + _TriangleCornerC, position + _TriangleCornerA);
    }
    
    [UnityEditor.CustomEditor (typeof(UIBarycentricSlider))]
    public class UIBarycentricSliderEditor : UnityEditor.Editor {
      public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        if (!(target is UIBarycentricSlider barycentricSlider)) {
          return;
        }
        using (new UnityEditor.EditorGUILayout.VerticalScope (GUI.skin.box)) {
          if (GUILayout.Button ("Reset")) {
            barycentricSlider.SetValue (new Vector3 (0, 0, 0));
          }
          if (GUILayout.Button ("A")) {
            barycentricSlider.SetValue (new Vector3 (1, 0, 0));
          }
          if (GUILayout.Button ("B")) {
            barycentricSlider.SetValue (new Vector3 (0, 1, 0));
          }
          if (GUILayout.Button ("C")) {
            barycentricSlider.SetValue (new Vector3 (0, 0, 1));
          }
        }
      }
    }
#endif
  }
}