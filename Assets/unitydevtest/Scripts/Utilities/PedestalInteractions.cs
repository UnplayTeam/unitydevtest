using UnityEngine;

/// <summary>
/// A simple script that detects when a mouse click has been recieved on this object, and reacts accordingly.
/// </summary>
public class PedestalInteractions : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startMousePos;
    private float startRotationY;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>()) // Could be expanded on if needed
            {
                isDragging = true;
                startMousePos = Input.mousePosition;
                startRotationY = transform.eulerAngles.y;
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 deltaMousePos = Input.mousePosition - startMousePos;
            float rotationAmount = deltaMousePos.x * 0.5f; // Get the rotation amount from the last mouse position

            transform.rotation = Quaternion.Euler(0, startRotationY + rotationAmount, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}