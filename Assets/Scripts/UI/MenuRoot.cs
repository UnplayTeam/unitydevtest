using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI {
  public class MenuRoot : MonoBehaviour {
    [SerializeField] private MenuPanel _StartingMenuPanel;
    
    public UnityEvent OnCurrentPanelChanged = new ();
    
    public MenuPanel CurrentMenuPanel => _CurrentMenuPanel;
    
    private MenuPanel _CurrentMenuPanel;
    
    public void ShowMenuPanel (MenuPanel menuPanel) {
      if (_CurrentMenuPanel == menuPanel || menuPanel == null) {
        return;
      }
      MenuPanel previousMenuPanel = _CurrentMenuPanel;
      _CurrentMenuPanel = menuPanel;
      if (previousMenuPanel != null) {
        previousMenuPanel.Hide ();
      }
      menuPanel.Show ();
      OnCurrentPanelChanged.Invoke ();
    }

    private void Start () {
      ShowMenuPanel (_StartingMenuPanel);
    }
  }
}
