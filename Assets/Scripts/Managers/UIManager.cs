using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform DialogViewport;

    [Header("Dialogs")]
    public GameObject CharacterCustomizationDialog;

    private AppManager _appManager;

    public void Init(AppManager appManager)
    {
        _appManager = appManager;
    }

    // with any more dialogs, I would create a dialog base class with a shared Init() function 
    // and use this function for all dialogs
    public void OpenCharacterCustomizationDialog(Character character)
    {
        GameObject dialogGameObject = Instantiate(CharacterCustomizationDialog);
        CharacterCustomizationDialog characterCustomizationDialog = dialogGameObject.GetComponent<CharacterCustomizationDialog>();
        dialogGameObject.transform.SetParent(DialogViewport, false);

        characterCustomizationDialog.Init(_appManager, character);
    }
}
