using UnityEngine;
using UnityEngine.UI;

// SpeciesToggleSlider is a combination UI element that can conditionally
// show/hide a slider as needed. When multiple species toggles are active,
// we show the sliders for the active species so that the user can modify
// the species values.
public class SpeciesToggleSlider : MonoBehaviour
{
    public Species species;
    public Toggle toggle;
    public Slider slider;

    [SerializeField] CustomizationPanel customizationPanel;

    private bool ignoreToggleValueChange;
    private bool ignoreSliderValueChange;

    // This method is triggered when the toggle is pressed.
    public void Toggle(bool isOn)
    {
        if (ignoreToggleValueChange) return;

        if (isOn)
        {
            customizationPanel.SpeciesToggledOn(this);
        }
        else
        {
            customizationPanel.SpeciesToggledOff(this);
        }
    }

    // This method is used when changing the toggle's value programatically,
    // as opposed to through user input. It allows us to set the toggle's value
    // in a script withouth also calling the body of Toggle.
    public void SetToggleValueSilent(bool value)
    {
        ignoreToggleValueChange = true;
        toggle.isOn = value;
        ignoreToggleValueChange = false;
    }

    public void TurnOffSlider()
    {
        // When a slider is turned off we always set its value to 0.
        SetSliderValueSilent(0);
        slider.gameObject.SetActive(false);
    }

    public void TurnOnSlider(float value)
    {
        // When a slider is turned on we can set its starting value.
        SetSliderValueSilent(value);
        slider.gameObject.SetActive(true);
    }

    // This method is trigger when the slider's value is changed.
    public void SliderValueChanged(float value)
    {
        if (ignoreSliderValueChange) return;

        customizationPanel.SpeciesSliderValueChanged(slider.value, species);
    }

    // This method is used when changing the slider's value programatically,
    // as opposed to through user input. It allows us to set the slider's value
    // in a script withouth also calling the body of SliderValueChanged.
    public void SetSliderValueSilent(float value)
    {
        ignoreSliderValueChange = true;
        slider.value = value;
        ignoreSliderValueChange = false;
    }
}
