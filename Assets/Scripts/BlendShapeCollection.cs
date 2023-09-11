using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct BSKeyAlias
{
    public string key;
    public string alias;

    public int IsAlias(string name)
	{
        return name.Contains(alias) ? alias.Length : -1;
	}
}

public struct BlendShape
{
    public int index;
    public Mesh mesh;

    public BlendShape(int index, Mesh mesh)
    {
        this.index = index;
        this.mesh = mesh;
    }
}

public class BlendShapeCollection : MonoBehaviour
{
    public BSKeyAlias[] _bsKeyAliases;

    private Dictionary<string, List<BlendShape>> m_collection;

    // Start is called before the first frame update
    void Start()
    {
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

                m_collection[key].Add(new BlendShape(i,mesh));
            }
        }

        string allKeys = "";
        
        foreach(string key in m_collection.Keys)
		{
            allKeys += key + "\n";
		}

        Debug.Log(allKeys);
    }

    public string GetKeyFromAlias(string key)
	{
        int longestPart = 0;
        BSKeyAlias best = default(BSKeyAlias);

        foreach(var alias in _bsKeyAliases)
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
}
