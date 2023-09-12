using System;
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

	public string _key = "";
	public bool _invertWeight = false;

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

	private void Start()
	{
		if (!string.IsNullOrEmpty(_key))
			AddListener(UpdateModel);
	}

	private void OnDestroy()
	{
		if (!string.IsNullOrEmpty(_key))
			RemoveListener(UpdateModel);
	}

	private void UpdateModel(float i)
	{
		if(_invertWeight)
			BlendShapeCollection.Singleton.SetWeight(_key, 1.0f - _slider.value);
		else
			BlendShapeCollection.Singleton.SetWeight(_key, _slider.value);
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
