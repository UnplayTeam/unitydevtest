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

	public bool _tripleBlend = false;
	public string _leftKey = "";
	public string _middleKey = "";
	public string _rightKey = "";

	private bool m_ignoreUpdate = false;

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
		if (!string.IsNullOrEmpty(_key) || _tripleBlend)
			AddListener(UpdateModel);
	}

	private void OnDestroy()
	{
		if (!string.IsNullOrEmpty(_key) || _tripleBlend)
			RemoveListener(UpdateModel);
	}

	private void OnEnable()
	{
		if (!string.IsNullOrEmpty(_key) && !_tripleBlend)
		{
			m_ignoreUpdate = true;

			float val = BlendShapeCollection.GetBSWeight(_key)/100.0f;

			if (_invertWeight)
				val = 1 - val;

			_slider.value = val;

			m_ignoreUpdate = false;
		}
		else if(_tripleBlend)
		{
			m_ignoreUpdate = true;

			float left = BlendShapeCollection.GetBSWeight(_leftKey) / 100.0f;
			float middle = BlendShapeCollection.GetBSWeight(_middleKey) / 100.0f;
			float right = BlendShapeCollection.GetBSWeight(_rightKey) / 100.0f;

			if (left > 0)
				_slider.value = (1 - left) / 2.0f;
			else if (right > 0)
				_slider.value = (right / 2.0f) + 0.5f;
			else if ((left == 0 && right == 0) || middle == 1)
				_slider.value = 0.5f;
			else if (left == 0)
				_slider.value = 1.0f - middle / 2.0f;
			else
				_slider.value = middle / 2.0f;

			m_ignoreUpdate = false;
		}
	}

	private void UpdateModel(float i)
	{
		if (m_ignoreUpdate)
			return;

		if (!_tripleBlend)
		{
			if (_invertWeight)
				BlendShapeCollection.SetWeight(_key, 1.0f - _slider.value);
			else
				BlendShapeCollection.SetWeight(_key, _slider.value);
		}
		else
		{
			BlendShapeCollection.SetMultiBlendWeights(_leftKey, _middleKey, _rightKey, _slider.value);
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
