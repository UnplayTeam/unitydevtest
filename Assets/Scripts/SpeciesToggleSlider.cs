using UnityEngine;
using UnityEngine.UI;

public class SpeciesToggleSlider : MonoBehaviour
{
    public Species species;
    public Toggle toggle;
    public Slider slider;

    [SerializeField] CustomizationPanel customizationPanel;

    private bool ignoreSliderValueChange;

    public void Toggle(bool isOn)
    {
        if (isOn)
        {
            customizationPanel.SpeciesToggledOn(this);
        }
        else
        {
            customizationPanel.SpeciesToggledOff(this);
        }
    }

    public void TurnOffSlider()
    {
        SetSliderValueSilent(0);
        slider.gameObject.SetActive(false);
    }

    public void TurnOnSlider(float value)
    {
        SetSliderValueSilent(value);
        slider.gameObject.SetActive(true);
    }

    public void SetSliderValueSilent(float value)
    {
        ignoreSliderValueChange = true;
        slider.value = value;
        ignoreSliderValueChange = false;
    }

    public void SliderValueChanged(float value)
    {
        if (ignoreSliderValueChange) return;

        customizationPanel.SpeciesSliderValueChanged(slider.value, species);
    }
}
