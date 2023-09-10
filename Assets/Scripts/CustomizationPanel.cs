using UnityEngine;

public partial class CustomizationPanel : MonoBehaviour
{
    [SerializeField] private CharacterAttributeInventory attributeInventory;
    [SerializeField] private GameObject[] tabs;

    private int tabIndex;

    private void Start()
    {
        SetupBasicPanel();
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
