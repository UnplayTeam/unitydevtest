using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    private const float BODY_WEIGHT_THRESHOLD = 0.5f;
    private const float BODY_WEIGHT_ADJUSTMENT_VALUE = 200;
    private const float MUSCULARITY_ADJUSTMENT_VALUE = 100;

    //public void BodyWeightUpdated(float value)
    //{
    //    if (value >= BODY_WEIGHT_THRESHOLD)
    //    {
    //        float adjustedValue = (value - BODY_WEIGHT_THRESHOLD) * BODY_WEIGHT_ADJUSTMENT_VALUE;
    //        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_WEIGHT_HEAVY, adjustedValue);
    //    }
    //    else
    //    {
    //        float adjustedValue = (BODY_WEIGHT_THRESHOLD - value) * BODY_WEIGHT_ADJUSTMENT_VALUE;
    //        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_WEIGHT_THIN, adjustedValue);
    //    }
    //}

    //public void MuscularityUpdated(float value)
    //{
    //    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_MUSCULARITY_HEAVY, value * MUSCULARITY_ADJUSTMENT_VALUE);
    //    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_MUSCULARITY_MID, value * MUSCULARITY_ADJUSTMENT_VALUE);
    //}
}