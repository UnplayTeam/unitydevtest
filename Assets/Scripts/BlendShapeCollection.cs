using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Application = UnityEngine.Application;

[System.Serializable]
public struct BSKeyAlias
{
    public string key;
    public string[] alias;

    public int IsAlias(string name)
	{
        int max = alias.Max(e => name.Contains(e) ? e.Length : -1);

        return (int)Mathf.Max(max,name.Contains(key) ? key.Length : -1);
	}
}

public struct BlendShapeAddress
{
    public int index;
    public string name;
    public SkinnedMeshRenderer renderer;

    public BlendShapeAddress(int index, string name, SkinnedMeshRenderer renderer)
    {
        this.index = index;
        this.name = name;
        this.renderer = renderer;
    }
}

public class BlendShapeCollection : MonoBehaviour
{
    private static BlendShapeCollection Singleton = null;

    public BSKeyAlias[] _bsKeyAliases;

    private Dictionary<string, List<BlendShapeAddress>> m_collection = new Dictionary<string, List<BlendShapeAddress>>();

    // Start is called before the first frame update
    void Start()
    {
        if (Singleton != null)
            Destroy(Singleton);

        Singleton = this;

        SortCollection();
    }

    private void SortCollection(CharacterFile file = null)
	{
        m_collection.Clear();

        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            string prefix = renderer.gameObject.name + "." + renderer.gameObject.name + "_";

            Mesh mesh = renderer.sharedMesh;

            for (int i = 0; i < renderer.sharedMesh.blendShapeCount; i++)
            {
                string key = mesh.GetBlendShapeName(i);

                string aliasName = GetKeyFromAlias(key);

                if (aliasName != null)
                    key = aliasName;
                else
                {
                    key = key.Replace("bs_", "");
                    key = key.Replace(prefix, "");
                }

                if (!m_collection.ContainsKey(key))
                {
                    m_collection[key] = new List<BlendShapeAddress>();
                    Debug.Log(m_collection.Count + ": " + key, renderer.gameObject);
                }

                string oName = mesh.GetBlendShapeName(i);

                if (file != null)
				{
                    CharacterFile.BlendShape blendShape = Array.Find(file._blendShapes,(e => e._name == oName));

                    if (blendShape._weight != 0)
                    {
                        Debug.Log(blendShape._name+" set to "+ blendShape._weight);
                        renderer.SetBlendShapeWeight(i, blendShape._weight);
                    }
                }

                m_collection[key].Add(new BlendShapeAddress(i, oName, renderer));
            }
        }

        var sortedKeys = m_collection.Keys.OrderBy(e => e);

        string allKeys = "";

        foreach (string key in sortedKeys)
        {
            allKeys += key + "\n";
        }

        Debug.Log(allKeys);
    }

    private string GetKeyFromAlias(string key)
	{
        int longestPart = 0;
        BSKeyAlias best = default(BSKeyAlias);

        foreach(BSKeyAlias alias in _bsKeyAliases)
		{
            int part = alias.IsAlias(key);
            if (part > longestPart)
			{
                longestPart = part;
                best = alias;
			}
		}

        return best.key;
	}

    public static float GetBSWeight(string key)
	{
        if (Singleton == null)
            return -1;

        return Singleton.SingletonGetWeight(key);
	}

    private float SingletonGetWeight(string key)
	{
        if (string.IsNullOrEmpty(key) || !m_collection.ContainsKey(key))
            return -1;

		BlendShapeAddress blendShape = m_collection[key][0];

        return blendShape.renderer.GetBlendShapeWeight(blendShape.index);
	}
    
    public static void SetWeight(string key, float value)
	{
        Singleton?.SingletonSetWeight(key, value);
	}

    private void SingletonSetWeight(string key, float value)
	{
        foreach(var blendShape in m_collection[key])
		{
            blendShape.renderer.SetBlendShapeWeight(blendShape.index, value*100);
		}
	}

    /// <summary>
    /// This function is used for setting multiple blend shapes based on a single slider value. Leave keys null or empty to signify if they are not to be used.
    /// </summary>
    /// <param name="firstKey">This key weight is 1 when the value is 0 and is 0 when the value is 0.5 or above</param>
    /// <param name="middleKey">This key weight approaches 1 based on how close the value is to 0.5 and approaches 0 when the value approaches 0 or 1</param>
    /// <param name="endKey">This key weight is 1 when the value is 1 and is 0 when the value is 0.5 or below</param>
    /// <param name="value">A value between 0 and 1 representing the slider lerp</param>
    public static void SetMultiBlendWeights(string firstKey, string middleKey, string endKey, float value)
	{
        Singleton?.SingletonSetMultiBlendWeights(firstKey,middleKey,endKey,value);
	}
    
        
    private void SingletonSetMultiBlendWeights(string firstKey, string middleKey, string endKey, float value)
	{
		if (!string.IsNullOrEmpty(firstKey))
            SetWeight(firstKey, Mathf.Max(0.5f - value, 0) * 2);
        
        if (!string.IsNullOrEmpty(middleKey))
            SetWeight(middleKey, 1-(Mathf.Abs(value - 0.5f) * 2));
        
        if (!string.IsNullOrEmpty(endKey))
            SetWeight(endKey, Mathf.Max(value - 0.5f, 0) * 2);
	}

	#region FILE_IO

    private CharacterFile GetCharacterFile(string name)
	{
        CharacterFile file = new CharacterFile();

        file._name = name;

        List<CharacterFile.BlendShape> shapes = new List<CharacterFile.BlendShape>();

        foreach(string key in m_collection.Keys)
		{
            float weight = SingletonGetWeight(key);

            if (weight == 0)
                continue;

            foreach (BlendShapeAddress bsa in m_collection[key]) {
                shapes.Add(new CharacterFile.BlendShape { _name = bsa.name, _weight = weight});
            }
		}

        file._blendShapes = shapes.ToArray();

        return file;
	}

    public static void SaveCharacterFile(string name)
	{
        Singleton?.SingletonSaveCharacterFile(name);
	}

    private void SingletonSaveCharacterFile(string name)
	{
        string destination = CharacterFile.SaveAdress + name + ".json";

        string jsonFile = JsonUtility.ToJson(GetCharacterFile(name));

        StreamWriter sw = new StreamWriter(destination, false);

        sw.WriteLine(jsonFile);
        sw.Flush();

        Debug.Log(destination + " ~ " + jsonFile);
    }

    public static void LoadCharacterFile(string address)
	{
        Singleton?.SingletonLoadCharacterFile(address);
	}

    private void SingletonLoadCharacterFile(string address)
	{
        Debug.Log("loading "+address);

        string destination = address;

        StreamReader sr = new StreamReader(destination);

        string json = sr.ReadToEnd();

        CharacterFile file = JsonUtility.FromJson<CharacterFile>(json);

        Debug.Log("Unloading "+file._blendShapes.Length+"...");

        SortCollection(file);
    }

	#endregion
}
