using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private CharacterAttributeInventory attributeInventory;
    [SerializeField] private GameObject[] tabs;

    private CharacterData characterData;
    private int tabIndex;

    private void Start()
    {
        characterData = new CharacterData
        {
            bodyType = BodyType.Masculine,
            species = Species.Human
        };
    }

    public void NextTab()
    {
        tabs[tabIndex].SetActive(false);
        tabIndex = ++tabIndex % tabs.Length;
        tabs[tabIndex].SetActive(true);
    }

    public void PreviousTab()
    {
        tabs[tabIndex].SetActive(false);
        tabIndex = Mathf.Abs(--tabIndex) % tabs.Length;
        tabs[tabIndex].SetActive(true);
    }
}
