using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.Character.Avatar {
  [CreateAssetMenu(fileName = "Data", menuName = "RPG/Avatar/AvatarBaseMeshData", order = 1)]
  public class AvatarBaseMeshData : ScriptableObject {
    [Serializable]
    public class BlendShapeData {
      public string BlendShapeName;
      public string MeshName;
      public int Index;
      public float MinValue = MeshUtils.BlendShapeWeightMin;
      public float MaxValue = MeshUtils.BlendShapeWeightMax;
    }
    
    [Serializable]
    public class BlendShapeGroup {
      public string GroupName;
      public string Category;
      public List<BlendShapeData> BlendShapes = new ();
    }
    
    [SerializeField] private string _Key;
    [SerializeField] private string _BaseMeshAssetGuid;
    [SerializeField, HideInInspector] private List<BlendShapeData> _AllMeshBlendShapes;
    [SerializeField] private List<BlendShapeGroup> _BlendShapeGroups;
    
    public string Key => _Key;
    public List<BlendShapeGroup> BlendShapeGroups => _BlendShapeGroups;
    
    public bool TryGetBlendShapeGroup (string groupName, out BlendShapeGroup blendShapeGroup) {
      blendShapeGroup = _BlendShapeGroups.FirstOrDefault (bsg => bsg.GroupName == groupName);
      return blendShapeGroup != null;
    }
    
    
#if UNITY_EDITOR
    private const string MenuItemPath = "Assets/Create AvatarBaseMeshData Asset";
    private const string ProgressTitle = "Extracting Meshes";
    private static readonly string[] ValidExtensions = { ".fbx" };

    [MenuItem(MenuItemPath, true, -1)]
    private static bool ValidateCreateAvatarBaseMeshData () {
      bool isValid = false;
      string firstGuid = Selection.assetGUIDs[0];
      string assetPath = AssetDatabase.GUIDToAssetPath(firstGuid);
      foreach (string ext in ValidExtensions) {
        isValid = assetPath.EndsWith (ext);
        if (isValid) {
          break;
        }
      }
      return isValid;
    }
    
    [MenuItem(MenuItemPath, false, -1)]
    private static void CreateAvatarBaseMeshData () {
      string selectedGuid = Selection.assetGUIDs[0] ?? throw new ArgumentNullException ("Selection.assetGUIDs");
      if (Selection.assetGUIDs.Length > 1) {
        Debug.LogWarning ("Multiple assets selected. Only the first asset will be processed.");
      }
      string assetPath = AssetDatabase.GUIDToAssetPath(selectedGuid);
      List<BlendShapeData> blendShapeDataCollection = ExtractBlendShapeData (assetPath);
      AvatarBaseMeshData dataAsset = CreateInstance<AvatarBaseMeshData>();
      dataAsset._BaseMeshAssetGuid = selectedGuid;
      dataAsset._Key = selectedGuid;
      dataAsset._AllMeshBlendShapes = blendShapeDataCollection;
      dataAsset._BlendShapeGroups = new List<BlendShapeGroup> ();
      string fileName = System.IO.Path.GetFileNameWithoutExtension (assetPath);
      string directoryPath = System.IO.Path.GetDirectoryName(assetPath);
      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(System.IO.Path.Combine (directoryPath, $"{fileName}.BaseMeshData.asset"));
      AssetDatabase.CreateAsset(dataAsset, assetPathAndName);
      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
      EditorUtility.FocusProjectWindow();
      Selection.activeObject = dataAsset;
    }

    private static List<BlendShapeData> ExtractBlendShapeData (string assetPath) {
      //Create Meshes
      List<BlendShapeData> blendShapeDataList = new();
      Object[] objects = AssetDatabase.LoadAllAssetsAtPath(assetPath);
      EditorUtility.DisplayProgressBar(ProgressTitle, "", 0);
      for (int objIndex = 0; objIndex < objects.Length; objIndex++) {
        Object obj = objects[objIndex];
        EditorUtility.DisplayProgressBar (ProgressTitle, $"{assetPath} : {obj.name}",
          (float)objIndex / (objects.Length - 1));
        if (obj is Mesh mesh) {
          Debug.Log ("Found mesh: " + obj.name);
          int blendShapeCount = mesh.blendShapeCount;
          Debug.Log ($"BlendShape count: {blendShapeCount}");
          for (int shapeIndex = 0; shapeIndex < blendShapeCount; shapeIndex++) {
            BlendShapeData data = new ();
            string blendShapeName = mesh.GetBlendShapeName (shapeIndex);
            data.MeshName = mesh.name;
            data.BlendShapeName = blendShapeName;
            data.Index = shapeIndex;
            Debug.Log ($"BlendShape name at index {shapeIndex}: {blendShapeName}");
            blendShapeDataList.Add (data);
          }
        }
      }
      EditorUtility.ClearProgressBar();
      return blendShapeDataList;
    }

    [CustomPropertyDrawer (typeof(BlendShapeData))]
    public class CustomDataPropertyDrawer : PropertyDrawer {
      public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        SerializedProperty blendShapeNameProperty = property.FindPropertyRelative("BlendShapeName");
        SerializedProperty minValueProperty = property.FindPropertyRelative("MinValue");
        SerializedProperty maxValueProperty = property.FindPropertyRelative("MaxValue");
        
        // Name Field
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), blendShapeNameProperty, new GUIContent("Blend Shape Name"));
        
        // Min-Max slider
        float minValue = minValueProperty.floatValue;
        float maxValue = maxValueProperty.floatValue;
        EditorGUI.MinMaxSlider(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), ref minValue, ref maxValue, MeshUtils.BlendShapeWeightMin, MeshUtils.BlendShapeWeightMax);
        minValue = EditorGUI.FloatField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width / 2 - 5, EditorGUIUtility.singleLineHeight), "Min", minValue);
        maxValue = EditorGUI.FloatField(new Rect(position.x + position.width / 2 + 5, position.y + EditorGUIUtility.singleLineHeight * 2, position.width / 2 - 5, EditorGUIUtility.singleLineHeight), "Max", maxValue);
        minValueProperty.floatValue = minValue;
        maxValueProperty.floatValue = maxValue;

        EditorGUI.EndProperty();
      }
      
      public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
      {
        // Define a custom height for our property
        return EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2;
      }
    }
    
    [CustomEditor (typeof(AvatarBaseMeshData))]
    public class AvatarBaseMeshDataEditor : Editor {
      private SerializedProperty _SerializedGroupList;
      private AvatarBaseMeshData _BaseMeshData;
      private bool _ShowFullList;
      private int _ExpandedGroupIndex = -1;
      // Shared across all groups, it's fine for now but maybe fix before submission
      private string _SearchQuery = string.Empty;

      private void OnEnable () {
        _BaseMeshData = target as AvatarBaseMeshData;
        _SerializedGroupList = serializedObject.FindProperty ("_BlendShapeGroups");
      }
      
      public override void OnInspectorGUI () {
        //DrawDefaultInspector ();
        if (_BaseMeshData == null) {
          return;
        }
        serializedObject.Update();
        EditorGUILayout.LabelField ($"GUID {_BaseMeshData._BaseMeshAssetGuid}", EditorStyles.boldLabel);
        _BaseMeshData._Key = EditorGUILayout.TextField ("Key", _BaseMeshData._Key);
        if (GUILayout.Button ("Refresh From Mesh Asset")) {
          string assetPath = AssetDatabase.GUIDToAssetPath (_BaseMeshData._BaseMeshAssetGuid);
          _BaseMeshData._AllMeshBlendShapes = ExtractBlendShapeData (assetPath);
          _BaseMeshData._BlendShapeGroups.Clear ();
        }

        using (new EditorGUILayout.VerticalScope (GUI.skin.box)) {
          if (GUILayout.Button("Add New Blend Shape Group")) {
            _SerializedGroupList.arraySize += 1;
          }
          for (int i = 0; i < _SerializedGroupList.arraySize; i++) {
            SerializedProperty listItem = _SerializedGroupList.GetArrayElementAtIndex (i);
            EditorGUILayout.PropertyField (listItem);
            if (listItem.isExpanded) {
              if (_ExpandedGroupIndex != i) {
                if (_ExpandedGroupIndex != -1) {
                  SerializedProperty prevExpanded = _SerializedGroupList.GetArrayElementAtIndex (i);
                  prevExpanded.isExpanded = false;
                }
                _ExpandedGroupIndex = i;
              }
            } else if (_ExpandedGroupIndex == i) {
              _ExpandedGroupIndex = -1;
            }
          } 
        }

        int matchedCount = 0;
        using (new EditorGUILayout.VerticalScope (GUI.skin.box)) {
          _SearchQuery = EditorGUILayout.TextField ("Search", _SearchQuery);
          if (_SearchQuery.Length > 0) {
            BlendShapeData[] matches = _BaseMeshData._AllMeshBlendShapes
              .Where (FilterBlendShapes).OrderBy(SortBlendShapes).ToArray ();
            matchedCount = matches.Length;
            EditorGUILayout.LabelField ($"Matches [{matchedCount}]");
            foreach (BlendShapeData data in matches) {
              GUI.enabled = _ExpandedGroupIndex != -1;
              if (GUILayout.Button (data.BlendShapeName)) {
                SerializedProperty expanded = _SerializedGroupList.GetArrayElementAtIndex (_ExpandedGroupIndex);
                SerializedProperty blendShapesList = expanded.FindPropertyRelative ("BlendShapes");
                int newIndex = blendShapesList.arraySize;
                blendShapesList.InsertArrayElementAtIndex(newIndex);
                SerializedProperty newBlendShape = blendShapesList.GetArrayElementAtIndex(newIndex);
                newBlendShape.FindPropertyRelative ("BlendShapeName").stringValue = data.BlendShapeName;
                newBlendShape.FindPropertyRelative ("MeshName").stringValue = data.MeshName;
                newBlendShape.FindPropertyRelative ("Index").intValue = data.Index;
                newBlendShape.FindPropertyRelative ("MinValue").floatValue = MeshUtils.BlendShapeWeightMin;
                newBlendShape.FindPropertyRelative ("MaxValue").floatValue = MeshUtils.BlendShapeWeightMax;
              }
              GUI.enabled = true;
            }
          } else {
            EditorGUILayout.LabelField ("Search the available blend shapes by name");
          }
          string prefix = _SearchQuery.Length == 0 ? "All" : "Remaining";
          int remainingCount = _BaseMeshData._AllMeshBlendShapes.Count - matchedCount;
          _ShowFullList = EditorGUILayout.Foldout(_ShowFullList, $"{prefix} Mesh Blend Shapes [{remainingCount}]");
          if (_ShowFullList) {
            List<BlendShapeData> unmatchedList = _BaseMeshData._AllMeshBlendShapes.OrderBy (SortBlendShapes).ToList ();
            foreach (BlendShapeData data in unmatchedList) {
              EditorGUILayout.LabelField ($"{data.BlendShapeName}");
            }
          }
        }
        serializedObject.ApplyModifiedProperties();
      }

      private bool FilterBlendShapes (BlendShapeData data) {
        return string.IsNullOrEmpty (_SearchQuery) || data.BlendShapeName.Contains (_SearchQuery, StringComparison.OrdinalIgnoreCase);
      }

      private string SortBlendShapes (BlendShapeData data) {
        return data.BlendShapeName;
      }
    }
#endif
  }
}