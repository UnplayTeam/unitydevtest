using UnityEngine;

// The CustomizationPanel is the primary UI element. It contains a set of
// tabs, each of which contains controls for a specific region of the character model.
// It serves as the connection between individual attribute sliders and the
// attributeInventory (which in turns controls the chracter's mesh renderer blend shapes).
public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private CharacterAttributeInventory attributeInventory;
    [SerializeField] private GameObject[] tabs;

    private int tabIndex;

    private void Start()
    {
        SetupSpeciesPanel();
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
        tabIndex--;
        if (tabIndex == -1) tabIndex = tabs.Length - 1;
        tabs[tabIndex].SetActive(true);
    }

    public void SetAttributeValue(string attributeId, float value)
    {
        attributeInventory.SetAttribute(attributeId, value);
    }
}
