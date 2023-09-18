using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
  public class MenuPanelTab : MonoBehaviour {
    [SerializeField] private MenuPanel _MenuPanel;
    [SerializeField] private MenuRoot _MenuRoot;
    [SerializeField] private Button _Button;

    public void SwitchTo () {
      _MenuRoot.ShowMenuPanel (_MenuPanel);
    }

    private void Start () {
      _MenuRoot.OnCurrentPanelChanged.AddListener (RefreshButtonState);
      _Button.onClick.AddListener (SwitchTo);
      RefreshButtonState ();
    }

    private void RefreshButtonState () {
      _Button.interactable = _MenuRoot.CurrentMenuPanel != _MenuPanel;
    }
  }
}
