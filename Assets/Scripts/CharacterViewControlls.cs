using System.Collections;
using UnityEngine;

// CharacterViewControlls contains logic to support the rotation and zoom buttons,
// allowing a user to rotate the character model and zoom in to a pre-defined position
// to closer inspect the model's face.
public class CharacterViewControlls : MonoBehaviour
{
    [SerializeField] private Transform characterTransfom;
    [SerializeField] private Transform cameraTransfom;
    [SerializeField] private Transform cameraZoomInPosition;
    [SerializeField] private Transform cameraZoomOutPosition;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float zoomDurationSeconds;

    private Vector3 lerpTarget;
    private IEnumerator lerpCoroutine;

    public void RotateRight()
    {
        characterTransfom.Rotate(0, -rotateSpeed, 0);
    }

    public void RotateLeft()
    {
        characterTransfom.Rotate(0, rotateSpeed, 0);
    }

    public void ToggleZoom()
    {
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }

        lerpTarget = lerpTarget.Equals(cameraZoomInPosition.position) ? cameraZoomOutPosition.position : cameraZoomInPosition.position;
        float lerpDuration = Vector3.Distance(cameraTransfom.position, lerpTarget) / Vector3.Distance(cameraZoomInPosition.position, cameraZoomOutPosition.position) * zoomDurationSeconds;
        
        lerpCoroutine = LerpPosition(cameraTransfom, lerpTarget, lerpDuration);
        StartCoroutine(lerpCoroutine);
    }

    IEnumerator LerpPosition(Transform movingTransform, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = movingTransform.position;
        while (time < duration)
        {
            movingTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        movingTransform.position = targetPosition;
        lerpCoroutine = null;
    }
}
