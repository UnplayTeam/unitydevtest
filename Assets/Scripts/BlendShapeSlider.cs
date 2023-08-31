using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BlendShapeSlider : MonoBehaviour
{
	//~~~Definitions~~~
	private const float BLEND_MULTIPLIER = 100f;

	//~~~Variables~~~
	//Serialized
	[SerializeField]
	private TextMeshProUGUI m_label;
	[SerializeField]
	private Slider m_slider;

	//Non-Serialized
	private List<BlendShaper.BlendShapeData> m_blendShapeList;

	//~~~Accessors~~~

	//~~~Unity Functions~~~

	public void Intialize(string a_name, List<BlendShaper.BlendShapeData> a_blendShapeList)
	{
		if (a_blendShapeList == null)
		{
			Debug.LogError("Attempted to initialize a UI_BlendShapeSlider element with a null BlendShapeReference.");
			return;
		}

		if (m_label != null)
			m_label.SetText(a_name);

		m_blendShapeList = a_blendShapeList;
	}

	//~~~Runtime Functions~~~

	//~~~Callback Functions~~~

	public void OnSliderValueChanged()
	{
		float value = m_slider.value * BLEND_MULTIPLIER;
		foreach (var blendShape in m_blendShapeList)
		{
			blendShape.SMR.SetBlendShapeWeight(blendShape.BlendShapeIndex, value);
		}
	}

	//~~~Editor Functions~~~
#if UNITY_EDITOR

	private void OnValidate()
	{
		if (m_slider == null)
		{
			m_slider = GetComponent<Slider>();
		}
	}

#endif

}
