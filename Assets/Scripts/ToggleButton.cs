using UnityEngine;

// ToggleButton is a button that has two distinct states, on and off.
// Each state includes a button component with individual onClick events.
// It is used for the music and sound effect toggles.
public class ToggleButton : MonoBehaviour
{
    [SerializeField] GameObject onState;
    [SerializeField] GameObject offState;

    public void ToggleOn()
    {
        onState.SetActive(true);
        offState.SetActive(false);
    }

    public void ToggleOff()
    {
        onState.SetActive(false);
        offState.SetActive(true);
    }
}
