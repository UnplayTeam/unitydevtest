using UnityEngine;

public class CharacterCameraController : MonoBehaviour
{
    public Transform _characterTransform; // Reference to the character's transform.

    public Menu _menu;

    public float _moveSpeed = 5.0f; // Adjust the move speed as needed.
    public float _rotationSpeed = 5.0f;
    public float _minY = 0, _maxY = 0;
    public float _minZ = 0, _maxZ = 0;

    private Vector3 m_lastMousePosition;

    void Update()
    {
        if (!_menu._leftPanel.MouseHovering) {
            if (Input.GetMouseButton(1))
            {
                MoveCharacterUpDown();
                RotateCharacter();
                _menu.gameObject.SetActive(false);
            }
            else
                _menu.gameObject.SetActive(true);

            if (Input.mouseScrollDelta != Vector2.zero)
                ScrollCharcterCloser();
        }

        m_lastMousePosition = Input.mousePosition;
    }

    private void MoveCharacterUpDown()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        Vector3 mouseDelta = currentMousePosition - m_lastMousePosition;

        float verticalMovement = -mouseDelta.y * _moveSpeed * Time.deltaTime;

        _characterTransform.Translate(Vector3.up * verticalMovement, Space.Self);
        _characterTransform.position = new Vector3(0,Mathf.Clamp(transform.position.y, _minY, _maxY), transform.position.z);
    }

    private void ScrollCharcterCloser()
	{
        _characterTransform.Translate(new Vector3(0,0,-Input.mouseScrollDelta.y),Camera.main.transform);
        _characterTransform.position = new Vector3(0, transform.position.y, Mathf.Clamp(transform.position.z, _minZ, _maxZ));
    }

    private void RotateCharacter()
    {
        // Get the current mouse position.
        Vector3 currentMousePosition = Input.mousePosition;

        // Calculate the mouse delta (change in position).
        Vector3 mouseDelta = currentMousePosition - m_lastMousePosition;

        // Rotate the character based on mouse movement.
        float rotationX = mouseDelta.x * _rotationSpeed * Time.deltaTime;

        // Apply the rotation to the character's transform.
        _characterTransform.Rotate(Vector3.up, rotationX);
    }
}