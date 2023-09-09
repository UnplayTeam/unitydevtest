using UnityEngine;

public class CharacterAttributeInventory : MonoBehaviour
{
    [SerializeField] private CharacterModelAttribute[] characterAttributes;

    public void SetAttribute(string attributeId, float value)
    {
        foreach(CharacterModelAttribute attribute in characterAttributes)
        {
            if (attribute.attributeId != attributeId)
            {
                continue;
            }

            foreach(AttributeBlendShape blendShape in attribute.blendShapes)
            {
                blendShape.meshRenderer.SetBlendShapeWeight(blendShape.attributeIndex, value);
            }
        }
    }
}

