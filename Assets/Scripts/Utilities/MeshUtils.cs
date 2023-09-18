namespace RPG {
  public static class MeshUtils {
    // Unity's docs state that the weight range is defined by the model, in practice the sample mesh is 0-100
    // A safer method would be checking first and changing the range in the AvatarBaseMeshData, Skipping for time reasons
    // https://docs.unity3d.com/ScriptReference/SkinnedMeshRenderer.SetBlendShapeWeight.html
    public const float BlendShapeWeightMin = 0f;
    public const float BlendShapeWeightMax = 100f;
  }
}
