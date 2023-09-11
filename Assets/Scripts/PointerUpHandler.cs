using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// PointerUpHandler fires its onPointerUp event when the user releases the UI
// element. It is used for trigging sound effects when letting go of a slider.
public class PointerUpHandler : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private UnityEvent onPointerUp;

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke();
    }
}