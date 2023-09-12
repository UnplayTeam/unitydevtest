using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDropDown : MonoBehaviour
{
	// Start is called before the first frame update
	public TMPro.TextMeshProUGUI _nameTextBox;
	public TMPro.TMP_Dropdown _DropDown;

    public int Value
	{
		get
		{
            return _DropDown.value;
		}
		set
		{
            _DropDown.value = value;
		}
	}

	public void AddListener(UnityAction<int> call)
	{
		_DropDown.onValueChanged.AddListener(call);
	}
	
	public void RemoveListener(UnityAction<int> call)
	{
		_DropDown.onValueChanged.RemoveListener(call);
	}
}
