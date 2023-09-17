using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Character.Avatar {
  /// <summary>
  /// Represents the avatar of a CharacterPawn. This is the component holds the data for the avatar's customization and applies it to the avatar's meshes.
  /// </summary>
  public class CharacterPawnAvatar : MonoBehaviour {
    [SerializeField] private AvatarBaseMeshData _BaseMeshData;
    [SerializeField] private SkinnedMeshRenderer[] _CustomizableMeshes;
    [SerializeField] private List<AvatarCustomizationDataEntry> _CustomizationDataEntries = new ();

    // Used to speed up lookup in-game
    private Dictionary<string, SkinnedMeshRenderer> _CustomizableMeshesDictionary = new ();
    
    /// <summary>
    /// Refreshes the avatar's meshes with the current customization data.
    /// </summary>
    public void RefreshAvatar () {
      foreach (AvatarCustomizationDataEntry entry in _CustomizationDataEntries) {
        RefreshEntry (entry);
      }
    }
    
    public void ResetAvatar () {
      _CustomizationDataEntries.Clear ();
      RefreshAvatar ();
      foreach (SkinnedMeshRenderer skinnedMeshRenderer in _CustomizableMeshes) {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++) {
          skinnedMeshRenderer.SetBlendShapeWeight (i, 0f);
        }
      }
      ResetBlendShapeWeights (true);
    }

    /// <summary>
    /// Refresh a specific entry on the avatar's meshes with the current customization data.
    /// </summary>
    /// <param name="entry">The entry to refresh</param>
    public void RefreshEntry (AvatarCustomizationDataEntry entry) {
      if (_BaseMeshData.TryGetBlendShapeGroup (entry.GroupName, out AvatarBaseMeshData.BlendShapeGroup blendShapeGroup)) {
        foreach (AvatarBaseMeshData.BlendShapeData blendShape in blendShapeGroup.BlendShapes) {
          if (TryGetCustomizableMesh (blendShape.MeshName, out SkinnedMeshRenderer customizableMesh)) {
            customizableMesh.SetBlendShapeWeight (blendShape.Index, entry.Value);
          }
        }
      }
    }
    
    /// <summary>
    /// Get the value of a blend shape group held by this avatar.
    /// </summary>
    /// <param name="blendShapeGroup">The BlendShapeGroup to retrieve a value for</param>
    /// <returns>The held customization value associated with this group within this avatar instance</returns>
    public float GetBlendShapeValue (AvatarBaseMeshData.BlendShapeGroup blendShapeGroup) {
      foreach (AvatarCustomizationDataEntry entry in _CustomizationDataEntries) {
        if (entry.GroupName == blendShapeGroup.BlendGroupName) {
          return entry.Value;
        }
      }
      return 0f;
    }
    
    /// <summary>
    /// Sets the value(s) of a BlendShapeGroup to the customizable meshes held by this avatar
    /// </summary>
    /// <param name="blendShapeGroup">The BlendShapeGroup used to map the provided value to the BlendShapes held on this avatar</param>
    /// <param name="value">The value to apply, will clamp between 0 and 100f as that is the defined range for all BlendShapes</param>
    /// <remarks>BlendShapeGroups hold multiple BlendShapeData entries, each of which is applied to this Avatar assuming a mesh name match is found</remarks>
    public void SetBlendShapeValue (AvatarBaseMeshData.BlendShapeGroup blendShapeGroup, float value) {
      float clampedValue = Mathf.Clamp (value, 0f, 100f);
      AvatarCustomizationDataEntry newEntry = new() {
        GroupName = blendShapeGroup.BlendGroupName,
        Value = clampedValue
      };
      for (int i = 0; i < _CustomizationDataEntries.Count; i++) {
        if (_CustomizationDataEntries[i].GroupName == blendShapeGroup.BlendGroupName) {
          _CustomizationDataEntries[i] = newEntry;
          RefreshEntry (newEntry);
          return;
        }
      }
      _CustomizationDataEntries.Add (newEntry);
      RefreshEntry (newEntry);
    }
    
    /// <summary>
    /// Attempt to retrieve a SkinnedMeshRenderer from this avatar's customizable meshes with a matching mesh name
    /// </summary>
    /// <param name="meshName">The mesh name to look for within this avatar</param>
    /// <param name="customizableMesh">A SkinnedMeshRenderer component that holds a mesh with the given name</param>
    /// <returns>True if a matching SkinnedMeshRenderer was found, otherwise false</returns>
    public bool TryGetCustomizableMesh (string meshName, out SkinnedMeshRenderer customizableMesh) {
#if UNITY_EDITOR
      // Only do this so BaseModelTesterWindow can find the SkinnedMeshRenderer for editor testing
      if (!Application.isPlaying) {
        customizableMesh = _CustomizableMeshes.FirstOrDefault(smr => smr.name == meshName);
        return customizableMesh != null;
      }
#endif
      return _CustomizableMeshesDictionary.TryGetValue (meshName, out customizableMesh);
    }

    // Unity
    private void Awake () {
      RefreshAvatar ();
      RefreshCustomizableMeshesDictionary ();
    }
    
    // Internal
    private void RefreshCustomizableMeshesDictionary () {
      _CustomizableMeshesDictionary.Clear ();
      foreach (SkinnedMeshRenderer customizableMesh in _CustomizableMeshes) {
        _CustomizableMeshesDictionary.Add (customizableMesh.name, customizableMesh);
      }
    }
    
    private void GetAllCustomizableMeshes () {
      List<SkinnedMeshRenderer> candidates = new (GetComponentsInChildren<SkinnedMeshRenderer> ());
      candidates.RemoveAll (smr => smr.sharedMesh.blendShapeCount == 0);
      _CustomizableMeshes = candidates.ToArray ();
    }
    
    private void ResetBlendShapeWeights (bool all = false) {
      foreach (SkinnedMeshRenderer skinnedMeshRenderer in _CustomizableMeshes) {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++) {
          if (all) {
            skinnedMeshRenderer.SetBlendShapeWeight (i, 0f);
          } else {
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName (i);
            if (_CustomizationDataEntries.All (entry => entry.GroupName != blendShapeName)) {
              skinnedMeshRenderer.SetBlendShapeWeight (i, 0f);
            }
          }
        }
      }
    }
    
#if UNITY_EDITOR
    private void PrintAllBlendShapes (bool onlyNonZero = false) {
      foreach (SkinnedMeshRenderer skinnedMeshRenderer in _CustomizableMeshes) {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++) {
          float weight = skinnedMeshRenderer.GetBlendShapeWeight (i);
          if (!onlyNonZero || weight > 0f) {
            Debug.Log ($"{skinnedMeshRenderer.name} - {skinnedMeshRenderer.sharedMesh.GetBlendShapeName (i)}: {weight}");
          }
        }
      }
    }
    
    [UnityEditor.CustomEditor (typeof(CharacterPawnAvatar))]
    public class CharacterAvatarHandlerEditor : UnityEditor.Editor {
      public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        if (!(target is CharacterPawnAvatar avatarHandler)) {
          return;
        }
        using (new UnityEditor.EditorGUILayout.VerticalScope (GUI.skin.box)) {
          if (GUILayout.Button ("Get All Customizable Meshes")) {
            avatarHandler.GetAllCustomizableMeshes ();
          }
          if (GUILayout.Button ("Print All Non-Zero BlendShapes")) {
            avatarHandler.PrintAllBlendShapes (true);
          }
          if (GUILayout.Button ("Refresh Avatar")) {
            avatarHandler.RefreshAvatar ();
          }
          if (GUILayout.Button ("Reset Avatar")) {
            avatarHandler.ResetAvatar();
          }
        }
      }
    }
#endif

    
  }
}