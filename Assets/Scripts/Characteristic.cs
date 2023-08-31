using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacteristicType
{
    None = 0,
    HUMAN,
    ORC,
    ELF,
    MUSCULARITY,
    WEIGHT,
    FEMME
}


// TODO the data in this class would be more appropriately baked in Protobuf, JSON, YAML or whatever other data
// designers and artists feel most comfortable with, but for sake of keeping it all accessible, 
// I have it all editable in the inspector!
public class Characteristic : MonoBehaviour
{
    public CharacteristicType Type;

    [SerializeField]
    private List<string> _characteristicPositiveValues;
    [SerializeField]
    private List<string> _characteristicNegativeValues;

    public bool HasNegativeValues
    {
        get { return _characteristicNegativeValues != null && _characteristicNegativeValues.Count > 0; }
    }

    public bool IsPositivelyOrNegativelyRelatedToCharacteristic(string blendShapeName)
    {
        return IsPositivelyRelatedToCharacteristic(blendShapeName) || IsNegativelyRelatedToCharacteristic(blendShapeName);
    }

    public bool IsPositivelyRelatedToCharacteristic(string blendShapeName)
    {
        return IsRelatedToCharacteristic(blendShapeName, true);
    }

    public bool IsNegativelyRelatedToCharacteristic(string blendShapeName)
    {
        return IsRelatedToCharacteristic(blendShapeName, false);
    }

    private bool IsRelatedToCharacteristic(string blendShapeName, bool related)
    {
        if (string.IsNullOrEmpty(blendShapeName))
        {
            return false;
        }
        List<string> listToCheck = related ? _characteristicPositiveValues : _characteristicNegativeValues;
        foreach (string characteristic in listToCheck)
        {
            if (blendShapeName.Contains(characteristic))
            {
                return true;
            }
        }
        return false;
    }

    public float CalculateWeight(float weight, string blendShapeName)
    {
        if (!HasNegativeValues)
        {
            return weight;
        }
        if (IsNegativelyRelatedToCharacteristic(blendShapeName))
        {
            float adjustedWeight = 100 - weight;
            return adjustedWeight > 0 ? adjustedWeight : 0;
        }
        if (IsPositivelyRelatedToCharacteristic(blendShapeName))
        {
            float adjustedWeight = weight - 100;
            return adjustedWeight > 0 ? adjustedWeight : 0;
        }
        return weight;
    }
}