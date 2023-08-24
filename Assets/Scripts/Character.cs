using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public AppManager AppManager;
    // this is my fake entrypoint, ideally, everything is spawned from somewhere and would have this passed in on Init()

    private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    private List<BlendShape> _blendShapes;
    private Dictionary<CharacteristicType, List<BlendShape>> _blendShapesSortedByCharacteristicType;

    void Start()
    {
        _blendShapes = new List<BlendShape>();
        _blendShapesSortedByCharacteristicType = new Dictionary<CharacteristicType, List<BlendShape>>();

        GetSkinnedMeshRenderers();

        GetBlendShapesFromSkinnedMeshRenderers();

        SortBlendShapesByCharacteristics();

        SetInitialBlendWeights();
    }

    void OnMouseDown()
    {
        if (AppManager?.UIManager == null)
        {
            return;
        }
        AppManager.UIManager.OpenCharacterCustomizationDialog(this);
    }

    private void GetSkinnedMeshRenderers()
    {
        _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void GetBlendShapesFromSkinnedMeshRenderers()
    {
        if (_skinnedMeshRenderers == null)
        {
            return;
        }
        foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
        {
            Mesh sharedMesh = skinnedMeshRenderer.sharedMesh;
            for (int index = 0; index < sharedMesh.blendShapeCount; index++)
            {
                string blendShapeName = sharedMesh.GetBlendShapeName(index);
                BlendShape blendShape = new BlendShape(
                    blendShapeName, skinnedMeshRenderer.GetBlendShapeWeight(index), index, skinnedMeshRenderer);
                _blendShapes.Add(blendShape);
            }
        }
    }

    private void SortBlendShapesByCharacteristics()
    {
        foreach (BlendShape blendShape in _blendShapes)
        {
            foreach (CharacteristicType characteristic in GetCharacteristicTypesByBlendShapeName(blendShape.Name))
            {
                if (!_blendShapesSortedByCharacteristicType.ContainsKey(characteristic))
                {
                    _blendShapesSortedByCharacteristicType[characteristic] = new List<BlendShape>();
                }
                _blendShapesSortedByCharacteristicType[characteristic].Add(blendShape);
            }
        }
    }

    private List<CharacteristicType> GetCharacteristicTypesByBlendShapeName(string blendShapeName)
    {
        List<CharacteristicType> characteristics = new List<CharacteristicType>();

        foreach (Characteristic characteristic in AppManager.CharacterManager.GetAllCharacteristics())
        {
            if (characteristic.IsPositivelyOrNegativelyRelatedToCharacteristic(blendShapeName))
            {
                characteristics.Add(characteristic.Type);
            }
        }
        return characteristics;
    }

    private void SetInitialBlendWeights()
    {
        foreach (var kvp in _blendShapesSortedByCharacteristicType)
        {
            SetWeightForBlendShapesPerCharacteristic(kvp.Key, 0);
        }
    }

    public void SetWeightForBlendShapesPerCharacteristic(CharacteristicType characteristicType, float weight)
    {
        if (!_blendShapesSortedByCharacteristicType.ContainsKey(characteristicType))
        {
            return;
        }

        List<BlendShape> characteristicsBlendShapes = _blendShapesSortedByCharacteristicType[characteristicType];

        foreach (var blendShape in characteristicsBlendShapes)
        {
            Characteristic characteristic = AppManager.CharacterManager.GetCharacteristicByType(characteristicType);

            float adjustedWeight = characteristic.CalculateWeight(weight, blendShape.Name);

            blendShape.SetWeight(adjustedWeight);
        }
    }
}
