using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum Species
{
    Human,
    Elf,
    Orc
}

// CustomizationPanelSpecies is an extension of the CustomizationPanel partial class.
// It controls logic to support the Species customization tab, which controls
// the character models' body type and species attributes.
public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] Slider bodyTypeSlider;
    [SerializeField] private SpeciesToggleSlider humanToggleSlider;
    [SerializeField] private SpeciesToggleSlider elfToggleSlider;
    [SerializeField] private SpeciesToggleSlider orcToggleSlider;

    private List<SpeciesToggleSlider> speciesToggleSliders = new();

    private const float ATTRIBUTE_SLIDER_ADJUSTMENT = 100;
    private const string ATTRIBUTE_ELF_FEM = "species_elf_fem";
    private const string ATTRIBUTE_ELF_MASC = "species_elf_masc";
    private const string ATTRIBUTE_ORC_FEM = "species_orc_fem";
    private const string ATTRIBUTE_ORC_MASC = "species_orc_masc";
    private const string ATTRIBUTE_ORC_CANINE = "species_orc_canine";

    private void SetupSpeciesPanel()
    {
        speciesToggleSliders.Add(humanToggleSlider);
        speciesToggleSliders.Add(elfToggleSlider);
        speciesToggleSliders.Add(orcToggleSlider);
    }

    public void SpeciesToggledOn(SpeciesToggleSlider toggledSlider)
    {
        int activeSpeciesCount = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn == true).Count();

        // If two toggles are on, the newly turned on toggle slider is set to 0 and the other is set to 1
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
    }

    public void SpeciesToggledOff(SpeciesToggleSlider toggledSlider)
    {
        int activeSpeciesCount = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn == true).Count();

        // Ensure one toggle is always on
        if (activeSpeciesCount == 0)
        {
            // If attempting to turn off the only slider that's currently on, force the slider to stay on
            switch (toggledSlider.species)
            {
                case Species.Human:
                    if (!elfToggleSlider.toggle.isOn && !orcToggleSlider.toggle.isOn)
                        toggledSlider.SetToggleValueSilent(true);
                    break;
                case Species.Elf:
                    if (!humanToggleSlider.toggle.isOn && !orcToggleSlider.toggle.isOn)
                        toggledSlider.SetToggleValueSilent(true);
                    break;
                case Species.Orc:
                    if (!humanToggleSlider.toggle.isOn && !elfToggleSlider.toggle.isOn)
                        toggledSlider.SetToggleValueSilent(true);
                    break;
            }
        }
        // Turn off all sliders if only one species is active
        else if (activeSpeciesCount == 1)
        {
            foreach (SpeciesToggleSlider toggleSlider in speciesToggleSliders)
            {
                toggleSlider.TurnOffSlider();

                // Set the one active slider's value to 1
                if (toggleSlider.toggle.isOn)
                {
                    toggleSlider.SetSliderValueSilent(1);
                }
            }

            UpdateSpeciesAttributes();
        }
        // Adjsut values for remaining sliders if two species are still active
        else
        {
            // The value by which the remaining sliders will increase
            float otherSliderIncreaseValue = toggledSlider.slider.value / 2;

            foreach (SpeciesToggleSlider toggleSlider in speciesToggleSliders)
            {
                if (toggleSlider.toggle.isOn)
                {
                    toggleSlider.SetSliderValueSilent(toggleSlider.slider.value + otherSliderIncreaseValue);
                }
                else
                {
                    toggleSlider.TurnOffSlider();
                }
            }

            UpdateSpeciesAttributes();
        }
    }

    public void SpeciesSliderValueChanged(float changedSliderValue, Species changedSliderSpecies)
    {
        // The value that will be proportionally split between the other active sliders
        float remainingSlidersValue = 1 - changedSliderValue;

        // The set of sliders that are active, but not currently changing
        List<SpeciesToggleSlider> otherSliders = speciesToggleSliders.Where(toggleSlider => toggleSlider.toggle.isOn &&
            toggleSlider.species != changedSliderSpecies).ToList();


        // The sum total of the otherSliders slider values
        float otherSlidersCombinedValue = 0;
        otherSliders.ForEach(toggleSlider => otherSlidersCombinedValue += toggleSlider.slider.value);


        // Set the values of the otherSliders to be their porportaional share of remainingSlidersValue,
        // where the porportion is determined by the sliders' relative values
        foreach (SpeciesToggleSlider toggleSlider in otherSliders)
        {
            float sliderPortion = otherSlidersCombinedValue == 0 ? 1 : toggleSlider.slider.value / otherSlidersCombinedValue;
            toggleSlider.SetSliderValueSilent(remainingSlidersValue * sliderPortion);
        }

        UpdateSpeciesAttributes();
    }

    private void UpdateSpeciesAttributes()
    {
        // Set each of the species attributes according to the current values of the body type and species sliders
        attributeInventory.SetAttribute(ATTRIBUTE_ELF_FEM, elfToggleSlider.slider.value * bodyTypeSlider.value * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(ATTRIBUTE_ELF_MASC, elfToggleSlider.slider.value * (1 - bodyTypeSlider.value) * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(ATTRIBUTE_ORC_FEM, orcToggleSlider.slider.value * bodyTypeSlider.value * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(ATTRIBUTE_ORC_MASC, orcToggleSlider.slider.value * (1 - bodyTypeSlider.value) * ATTRIBUTE_SLIDER_ADJUSTMENT);
        attributeInventory.SetAttribute(ATTRIBUTE_ORC_CANINE, orcToggleSlider.slider.value * ATTRIBUTE_SLIDER_ADJUSTMENT);
    }
}

