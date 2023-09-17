using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
  public static class TransformUtils {
    public static void AlignWith (this Transform transform, Transform alignWith) {
      transform.position = alignWith.position;
      transform.rotation = alignWith.rotation;
      transform.localScale = alignWith.localScale;
    }
  }
}
