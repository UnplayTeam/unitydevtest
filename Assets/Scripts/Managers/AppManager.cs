using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public CharacterManager CharacterManager;
    public UIManager UIManager;

    void Start()
    {
        SetupManagers();
    }

    private void SetupManagers()
    {
        CharacterManager.Init(this);
        UIManager.Init(this);
    }
}
