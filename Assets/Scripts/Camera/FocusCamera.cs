using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
  public class FocusCamera : MonoBehaviour {
    [SerializeField] private Camera _Camera;
    [SerializeField] private FocusCameraTarget _DefaultCameraTarget;
    [SerializeField] private Vector2 _TargetScreenPoint;

    private FocusCameraTarget _CurrentCameraTarget;
    public FocusCameraTarget CurrentCameraTarget => _CurrentCameraTarget != null ? _CurrentCameraTarget : _DefaultCameraTarget;
    
    // Unity
    private void LateUpdate () {
      if (CurrentCameraTarget == null) {
        return;
      }

      Vector3 targetPosition = CurrentCameraTarget.transform.position;
      // Adjust the camera's position to keep the target at the specified normalized position in the frame
      Vector3 targetViewportPosition = _Camera.WorldToViewportPoint(targetPosition);
      Vector3 targetWorldPosition = _Camera.ViewportToWorldPoint(new Vector3(_TargetScreenPoint.x, _TargetScreenPoint.y, targetViewportPosition.z));
      Vector3 offset = targetPosition - targetWorldPosition;

      _Camera.transform.position += offset;
      
      // Find the direction and the new position to maintain the specified distance from the target
      Vector3 directionFromTarget = _Camera.transform.position - targetPosition;
      _Camera.transform.position = targetPosition + directionFromTarget.normalized * CurrentCameraTarget.DistanceFromTarget;
    }


    public void SetFocusCameraTarget (FocusCameraTarget target) {
      _CurrentCameraTarget = target;
    }
  }
}