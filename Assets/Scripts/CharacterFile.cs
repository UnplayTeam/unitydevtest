using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterFile
{
	public static string SaveAdress
	{
		get
		{
			return Application.dataPath+"/CharacterSaves/";
		}
	}

	[System.Serializable]
	public struct BlendShape
	{
		public string _name;
		public float _weight;
	}

	public string _name;
	public BlendShape[] _blendShapes;
}
