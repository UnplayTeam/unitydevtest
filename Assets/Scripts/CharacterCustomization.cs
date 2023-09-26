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
    //Array of BlendshapeData ScriptableObjects to read from (captured by the UI)
    public BlendshapeData[] blendshapeData;

    private List<SkinnedMeshRenderer> skmrs;
    private Dictionary<string, SkinnedMeshRenderer> objectNameToSkinnedMeshRenderer
        = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, List<Blendshape>> blendShapeDatabase
        = new Dictionary<string, List<Blendshape>>();


    #region Private Functions

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //We're only interested in SkinnedMeshRenderers with blendshapes
        skmrs = target.GetComponentsInChildren<SkinnedMeshRenderer>()
               .Where(smr => smr.sharedMesh.blendShapeCount > 0)
               .ToList();

        ///For ease of serialization, Blendshape objects no longer hold references to their own 
        ///SkinnedMeshRenderer and instead look them up via name and this Dictionary
        foreach (var smr in skmrs)
        {
            objectNameToSkinnedMeshRenderer.Add(smr.name, smr);
        }

        foreach (var data in blendshapeData)
        {
            AddToKeywordDictionary(data);
            data.OnValueChanged += ChangeBlendshapeValue;
        }
    }


    private void AddToKeywordDictionary(BlendshapeData data)
    {
        foreach (var smr in objectNameToSkinnedMeshRenderer.Values)
        {
            //This LINQ query obtains all the names as-is for each blendshape in a particular SkinnedMeshRenderer
            foreach (var name in (Enumerable.Range(0, smr.sharedMesh.blendShapeCount)
                .Select(x => smr.sharedMesh.GetBlendShapeName(x))))
            {
                //Adding support for a "catch-all" keyword that may want to get all blendshapes for a keyword regardless of modifier
                if (string.IsNullOrEmpty(data.blendshapePositiveModifier) && string.IsNullOrEmpty(data.blendshapeNegativeModifier))
                {
                    if (name.Contains(data.blendshapeKeyword))
                    {
                        var bs = new Blendshape(name, -1, -1, smr.name);
                        bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(name);
                        if (blendShapeDatabase.ContainsKey(data.blendshapeKeyword))
                        {
                            blendShapeDatabase[data.blendshapeKeyword].Add(bs);
                        }
                        else
                        {
                            blendShapeDatabase.Add(data.blendshapeKeyword, new List<Blendshape>() { bs });
                        }
                    }
                }
                else
                {
                    var bs = new Blendshape(name, -1, -1, smr.name);
                    var positiveKeyword = new StringBuilder(data.blendshapeKeyword).Append("_").Append(data.blendshapePositiveModifier);
                    var negativeKeyword = new StringBuilder(data.blendshapeKeyword).Append("_").Append(data.blendshapeNegativeModifier);

                    //We create a new blendshape object if the key_pos or key_neg string matches
                    if (name.EndsWith(positiveKeyword.ToString()) || name.EndsWith(negativeKeyword.ToString()))
                    {
                        ///If there is a match for the positive (the positive field must be 
                        ///filled out in the slider), we also have to check the negative and
                        ///assign them to their respective "positiveIndex" and "negativeIndex"
                        if (name.EndsWith(positiveKeyword.ToString()))
                        {
                            var alt = name.Replace(positiveKeyword.ToString(), negativeKeyword.ToString());

                            bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(name);
                            if (smr.sharedMesh.GetBlendShapeIndex(alt) != -1)
                                bs.negativeIndex = smr.sharedMesh.GetBlendShapeIndex(alt);
                        }
                        ///At this moment, the negative field on the blend shape slider is 
                        ///allowed to be empty/null
                        if (!string.IsNullOrEmpty(data.blendshapeKeyword) && name.EndsWith(negativeKeyword.ToString()))
                        {
                            var alt = name.Replace(negativeKeyword.ToString(), positiveKeyword.ToString());

                            bs.negativeIndex = smr.sharedMesh.GetBlendShapeIndex(name);
                            if (smr.sharedMesh.GetBlendShapeIndex(alt) != -1)
                                bs.positiveIndex = smr.sharedMesh.GetBlendShapeIndex(alt);
                        }

                        if (blendShapeDatabase.ContainsKey(data.blendshapeKeyword))
                        {
                            blendShapeDatabase[data.blendshapeKeyword].Add(bs);
                        }
                        else
                        {
                            blendShapeDatabase.Add(data.blendshapeKeyword, new List<Blendshape>() { bs });
                        }

                    }
                }
            }
        }
    }

    /// <summary>
    /// Changing each relevant SkinnedMeshRenderer and thier blendshapes to a value.
    /// "Listens" to the scriptable objects OnChangedValue Action
    /// </summary>
    /// <param name="data">The data to read from</param>
    /// <param name="optional">The data to be influenced by</param>
    public void ChangeBlendshapeValue(BlendshapeData data, BlendshapeData optional = null)
    {
        if (!blendShapeDatabase.ContainsKey(data.blendshapeKeyword))
        {
            Debug.LogError($"Blendshape {data.blendshapeKeyword} does not exist!");
            return;
        }

        var value = Mathf.Clamp(data.value, -100, 100);
        var blendshapes = blendShapeDatabase[data.blendshapeKeyword];

        ///If we increase the slider to the right, the "positive" blendshape associated with
        ///the keyword increases the blend while resetting the "negative" blendshape
        if (value >= 0)
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.positiveIndex == -1) continue;

                //If the blendhshape value is being influenced by another value
                if (optional)
                {
                    ///We change the value up to the amount that the influenced value is at and proportion it out so that the "opposite" value is
                    ///the opposite value, or 100 minue the influenced value
                    if (blendshape.shapeName.EndsWith(optional.blendshapePositiveModifier))
                    {
                        if (value <= optional.value)
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, optional.value);

                    }
                    else
                    {
                        if (value <= 100f - optional.value)
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, 100f - optional.value);

                        //optional.OnValueChanged += (x, y) => { OverideTargetBlendshape(blendshape, blendshape.positiveIndex, 100f - value); };
                    }

                    //The influenced data now cares about the blendshapes it has influenced
                    blendShapeDatabase[optional.blendshapeKeyword].Add(blendshape);
                }
                else
                {
                    //As of now, the "influencer" stuff is only supported by sliders that go one way 
                    if (data.sliderType == SliderType.JUST_POSITIVE)
                    {
                        if (blendshape.shapeName.EndsWith(data.blendshapePositiveModifier))
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, 100f - value);
                    }
                    else
                    {
                        objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, value);
                    }
                }
                
                if (blendshape.negativeIndex == -1) continue;
                objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }
        }

        else
        {
            foreach (var blendshape in blendshapes)
            {
                if (blendshape.negativeIndex == -1) continue;
                if (optional)
                {
                    if (blendshape.shapeName.EndsWith(optional.blendshapeNegativeModifier))
                    {
                        if (value >= optional.value)
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -optional.value);
                    }
                    else
                    {
                        if (value >= 100f - optional.value)
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -(100f-value));
                    }
                }
                else
                {
                    if (data.sliderType == SliderType.JUST_POSITIVE)
                    {
                        if (blendshape.shapeName.EndsWith(data.blendshapeNegativeModifier))
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -value);
                        else
                            objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -(100f - value));
                    }
                    else
                    {
                        objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.negativeIndex, -value);
                    }
                }
                
                if (blendshape.positiveIndex == -1) continue;
                objectNameToSkinnedMeshRenderer[blendshape.skinnedMeshRendererName].SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var data in blendshapeData)
        {
            data.OnValueChanged -= ChangeBlendshapeValue;
        }
    }
    #endregion
}
