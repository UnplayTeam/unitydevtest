using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BlendShaper : MonoBehaviour
{
	//~~~Definitions~~~
	public class BlendShapeData
	{
		public SkinnedMeshRenderer SMR { get; private set; }
		public int BlendShapeIndex { get; private set; }

		public BlendShapeData(SkinnedMeshRenderer a_smr, int a_blendShapeIndex)
		{
			SMR = a_smr;
			BlendShapeIndex = a_blendShapeIndex;
		}
	}

	//~~~Variables~~~
	//Serialized
	[SerializeField]
	private List<string> m_categoryIdentifiers;
	[SerializeField]
	private Transform m_meshRoot;
	[SerializeField]
	private Transform m_sliderGroupRoot;
	[SerializeField]
	private BlendShapeSliderGroup m_sliderGroupPrefab;

	//Non-Serialized
	private Dictionary<string, Dictionary<string, List<BlendShapeData>>> m_categoryList = new Dictionary<string, Dictionary<string, List<BlendShapeData>>>();
	private List<BlendShapeSliderGroup> m_sliderGroups = new List<BlendShapeSliderGroup>();

	//~~~Accessors~~~

	//~~~Unity Functions~~~

	void Start()
	{
		if (m_meshRoot == null)
		{
			Debug.LogError("Mesh Root must be defined in BlendShaper.");
			return;
		}

		if (m_sliderGroupRoot == null)
		{
			Debug.LogError("Slider Group Root must be defined in BlendShaper.");
			return;
		}

		if (m_sliderGroupPrefab == null)
		{
			Debug.LogError("Slider prefab must be defined in BlendShaper.");
			return;
		}

		GenerateBlendShapeList();
		GenerateSlidersGroups();
	}
	
	//~~~Runtime Functions~~~

	private void GenerateBlendShapeList()
	{
		m_categoryList.Clear();
		ParseBlendShapesFor(m_meshRoot);
	}

	//Scrubs the given path + children to pick out and blend shapes, goruping them into categories
	private void ParseBlendShapesFor(Transform a_transform)
	{
		if (a_transform == null)
			return;

		var smr = a_transform.GetComponent<SkinnedMeshRenderer>();
		if (smr != null && smr.sharedMesh != null)
		{
			for (int blendShapeIndex = 0; blendShapeIndex < smr.sharedMesh.blendShapeCount; blendShapeIndex++)
			{
				string name = smr.sharedMesh.GetBlendShapeName(blendShapeIndex);
				foreach (string categoryID in m_categoryIdentifiers)
				{
					int categoryIndex = name.IndexOf(categoryID);
					if (categoryIndex == -1)
						continue;

					string categoryName = categoryID.Substring(1, categoryID.Length - 2);
					if (!m_categoryList.TryGetValue(categoryName, out var categoryList))
					{
						m_categoryList.Add(categoryName, new Dictionary<string, List<BlendShapeData>>());
					}

					string subCategoryName =  name.Substring(categoryIndex + categoryID.Length).Replace('_', ' ');
					if (!m_categoryList[categoryName].TryGetValue(subCategoryName, out var subCategoryList))
					{
						m_categoryList[categoryName].Add(subCategoryName, new List<BlendShapeData>());
					}
					m_categoryList[categoryName][subCategoryName].Add(new BlendShapeData(smr, blendShapeIndex));
				}
			}
		}

		//See if any child nodes have blend shapes we need to be aware of
		foreach (Transform transform in a_transform)
		{
			ParseBlendShapesFor(transform);
		}
	}

	private void GenerateSlidersGroups()
	{
		foreach (var category in m_categoryList.Keys)
		{
			BlendShapeSliderGroup sliderGroup = Instantiate<BlendShapeSliderGroup>(m_sliderGroupPrefab, m_sliderGroupRoot);
			sliderGroup.Intialize(category, m_categoryList[category], OnGroupExpanded);
			m_sliderGroups.Add(sliderGroup);
		}
	}

	//~~~Callback Functions~~~

	public void OnGroupExpanded(BlendShapeSliderGroup m_expandedGroup)
	{
		if (m_expandedGroup.Expanded)
		{
			//It's already shown, so just hide it again, no need to alert the rest of the gorups to update
			m_expandedGroup.ExpandGroup(false);
		}
		else
		{
			foreach (var group in m_sliderGroups)
			{
				group.ExpandGroup(group == m_expandedGroup);
			}
		}

		LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
	}

	//~~~Editor Functions~~~
#if UNITY_EDITOR

#endif

}
