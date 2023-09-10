using UnityEngine;

// CharacterAttributeInventory keeps track of all of the model's attributes.
// The SetAttribute method is used to update the value of a given attribute,
// modifying the blend shape weight of all of the blend shapes  that include
// the supplied attributeId.
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

