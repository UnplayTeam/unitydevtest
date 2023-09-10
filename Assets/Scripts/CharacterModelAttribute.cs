using UnityEngine;
using System;

// A CharacterModelAttribute is a collection of AttributeBlendShapes that share
// an attributeId.
[Serializable]
public class CharacterModelAttribute
{
    public string attributeId;
    public AttributeBlendShape[] blendShapes;
}

// An AttributeBlendShape stores the blend shape index of a specific attributeId
// within a mesh renderer's array of blend shapes.
[Serializable]
public struct AttributeBlendShape
{
    public SkinnedMeshRenderer meshRenderer;
    public int attributeIndex;
}

