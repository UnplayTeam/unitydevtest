using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSlider : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _nameTextBox;
    public TMPro.TextMeshProUGUI _leftTextBox;
    public TMPro.TextMeshProUGUI _rightTextBox;
    public Slider _slider;

    public float Value
	{
		get
		{
            return _slider.value;
		}
        set
		{
            _slider.value = value;
		}
	}

    public void AddListener(UnityAction<float> call)
	{
		_slider.onValueChanged.AddListener(call);
	}
	
	public void RemoveListener(UnityAction<float> call)
	{
		_slider.onValueChanged.RemoveListener(call);
	}
}
