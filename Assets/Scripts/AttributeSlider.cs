using UnityEngine;

public class AttributeSlider : MonoBehaviour
{
    [SerializeField] CustomizationPanel customizationPanel;
    [SerializeField] string[] attributeIds;

    private const float ATTRIBUTE_ADJUSTMENT_VALUE = 100;

    public void SliderValueChanged(float value)
    {
        foreach(string attributeId in attributeIds)
        {
            customizationPanel.SetAttributeValue(attributeId, value * ATTRIBUTE_ADJUSTMENT_VALUE);
        }
    }
}
