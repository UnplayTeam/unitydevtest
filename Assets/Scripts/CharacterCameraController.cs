using UnityEngine;

[System.Serializable]
public struct CameraTarget
{
    public Transform targetTransform;
    public float angle;
    public float zoom;
}

public class CharacterCameraController : MonoBehaviour
{
    public static CharacterCameraController Singleton = null;

    public Transform _characterTransform; // Reference to the character's transform.
    public Transform _cameraTransform;

    public Menu _menu;

    public float _moveSpeed = 5.0f; // Adjust the move speed as needed.
    public float _rotationSpeed = 5.0f;
    public float _minY = 0, _maxY = 0;
    public float _minZ = 0, _maxZ = 0;

    public float _lerpTime = 1;

    private Vector3 m_lastMousePosition;

    private float m_lerpStartTime = -1;
    private float m_startY = 0;
    private float m_targetY = 0;
    private float m_startZ = 0;
    private float m_targetZ = 0;
    private float m_startRot = 0;
    private float m_targetRot = 0;

	private void Start()
	{
        if (Singleton != null)
            Destroy(Singleton);

        Singleton = this;
	}

	void Update()
    {
        if(m_lerpStartTime != -1)
		{
            float lerp = (Time.time - m_lerpStartTime)/_lerpTime;

            float y = Mathf.Lerp(0, m_targetY, lerp)+m_startY;
            float z = Mathf.Lerp(m_startZ, m_targetZ, lerp);

            float angle = Mathf.LerpAngle(m_startRot, m_targetRot, lerp);

            _characterTransform.position = new Vector3(0, y, z);
            _characterTransform.eulerAngles = new Vector3(0, angle, 0);

            if (lerp > 1)
			{
                _characterTransform.position = new Vector3(0, m_targetY + m_startY, m_targetZ);
                _characterTransform.eulerAngles = new Vector3(0, m_targetRot, 0);
                m_lerpStartTime = -1;
			}
		}
        else if (!_menu._leftPanel.MouseHovering) {
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
        Vector3 currentMousePosition = Input.mousePosition;

        Vector3 mouseDelta = currentMousePosition - m_lastMousePosition;

        float rotationX = mouseDelta.x * _rotationSpeed * Time.deltaTime;

        _characterTransform.Rotate(Vector3.up, -rotationX);
    }

    public void SetCameraTarget(CameraTarget target)
	{
        m_lerpStartTime = Time.time;

        m_startRot = _characterTransform.rotation.eulerAngles.y;
        m_targetRot = target.angle;
        m_startY = _characterTransform.position.y;
        m_targetY = _cameraTransform.position.y - target.targetTransform.position.y;
        m_startZ = _characterTransform.position.z;
        m_targetZ = (target.zoom * (_maxZ - _minZ)) + _minZ;
    }
}