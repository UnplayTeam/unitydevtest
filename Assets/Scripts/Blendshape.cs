using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blendshape
{
    public int positiveIndex { get; set; }
    public int negativeIndex { get; set; }
    public SkinnedMeshRenderer skinnedMesh { get; set; }

    public Blendshape(int positiveIndex, int negativeIndex, SkinnedMeshRenderer smr)
    {
        this.positiveIndex = positiveIndex;
        this.negativeIndex = negativeIndex;
        this.skinnedMesh = smr;
    }
}
