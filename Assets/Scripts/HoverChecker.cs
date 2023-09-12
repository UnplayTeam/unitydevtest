using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool m_hovering = false;

	public bool MouseHovering
	{
		get
		{
			return m_hovering;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_hovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_hovering = false;
	}
}
