using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public CharacterManager CharacterManager;
    public UIManager UIManager;

    // Start is called before the first frame update
    void Start()
    {
        SetupManagers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetupManagers()
    {
        CharacterManager.Init(this);
        UIManager.Init(this);
    }
}
