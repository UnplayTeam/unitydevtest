using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for the blendshapes
/// </summary>
public class Blendshape
{
    public int positiveIndex { get; set; }
    public int negativeIndex { get; set; }
    ///We have a reference to the SkinnedMeshRenderer here because a single blend slider
    ///has the potential to affect multple SkinnedMeshRenderers throughout the whole model,
    ///so we have a reference to the particular SkinnedMeshRenderer in which each affected
    ///blendshape can then use to apply the blend
    public SkinnedMeshRenderer parentSkinnedMeshRenderer { get; set; }

    public Blendshape(int positiveIndex, int negativeIndex, SkinnedMeshRenderer smr)
    {
        this.positiveIndex = positiveIndex;
        this.negativeIndex = negativeIndex;
        this.parentSkinnedMeshRenderer = smr;
    }
}
