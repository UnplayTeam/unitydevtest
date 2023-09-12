using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    enum SubmenuOptions
	{
        Biology,
        Body,
        Face,
        ID,
        None
	}

    public HoverChecker _leftPanel;

    public UnityEngine.UI.Button _biologyButton;
    public UnityEngine.UI.Button _bodyButton;
    public UnityEngine.UI.Button _faceButton;
    public UnityEngine.UI.Button _idButton;

    public BiologyMenu _biologyMenu;
    public BodyMenu _bodyMenu;
    public FaceMenu _faceMenu;

    private SubmenuOptions m_curMenu = SubmenuOptions.None;

    // Start is called before the first frame update
    void Start()
    {
        _biologyButton.onClick.AddListener(BioButtonPressed);
        _bodyButton.onClick.AddListener(BodyButtonPressed);
        _faceButton.onClick.AddListener(FaceButtonPressed);
        _idButton.onClick.AddListener(IDButtonPressed);

        SetSubmenu(SubmenuOptions.Biology);
    }

	private void BioButtonPressed()
	{
        SetSubmenu(SubmenuOptions.Biology);
    }

    private void BodyButtonPressed()
    {
        SetSubmenu(SubmenuOptions.Body);
    }

    private void FaceButtonPressed()
    {
        SetSubmenu(SubmenuOptions.Face);
    }

    private void IDButtonPressed()
    {
        SetSubmenu(SubmenuOptions.ID);
    }

    private void SetSubmenu(SubmenuOptions newMenu)
	{
        if (m_curMenu == newMenu)
            return;

        _biologyMenu.gameObject.SetActive(newMenu == SubmenuOptions.Biology);
        _bodyMenu.gameObject.SetActive(newMenu == SubmenuOptions.Body);
        _faceMenu.gameObject.SetActive(newMenu == SubmenuOptions.Face);

        _biologyButton.interactable = newMenu != SubmenuOptions.Biology;
        _bodyButton.interactable = newMenu != SubmenuOptions.Body;
        _faceButton.interactable = newMenu != SubmenuOptions.Face;
        _idButton.interactable = newMenu != SubmenuOptions.ID;

        m_curMenu = newMenu;
    }
}
