using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User Input both read and captured to this scriptable obeject
/// </summary>

[CreateAssetMenu(fileName = "BlendshapeDataObj", menuName = "BlendshapeData")]
public class BlendshapeData : ScriptableObject
{
    [Header("UI Variables")]
    public string sliderTitle;
    public SliderType sliderType;
    public string sliderLeftSpectrumLabel;
    public string sliderRightSpectrumLabel;

    [Header("Blendshape Variables")]
    public string blendshapeKeyword;
    public string blendshapePositiveModifier;
    public string blendshapeNegativeModifier;
    public BlendshapeData blendshapeInfluence;

    public Action<BlendshapeData, BlendshapeData?> OnValueChanged;

    public float value { get; private set; }

    public void SetValue(float newValue)
    {
        value = newValue;
        //This is what actually causes the blend weight updates
        OnValueChanged?.Invoke(this, blendshapeInfluence);
    }
}

public enum SliderType
{
    JUST_POSITIVE,
    POSITIVE_AND_NEGATIVE
}
