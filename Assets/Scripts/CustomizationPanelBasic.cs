using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    public void FeminineBodyTypeSelected()
    {
        characterData.bodyType = BodyType.Feminine;
        UpdateBody();
    }

    public void MasculineBodyTypeSelected()
    {
        characterData.bodyType = BodyType.Masculine;
        UpdateBody();
    }

    public void HumanSpeciesSelected()
    {
        characterData.species = Species.Human;
        UpdateBody();
    }

    public void OrcSpeciesSelected()
    {
        characterData.species = Species.Orc;
        UpdateBody();
    }

    public void ElfSpeciesSelected()
    {
        characterData.species = Species.Elf;
        UpdateBody();
    }

    private void UpdateBody()
    {
        bool isFeminine = characterData.bodyType == BodyType.Feminine;

        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_HUMAN_FEM, 0);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_FEM, 0);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_MASC, 0);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_FEM, 0);
        attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_MASC, 0);

        if (isFeminine)
            attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_HUMAN_FEM, 100);

        switch (characterData.species)
        {
            case Species.Orc:
                if (isFeminine)
                    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_FEM, 100);
                else
                    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ORC_MASC, 100);
                break;
            case Species.Elf:
                if (isFeminine)
                    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_FEM, 100);
                else
                    attributeInventory.SetAttribute(AttributeList.ATTRIBUTE_ELF_MASC, 100);
                break;
        }
    }
}

