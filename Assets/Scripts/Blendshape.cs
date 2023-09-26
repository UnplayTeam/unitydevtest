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
    public string skinnedMeshRendererName { get; set; }
    public string shapeName { get; set; }

    public Blendshape(string shapeName, int positiveIndex, int negativeIndex, string skinnedMeshRendererName)
    {
        this.shapeName = shapeName;
        this.positiveIndex = positiveIndex;
        this.negativeIndex = negativeIndex;
        this.skinnedMeshRendererName = skinnedMeshRendererName;
    }
}
