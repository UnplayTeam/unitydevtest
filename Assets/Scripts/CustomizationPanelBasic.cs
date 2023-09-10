using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Species
{
    Human,
    Orc,
    Elf
}

public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private SpeciesToggleSlider humanToggleSlider;
    [SerializeField] private SpeciesToggleSlider elfToggleSlider;
    [SerializeField] private SpeciesToggleSlider orcToggleSlider;

    private List<SpeciesToggleSlider> speciesToggleSliders = new();
    private float bodyTypeFemValue;

    private const float ATTRIBUTE_SLIDER_ADJUSTMENT = 100;

    private void SetupBasicPanel()
    {
        speciesToggleSliders.Add(humanToggleSlider);
        speciesToggleSliders.Add(elfToggleSlider);
        speciesToggleSliders.Add(orcToggleSlider);
    }

    public void BodyTypeUpdated(float value)
    {
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_GENDER_FEM, value * ATTRIBUTE_SLIDER_ADJUSTMENT);
        bodyTypeFemValue = value;
    }

    public void SpeciesToggledOn(SpeciesToggleSlider toggledSlider)
    {
        int activeSpeciesCount = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn == true).Count();

        // If two toggle are on, the newly turned on toggle slider is set to 0 and the other is set to 1
        if (activeSpeciesCount == 2)
        {
            foreach (SpeciesToggleSlider toggleSlider in speciesToggleSliders)
            {
                if (toggleSlider.toggle.isOn)
                {
                    toggleSlider.TurnOnSlider(toggleSlider == toggledSlider ? 0 : 1);
                }
            }
        }
        // If all toggles are on, the newly turned on toggle slider is set to 0 and the other two don't change
        else
        {
            toggledSlider.TurnOnSlider(0);
        }

        UpdateSpeciesAttributes();
    }

    public void SpeciesToggledOff(SpeciesToggleSlider toggledSlider)
    {
        int activeSpeciesCount = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn == true).Count();

        // Ensure one toggle is always on
        if (activeSpeciesCount == 0)
        {
            switch (toggledSlider.species)
            {
                case Species.Human:
                    if (!elfToggleSlider.toggle.isOn && !orcToggleSlider.toggle.isOn)
                        toggledSlider.toggle.isOn = true;
                    break;
                case Species.Elf:
                    if (!humanToggleSlider.toggle.isOn && !orcToggleSlider.toggle.isOn)
                        toggledSlider.toggle.isOn = true;
                    break;
                case Species.Orc:
                    if (!humanToggleSlider.toggle.isOn && !elfToggleSlider.toggle.isOn)
                        toggledSlider.toggle.isOn = true;
                    break;
            }
        }
        // Turn off all sliders if only one species is active
        else if (activeSpeciesCount == 1)
        {
            speciesToggleSliders.ForEach(toggleSlider => toggleSlider.TurnOffSlider());

            foreach (SpeciesToggleSlider toggleSlider in speciesToggleSliders)
            {
                toggleSlider.TurnOffSlider();
                if (toggleSlider.toggle.isOn)
                {
                    toggleSlider.ignoreSliderValueChange = true;
                    toggleSlider.slider.value = 1;
                    toggleSlider.ignoreSliderValueChange = false;
                }
            }
        }
        // Adjsut values for remaining sliders if two species are still active
        else
        {
            float otherSliderIncreaseValue = toggledSlider.slider.value / 2;

            foreach (SpeciesToggleSlider toggleSlider in speciesToggleSliders)
            {
                if (toggleSlider.toggle.isOn)
                {
                    toggleSlider.ignoreSliderValueChange = true;
                    toggleSlider.slider.value += otherSliderIncreaseValue;
                    toggleSlider.ignoreSliderValueChange = false;
                }
                else
                {
                    toggleSlider.TurnOffSlider();
                }
            }
        }

        UpdateSpeciesAttributes();
    }

    public void SpeciesSliderValueChanged(SpeciesToggleSlider changedToggleSlider)
    {
        float changedSliderValue = changedToggleSlider.slider.value;
        float remainingSlidersValue = 1 - changedSliderValue;

        List<SpeciesToggleSlider> otherSliders = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn &&
            toggleSlider != changedToggleSlider).ToList();


        float otherSlidersCombinedValue = 0;
        otherSliders.ForEach(toggleSlider => otherSlidersCombinedValue += toggleSlider.slider.value);



        foreach (SpeciesToggleSlider toggleSlider in otherSliders)
        {
            toggleSlider.ignoreSliderValueChange = true;

            float sliderPortion = otherSlidersCombinedValue == 0 ? 1 : toggleSlider.slider.value / otherSlidersCombinedValue;
            toggleSlider.slider.value = remainingSlidersValue * sliderPortion;

            toggleSlider.ignoreSliderValueChange = false;
        }

        UpdateSpeciesAttributes();
    }

    private void UpdateSpeciesAttributes()
    {
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_FEM, elfToggleSlider.slider.value * bodyTypeFemValue * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_MASC, elfToggleSlider.slider.value * (1 - bodyTypeFemValue) * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_FEM, orcToggleSlider.slider.value * bodyTypeFemValue * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_MASC, orcToggleSlider.slider.value * (1 - bodyTypeFemValue) * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_CANINE, orcToggleSlider.slider.value * ATTRIBUTE_SLIDER_ADJUSTMENT);
    }
}

