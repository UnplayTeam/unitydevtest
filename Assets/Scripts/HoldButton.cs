using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// A HoldButton is a button that continuously fires its onButtonHeld event
// for as long as the button is held down.
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
}
