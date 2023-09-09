using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent onButtonHeld;

    private bool isPressed;

    private void Update()
    {
        if (!isPressed) return;

        onButtonHeld.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void SetButtonActive(bool isActive)
    {
        if (gameObject.activeInHierarchy == isActive)
        {
            return;
        }

        if (isActive == false)
        {
            isPressed = false;
        }

        gameObject.SetActive(isActive);
    }
}
