using UnityEngine;
using UnityEngine.UI;

public class SpeciesToggleSlider : MonoBehaviour
{
    [SerializeField] CustomizationPanel customizationPanel;
    public Species species;
    public Toggle toggle;
    public Slider slider;

    public bool ignoreSliderValueChange;

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
        ignoreSliderValueChange = true;
        slider.value = 0;
        slider.gameObject.SetActive(false);
        ignoreSliderValueChange = false;
    }

    public void TurnOnSlider(float value)
    {
        ignoreSliderValueChange = true;
        slider.value = value;
        slider.gameObject.SetActive(true);
        ignoreSliderValueChange = false;
    }

    public void SliderValueChanged(float value)
    {
        if (ignoreSliderValueChange) return;

        customizationPanel.SpeciesSliderValueChanged(this);
    }
}
