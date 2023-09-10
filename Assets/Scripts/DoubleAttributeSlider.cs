using UnityEngine;

public class DoubleAttributeSlider : MonoBehaviour
{
    [SerializeField] CustomizationPanel customizationPanel;
    [SerializeField] string firstAttributeId;
    [SerializeField] string secondAttributeId;

    private const float ATTRIBUTE_THRESHOLD = 0.5f;
    private const float ATTRIBUTE_ADJUSTMENT_VALUE = 200;

    public void SliderValueChanged(float value)
    {
        if (value >= ATTRIBUTE_THRESHOLD)
        {
            float adjustedValue = (value - ATTRIBUTE_THRESHOLD) * ATTRIBUTE_ADJUSTMENT_VALUE;
            customizationPanel.SetAttributeValue(secondAttributeId, adjustedValue);
        }
        else
        {
            float adjustedValue = (ATTRIBUTE_THRESHOLD - value) * ATTRIBUTE_ADJUSTMENT_VALUE;
            customizationPanel.SetAttributeValue(firstAttributeId, adjustedValue);
        }
    }
}
