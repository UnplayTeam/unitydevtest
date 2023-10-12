using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static BlendShaper;

public class BlendShapeSliderGroup : MonoBehaviour
{
	//~~~Definitions~~~

	//~~~Variables~~~
	//Serialized
	[SerializeField]
	private TextMeshProUGUI m_label;
	[SerializeField]
	private Transform m_slidersRoot;
	[SerializeField]
	private BlendShapeSlider m_sliderPrefab;

	//Non-Serialized
	private List<BlendShapeSlider> m_sliders = new List<BlendShapeSlider>();
	private System.Action<BlendShapeSliderGroup> m_onLabelClicked;

	//~~~Accessors~~~
	public bool Expanded { get; private set; } = false;

	//~~~Unity Functions~~~

	//~~~Runtime Functions~~~
	public void Intialize(string a_name, Dictionary<string, List<BlendShapeData>> a_subcategoryData, System.Action<BlendShapeSliderGroup> a_onLabelClicked)
	{
		if (a_subcategoryData == null)
		{
			Debug.LogError("Attempted to initialize a BlendShapeSliderGroup element with a null BlendShapeReference.");
			return;
		}

		m_onLabelClicked = a_onLabelClicked;

		if (m_label != null)
			m_label.SetText(a_name);

		GenerateSliders(a_subcategoryData);
	}

	private void GenerateSliders(Dictionary<string, List<BlendShapeData>> a_subcategoryData)
	{
		foreach (var subcategory in a_subcategoryData.Keys)
		{
			BlendShapeSlider slider = Instantiate<BlendShapeSlider>(m_sliderPrefab, m_slidersRoot);
			slider.Intialize(subcategory, a_subcategoryData[subcategory]);
			slider.gameObject.SetActive(false);
			m_sliders.Add(slider);
		}
	}

	public void ExpandGroup(bool a_expanded)
	{
		if (a_expanded == Expanded)
			return; //Already in expected state

		foreach (var slider in m_sliders)
		{
			slider.gameObject.SetActive(a_expanded);
		}

		Expanded = a_expanded;
	}

	//~~~Callback Functions~~~

	public void OnLabelClicked()
	{
		m_onLabelClicked(this);
	}

	//~~~Editor Functions~~~
#if UNITY_EDITOR

#endif

}
