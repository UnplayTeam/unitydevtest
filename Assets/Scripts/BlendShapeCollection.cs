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

                key = key.Replace("bs_", "");
                key = key.Replace(prefix, "");

                string aliasName = GetKeyFromAlias(key);

                if (aliasName != null)
                    key = aliasName;

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

    public void SetWeight(string key, float value)
	{
        foreach(var blendShape in m_collection[key])
		{
            blendShape.renderer.SetBlendShapeWeight(blendShape.index, value*100);
		}
	}
}
