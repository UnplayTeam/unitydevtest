using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
  public class AvatarCustomizationCamera : MonoBehaviour {
    [SerializeField] private Camera _Camera;
    [SerializeField] private Vector2 _TargetScreenPoint;
    [SerializeField] private float _PositionLerpSpeed = 1.0f;
    [SerializeField] private float _Duration = 1.0f;
    
    public void MoveCameraToPositionTarget (Transform target) {
      Vector2 screenTarget = new Vector2 ((Screen.width / 2) * _TargetScreenPoint.x,
        (Screen.height / 2) * _TargetScreenPoint.y);
      // Step 1: Find the world position corresponding to the center of the screen
      Vector3 screenCenter = new (screenTarget.x, screenTarget.y, _Camera.nearClipPlane);
      Vector3 worldPositionAtScreenCenter = _Camera.ScreenToWorldPoint (screenCenter);

      // Step 2: Find the offset between this world position and the target transform's position
      Vector3 offset = target.position - worldPositionAtScreenCenter;

      // Step 3: Move the camera by this offset
      _Camera.transform.position += offset;

      IEnumerator MoveToTargetCoroutine () {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float elapsed = 0f;

        while (elapsed < _Duration) {
          elapsed += Time.deltaTime;
          transform.position = Vector3.Lerp (startPosition, target.position, elapsed / _Duration);
          transform.rotation = Quaternion.Slerp (startRotation, target.rotation, elapsed / _Duration);
          yield return null;
        }

        // Set the final position and rotation to ensure it is exactly at the target
        _Camera.transform.position = target.position;
        _Camera.transform.rotation = target.rotation;
      }
    }
  }
}