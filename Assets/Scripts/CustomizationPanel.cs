using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private CharacterAttributeInventory attributeInventory;

    private CharacterData characterData;

    private void Start()
    {
        characterData = new CharacterData
        {
            bodyType = BodyType.Masculine,
            species = Species.Human
        };
    }
}
