using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public struct BlendShape
{
    public int index;
    public SkinnedMeshRenderer renderer;

    public BlendShape(int index, SkinnedMeshRenderer renderer)
    {
        this.index = index;
        this.renderer = renderer;
    }
}

public class BlendShapeCollection : MonoBehaviour
{
    public static BlendShapeCollection Singleton = null;

    public BSKeyAlias[] _bsKeyAliases;

    private Dictionary<string, List<BlendShape>> m_collection;

    // Start is called before the first frame update
    void Start()
    {
        if (Singleton != null)
            Destroy(Singleton);

        Singleton = this;

        m_collection = new Dictionary<string, List<BlendShape>>();

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
                    m_collection[key] = new List<BlendShape>();
                    Debug.Log(m_collection.Count + ": " + key, renderer.gameObject);
                }

                m_collection[key].Add(new BlendShape(i,renderer));
            }
        }

        var sortedKeys = m_collection.Keys.OrderBy(e => e);

        string allKeys = "";
        
        foreach(string key in sortedKeys)
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

    public float GetWeight(string key)
	{
		BlendShape blendShape = m_collection[key][0];

        return blendShape.renderer.GetBlendShapeWeight(blendShape.index);
	}
    
    public void SetWeight(string key, float value)
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
    public void SetMultiBlendWeights(string firstKey, string middleKey, string endKey, float value)
	{
		if (!string.IsNullOrEmpty(firstKey))
            SetWeight(firstKey, Mathf.Max(0.5f - value, 0) * 2);
        
        if (!string.IsNullOrEmpty(middleKey))
            SetWeight(middleKey, 1-(Mathf.Abs(value - 0.5f) * 2));
        
        if (!string.IsNullOrEmpty(endKey))
            SetWeight(endKey, Mathf.Max(value - 0.5f, 0) * 2);
	}
}
