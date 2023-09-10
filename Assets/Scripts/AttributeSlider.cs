using UnityEngine;

// AttributeSlider is used to set the value of one or more linked attributes.
// The attributes listed in attributesIds will have their value set to the
// slider's value multiplied by the adjustment value (to convert from 0-1 sliders to 0-100 attribute ranges).
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
