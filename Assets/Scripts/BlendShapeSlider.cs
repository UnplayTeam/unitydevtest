using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The UI component that writes it's input to a given scriptable object
/// </summary>
public class BlendShapeSlider : MonoBehaviour
{
    [SerializeField]
    private Text sliderName;
    [SerializeField]
    private Text sliderLeftSpectrum;
    [SerializeField]
    private Text sliderRightSpectrum;
    private Slider slider;

    public BlendshapeData blendshapeData;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        //Setup UI
        if (blendshapeData != null)
        {
            sliderName.text = blendshapeData.sliderTitle;
            sliderLeftSpectrum.text = blendshapeData.sliderLeftSpectrumLabel;
            sliderRightSpectrum.text = blendshapeData.sliderRightSpectrumLabel;
            switch (blendshapeData.sliderType)
            {
                case SliderType.JUST_POSITIVE:
                    slider.minValue = 0f;
                    slider.maxValue = 100f;
                    break;

                case SliderType.POSITIVE_AND_NEGATIVE:
                    slider.minValue = -100f;
                    slider.maxValue = 100f;
                    break;
            }

            //Decoupling UI to have it not know about the overarching data
            slider.onValueChanged.AddListener(blendshapeData.SetValue);
        }
    }

}

