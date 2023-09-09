using UnityEngine;
using System;

public class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private CharacterAttributeInventory attributeInventory;

    public void SetFemale()
    {
        attributeInventory.SetAttribute("gender_fem", 100);
    }

    public void SetMale()
    {
        attributeInventory.SetAttribute("gender_fem", 0);
    }
}
