using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Singleton class for the overarching program. Actually controls the blend and holds
/// all the blendshapes and SkinnedMeshRenderers
/// </summary>
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

    /// <summary>
    /// Changing each relevant SkinnedMeshRenderer and thier blendshapes to a value.
    /// "Listens" to the slider onValueChangedEvent
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="value"></param>
    public void ChangeBlendshapeValue(string keyword, float value)
    {
        if (!blendShapeDatabase.ContainsKey(keyword)) 
        { 
            Debug.LogError("Blendshape " + keyword + " does not exist!"); 
            return; 
        }

        value = Mathf.Clamp(value, -100, 100);

        var blendshapes = blendShapeDatabase[keyword];

        ///If we increase the slider to the right, the "positive" blendshape associated with
        ///the keyword increases the blend while resetting the "negative" blendshape
        if (value >= 0)
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.positiveIndex == -1) continue;
                blendshape.parentSkinnedMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, value);
                if (blendshape.negativeIndex == -1) continue;
                blendshape.parentSkinnedMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }
        }

        else
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.negativeIndex == -1) continue;
                blendshape.parentSkinnedMeshRenderer.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                if (blendshape.positiveIndex == -1) continue;
                blendshape.parentSkinnedMeshRenderer.SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }
        }

    }

    /// <summary>
    /// Function called at the beginning of the slider's Start() method to add to the 
    /// "blendshape database" dictionary
    /// </summary>
    /// <param name="key"></param>
    /// <param name="pos"></param>
    /// <param name="neg"></param>
    public void AddToKeywordDictionary(string key, string pos, string neg)
    {
        foreach (var smr in skmrs)
        {
            //This LINQ query obtains all the names as-is for each blendshape in a particular SkinnedMeshRenderer
            foreach (var s in (Enumerable.Range(0, smr.sharedMesh.blendShapeCount)
                .Select(x => smr.sharedMesh.GetBlendShapeName(x))))
            {
                //We create a new blendshape object if the key_pos or key_neg string matches
                if (s.Contains(key + "_" + pos) || s.Contains(key + "_" + neg))
                {
                    var bs = new Blendshape(-1, -1, smr);

                    ///If there is a match for the positive (the positive field must be 
                    ///filled out in the slider), we also have to check the negative and
                    ///assign them to their respective "positiveIndex" and "negativeIndex"
                    if (s.Contains(key + "_" + pos))
                    {
                        var alt = s.Replace(key + "_" + pos, key + "_" + neg);

                        bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(s);
                        if (smr.sharedMesh.GetBlendShapeIndex(alt) != -1)
                            bs.negativeIndex = smr.sharedMesh.GetBlendShapeIndex(alt);
                    }
                    ///At this moment, the negative field on the blend shape slider is 
                    ///allowed to be empty/null
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
    }

    #endregion

    #region Private Functions

    private void Initialize()
    {
        //We're only interested in SkinnedMeshRenderers with blendshapes
        skmrs = target.GetComponentsInChildren<SkinnedMeshRenderer>()
               .Where(smr => smr.sharedMesh.blendShapeCount > 0)
               .ToList();
    }
    #endregion

}
