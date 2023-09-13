using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterDropDown : MonoBehaviour
{
	
	public TMPro.TextMeshProUGUI _nameTextBox;

	public TMPro.TMP_Dropdown _dropDown;

    public int Value
	{
		get
		{
            return _dropDown.value;
		}
		set
		{
            _dropDown.value = value;
		}
	}

	public void AddListener(UnityAction<int> call)
	{
		_dropDown.onValueChanged.AddListener(call);
	}
	
	public void RemoveListener(UnityAction<int> call)
	{
		_dropDown.onValueChanged.RemoveListener(call);
	}
}
