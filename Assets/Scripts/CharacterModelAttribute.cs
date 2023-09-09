using UnityEngine;
using System;

[Serializable]
public class CharacterModelAttribute
{
    public string attributeId;
    public AttributeBlendShape[] blendShapes;
}

[Serializable]
public struct AttributeBlendShape
{
    public SkinnedMeshRenderer meshRenderer;
    public int attributeIndex;
}

