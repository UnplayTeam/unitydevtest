using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlendShape
{
    public string Name;
    private float _weight;
    private int _blendShapeIndex;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    public BlendShape(string name, float weight, int index, SkinnedMeshRenderer skinnedMeshRenderer)
    {
        Name = name;
        _weight = weight;
        _blendShapeIndex = index;
        _skinnedMeshRenderer = skinnedMeshRenderer;
    }

    public void SetWeight(float weight)
    {
        if (_skinnedMeshRenderer == null)
        {
            return;
        }
        _weight = weight;
        _skinnedMeshRenderer.SetBlendShapeWeight(_blendShapeIndex, _weight);
    }
}