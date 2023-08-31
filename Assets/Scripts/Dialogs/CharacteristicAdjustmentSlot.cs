using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicAdjustmentSlot : MonoBehaviour
{
    public Character Character;
    public CharacteristicType CharacteristicType;
    public TMPro.TextMeshProUGUI ButtonText;
    public Slider Slider;

    public void Init(Character character, CharacteristicType characteristicType, float weight = 0, bool hasNegativeValues = false)
    {
        Character = character;
        CharacteristicType = characteristicType;
        Slider.value = weight;
        if (hasNegativeValues)
        {
            Slider.maxValue = 200;
            Slider.value = 100;
        }
        ButtonText.text = $"Max out {characteristicType.ToString()}!";
    }

    public void OnValueChangedCallback(Slider slider)
    {
        Character.SetWeightForBlendShapesPerCharacteristic(CharacteristicType, slider.value);
    }

    public void OnMaxClickedCallback(Slider slider)
    {
        float max = slider.maxValue;
        Character.SetWeightForBlendShapesPerCharacteristic(CharacteristicType, max);
        slider.value = max;
    }
}
