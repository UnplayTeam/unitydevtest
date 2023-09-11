using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlider : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _nameTextBox;
    public TMPro.TextMeshProUGUI _leftTextBox;
    public TMPro.TextMeshProUGUI _rightTextBox;
    private Slider m_slider;

    public Slider Slider
	{
		get
		{
            return m_slider;
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
    }
}
