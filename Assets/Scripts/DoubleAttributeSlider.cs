using UnityEngine;

// DoubleAttributeSlider is used to set the value of two opposing attributes.
// The slider starts at value 0.5, which means that each of its two attribute
// values are 0. As the slider moves from 0.5 to 1 (aka closer to the second attribute),
// the value of the second attribute will increase and the value of the first
// attribute remains at 0. As the slider moves from 0.5 to 0 (aka closer to the
// first attribute), the value of the first attribute will increase and the value
// of the second attribute remains at 0.
//
// BodyWeight is a good example of where using DoubleAttributeSlider is appropriate.
// The two opposing attributes are "body_weight_thin" and "body_weight_heavy".
// Since only one attribute can have a non-zero value at a time, using
// DoubleAttributeSlider ensures that moving the slider further towards one of the
// two attributes has the desired impact on the character model.
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
