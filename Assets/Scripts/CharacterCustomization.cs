using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CharacterCustomization : Singleton<CharacterCustomization>
{
    public GameObject target;

    private List<SkinnedMeshRenderer> skmrs;

    private Dictionary<string, List<Blendshape>> blendShapeDatabase 
        = new Dictionary<string, List<Blendshape>>();

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }


    #region Public Functions

    public void ChangeBlendshapeValue(string keyword, float value)
    {
        if (!blendShapeDatabase.ContainsKey(keyword)) { Debug.LogError("Blendshape " + keyword + " does not exist!"); return; }

        value = Mathf.Clamp(value, -100, 100);

        var blendshapes = blendShapeDatabase[keyword];

        if (value >= 0)
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.positiveIndex == -1) continue;
                blendshape.skinnedMesh.SetBlendShapeWeight(blendshape.positiveIndex, value);
                if (blendshape.negativeIndex == -1) continue;
                blendshape.skinnedMesh.SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }
        }

        else
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.negativeIndex == -1) continue;
                blendshape.skinnedMesh.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                if (blendshape.positiveIndex == -1) continue;
                blendshape.skinnedMesh.SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }
        }

    }

    #endregion

    #region Private Functions

    private void Initialize()
    {
        skmrs = target.GetComponentsInChildren<SkinnedMeshRenderer>()
               .Where(smr => smr.sharedMesh.blendShapeCount > 0)
               .ToList();
    }

    public void AddToKeywordDictionary(string key, string pos, string neg)
    {
        foreach (var smr in skmrs)
        {
            foreach(var s in (Enumerable.Range(0, smr.sharedMesh.blendShapeCount)
                .Select(x => smr.sharedMesh.GetBlendShapeName(x))))
            {
                if (s.Contains(key + "_" + pos) || s.Contains(key + "_" + neg))
                {
                    var bs = new Blendshape(-1, -1, smr);

                    if (s.Contains(key + "_" + pos))
                    {
                        var alt = s.Replace(key + "_" + pos, key + "_" + neg);

                        bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(s);
                        if (smr.sharedMesh.GetBlendShapeIndex(alt) != -1)
                            bs.negativeIndex = smr.sharedMesh.GetBlendShapeIndex(alt);
                    }
                    if (!string.IsNullOrEmpty(neg) && s.Contains(key + "_" + neg))
                    {
                        var alt = s.Replace(key + "_" + neg, key + "_" + pos);

                        bs.negativeIndex = smr.sharedMesh.GetBlendShapeIndex(s);
                        if (smr.sharedMesh.GetBlendShapeIndex(alt) != -1)
                            bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(alt);
                    }

                    if (blendShapeDatabase.ContainsKey(key))
                    {
                        blendShapeDatabase[key].Add(bs);
                    }
                    else
                    {
                        blendShapeDatabase.Add(key, new List<Blendshape>() { bs });
                    }
                }
            }
        }

        /*//Get all blendshape names
        Dictionary<string, SkinnedMeshRenderer> nameToRenderer = new Dictionary<string, SkinnedMeshRenderer>();
        foreach (var smr in skmrs) 
        {
            foreach (var bs in (Enumerable.Range(0, smr.sharedMesh.blendShapeCount)).Select(x => smr.sharedMesh.GetBlendShapeName(x)))
            {
                nameToRenderer.Add(bs, smr);
            }
        }

        foreach (var name in nameToRenderer.Keys)
        {
            string noPrefix;
            //Removes the beginning prefix
            noPrefix = name.Remove(0, name.IndexOf('.') + 1);
            noPrefix = noPrefix.Remove(0, noPrefix.IndexOf("_") + 1);
            if (noPrefix.ElementAt(0).Equals('L') || noPrefix.ElementAt(0).Equals('R'))
            {
                noPrefix = noPrefix.Remove(0, noPrefix.IndexOf("_") + 1);
            }

            string positiveName = string.Empty, negativeName = string.Empty;

            int postiveIndex = -1, negativeIndex = -1;
            postiveIndex = nameToRenderer[name].sharedMesh.GetBlendShapeIndex(name);

            if (blendShapeDatabase.ContainsKey(noPrefix))
                blendShapeDatabase[noPrefix].Add(new Blendshape(postiveIndex, negativeIndex, nameToRenderer[name]));
            else
                blendShapeDatabase.Add(noPrefix, new List<Blendshape>() { new Blendshape(postiveIndex, negativeIndex, nameToRenderer[name]) });
        }*/
    }

    #endregion

    //Get all registered Blendshapes names without suffixes (The Dictionary Keys)
    public string[] GetBlendShapeNames()
    {
        return blendShapeDatabase.Keys.ToArray();
    }

    public int GetNumberOfEntries()
    {
        return blendShapeDatabase.Count;
    }

/*    public Blendshape GetBlendshape(string name)
    {
        return blendShapeDatabase[name];
    }*/

    public void ClearDatabase()
    {
        blendShapeDatabase.Clear();
    }
}
