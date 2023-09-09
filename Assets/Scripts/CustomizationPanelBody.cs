using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private float threshold = 0.5f;

    public void BodyWeightUpdated(float value)
    {
        if (value >= threshold)
        {
            float adjustedValue = (value - threshold) * 200;
            attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_WEIGHT_HEAVY, adjustedValue);
        }
        else
        {
            float adjustedValue = (threshold - value) * 200;
            attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_WEIGHT_THIN, adjustedValue);
        }
    }
}