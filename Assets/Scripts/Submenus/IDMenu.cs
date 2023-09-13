using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IDMenu : MonoBehaviour
{
	public TMPro.TMP_InputField _tb_name;

	public UnityEngine.UI.Button _restartButton;
	public UnityEngine.UI.Button _saveButton;
	public UnityEngine.UI.Button _loadButton;

	public TMPro.TMP_Dropdown _dd_saves;
	public CameraTarget _cameraTarget;

	private List<string> m_saves = new List<string>();
	private string m_directory;

	void Start()
    {
		_restartButton.onClick.AddListener(RestartPressed);
		_saveButton.onClick.AddListener(SavePressed);
		_loadButton.onClick.AddListener(LoadPressed);

		UpdateSavesDropDown();
	}

	private void RestartPressed()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void SavePressed()
	{
		if(_tb_name.text == "")
			_tb_name.text = "Unamed_Character";
		BlendShapeCollection.SaveCharacterFile(_tb_name.text.Replace(" ","_"));
		UpdateSavesDropDown();
	}
	private void LoadPressed()
	{
		_tb_name.text = _dd_saves.options[_dd_saves.value].text;
		BlendShapeCollection.LoadCharacterFile(m_saves[_dd_saves.value]);
	}

	private void UpdateSavesDropDown()
	{
		m_saves = new List<string>(Directory.GetFiles(CharacterFile.SaveAdress));

		m_saves.RemoveAll(e => !e.EndsWith(".json"));

		_dd_saves.options = new List<TMPro.TMP_Dropdown.OptionData>();

		foreach (string file in m_saves) 
		{
			int index = file.LastIndexOf("/")+1;
			string shortFile = file.Substring(index,file.Length-index-5);

			_dd_saves.options.Add(new TMPro.TMP_Dropdown.OptionData(shortFile));
		}

		_loadButton.interactable = _dd_saves.options.Count > 0;
		_dd_saves.interactable = _dd_saves.options.Count > 0;

		_dd_saves.value = 0;
	}

	public void ApplyCameraTarget()
	{
		CharacterCameraController.SetCameraTarget(_cameraTarget);
	}
}
