using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
  [Serializable]
  public struct AvatarCustomizationDataEntry {
    public string GroupName;
    [Range(MeshUtils.BlendShapeWeightMin, MeshUtils.BlendShapeWeightMin)] public float Value;
  }
}
