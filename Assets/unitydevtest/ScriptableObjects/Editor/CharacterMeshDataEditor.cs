using UnityEditor;
using JoshBowersDEV.Characters;

[CustomEditor(typeof(CharacterMeshData))]
public class CharacterMeshDataEditor : Editor
{
    private bool showGenderProperties = false;
    private bool showHeadProperties = false;
    private bool showUpperBodyProperties = false;
    private bool showLowerBodyProperties = false;

    public override void OnInspectorGUI()
    {
        CharacterMeshData characterMeshData = (CharacterMeshData)target;

        characterMeshData.IsHybrid = EditorGUILayout.Toggle("Is a Hybrid?", characterMeshData.IsHybrid);

        // Check if Race is Hybrid, and enable/disable the FirstRace and SecondRace fields accordingly
        if (characterMeshData.IsHybrid)
        {
            EditorGUI.BeginChangeCheck();
            characterMeshData.FirstRace = (Race)EditorGUILayout.EnumPopup("First Race", characterMeshData.FirstRace);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(characterMeshData);
            }

            EditorGUI.BeginChangeCheck();
            characterMeshData.SecondRace = (Race)EditorGUILayout.EnumPopup("Second Race", characterMeshData.SecondRace);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(characterMeshData);
            }

            characterMeshData.HybridBlend = EditorGUILayout.Slider("Hybrid Blend", characterMeshData.HybridBlend, -1f, 1f);
        }
        else
        {
            // Display the default Race field
            characterMeshData.Race = (Race)EditorGUILayout.EnumPopup("Race", characterMeshData.Race);

            EditorGUILayout.HelpBox("First Race and Second Race are only valid if Race is Hybrid.", MessageType.Info);
        }

        ShowOtherProperties(characterMeshData);
        EditorUtility.SetDirty(characterMeshData);
        // Make sure to update the serialized object
        serializedObject.ApplyModifiedProperties();
    }

    private void ShowOtherProperties(CharacterMeshData characterMeshData)
    {
        showGenderProperties = EditorGUILayout.Foldout(showGenderProperties, "Gender Properties", true);
        if (showGenderProperties)
        {
            // Display the serialized float properties with their necessary ranges
            EditorGUILayout.BeginVertical();
            characterMeshData.FemaleMale = EditorGUILayout.Slider("Female Slider", characterMeshData.FemaleMale, 0, 1f);
            characterMeshData.Male = EditorGUILayout.Slider("Male Slider", characterMeshData.Male, 0, 1f);
            EditorGUILayout.EndVertical();
        }

        showHeadProperties = EditorGUILayout.Foldout(showHeadProperties, "Head Properties", true);
        if (showHeadProperties)
        {
            EditorGUILayout.BeginVertical();
            characterMeshData.FacialEarScale = EditorGUILayout.Slider("Facial Ear Scale", characterMeshData.FacialEarScale, 0f, 1f);
            characterMeshData.FacialEarLobeSize = EditorGUILayout.Slider("Facial Ear Lobe Size", characterMeshData.FacialEarLobeSize, 0f, 1f);
            characterMeshData.FacialEarsOut = EditorGUILayout.Slider("Facial Ears Out", characterMeshData.FacialEarsOut, 0f, 1f);
            characterMeshData.FacialBrowWide = EditorGUILayout.Slider("Facial Brow Wide", characterMeshData.FacialBrowWide, 0f, 1f);
            characterMeshData.FacialBrowForward = EditorGUILayout.Slider("Facial Brow Forward", characterMeshData.FacialBrowForward, 0f, 1f);
            characterMeshData.FacialCheekbonesInOut = EditorGUILayout.Slider("Facial Cheekbones In/Out", characterMeshData.FacialCheekbonesInOut, 0f, 1f);
            characterMeshData.FacialCheeksGauntFull = EditorGUILayout.Slider("Facial Cheeks Gaunt/Full", characterMeshData.FacialCheeksGauntFull, 0f, 1f);
            characterMeshData.FacialChinTipLength = EditorGUILayout.Slider("Facial Chin Tip Length", characterMeshData.FacialChinTipLength, 0f, 1f);
            characterMeshData.FacialChinTipWidth = EditorGUILayout.Slider("Facial Chin Tip Width", characterMeshData.FacialChinTipWidth, 0f, 1f);
            characterMeshData.FacialJawDown = EditorGUILayout.Slider("Facial Jaw Down", characterMeshData.FacialJawDown, 0f, 1f);
            characterMeshData.FacialJawWide = EditorGUILayout.Slider("Facial Jaw Wide", characterMeshData.FacialJawWide, 0f, 1f);
            characterMeshData.FacialLipTopThinFull = EditorGUILayout.Slider("Facial Lip Top Thin/Full", characterMeshData.FacialLipTopThinFull, 0f, 1f);
            characterMeshData.FacialLipBotThinFull = EditorGUILayout.Slider("Facial Lip Bottom Thin/Full", characterMeshData.FacialLipBotThinFull, 0f, 1f);
            characterMeshData.FacialMouthCrease = EditorGUILayout.Slider("Facial Mouth Crease", characterMeshData.FacialMouthCrease, 0f, 1f);
            characterMeshData.FacialMouthWidth = EditorGUILayout.Slider("Facial Mouth Width", characterMeshData.FacialMouthWidth, 0f, 1f);
            characterMeshData.FacialMouthOut = EditorGUILayout.Slider("Facial Mouth Out", characterMeshData.FacialMouthOut, 0f, 1f);
            characterMeshData.FacialNoseAngle = EditorGUILayout.Slider("Facial Nose Angle", characterMeshData.FacialNoseAngle, 0f, 1f);
            characterMeshData.FacialNoseBulb = EditorGUILayout.Slider("Facial Nose Bulb", characterMeshData.FacialNoseBulb, 0f, 1f);
            characterMeshData.FacialNoseBridgeDepth = EditorGUILayout.Slider("Facial Nose Bridge Depth", characterMeshData.FacialNoseBridgeDepth, 0f, 1f);
            characterMeshData.FacialNoseBridgeWidth = EditorGUILayout.Slider("Facial Nose Bridge Width", characterMeshData.FacialNoseBridgeWidth, 0f, 1f);
            characterMeshData.FacialNoseLength = EditorGUILayout.Slider("Facial Nose Length", characterMeshData.FacialNoseLength, 0f, 1f);
            characterMeshData.FacialNoseTipWidthInOut = EditorGUILayout.Slider("Facial Nose Tip Width In/Out", characterMeshData.FacialNoseTipWidthInOut, 0f, 1f);
            EditorGUILayout.EndVertical();
        }

        showUpperBodyProperties = EditorGUILayout.Foldout(showUpperBodyProperties, "Body Properties", true);
        if (showUpperBodyProperties)
        {
            EditorGUILayout.BeginVertical();
            characterMeshData.BodyMuscularMidHeavy = EditorGUILayout.Slider("Body Muscular Mid/Heavy", characterMeshData.BodyMuscularMidHeavy, 0f, 1f);
            characterMeshData.BodyWeightThinHeavy = EditorGUILayout.Slider("Body Weight Thin/Heavy", characterMeshData.BodyWeightThinHeavy, 0f, 1f);
            characterMeshData.IsoBack = EditorGUILayout.Slider("Iso Back", characterMeshData.IsoBack, 0f, 1f);
            characterMeshData.IsoBelly = EditorGUILayout.Slider("Iso Belly", characterMeshData.IsoBelly, 0f, 1f);
            characterMeshData.IsoBellyHeight = EditorGUILayout.Slider("Iso Belly Height", characterMeshData.IsoBellyHeight, 0f, 1f);
            characterMeshData.IsoBiceps = EditorGUILayout.Slider("Iso Biceps", characterMeshData.IsoBiceps, 0f, 1f);
            characterMeshData.IsoBustSmallLarge = EditorGUILayout.Slider("Iso Bust Small/Large", characterMeshData.IsoBustSmallLarge, 0f, 1f);
            characterMeshData.IsoButt = EditorGUILayout.Slider("Iso Butt", characterMeshData.IsoButt, 0f, 1f);
            characterMeshData.IsoDeltoids = EditorGUILayout.Slider("Iso Deltoids", characterMeshData.IsoDeltoids, 0f, 1f);
            characterMeshData.IsoForearms = EditorGUILayout.Slider("Iso Forearms", characterMeshData.IsoForearms, 0f, 1f);
            characterMeshData.IsoPectorals = EditorGUILayout.Slider("Iso Pectorals", characterMeshData.IsoPectorals, 0f, 1f);
            characterMeshData.IsoRibcage = EditorGUILayout.Slider("Iso Ribcage", characterMeshData.IsoRibcage, 0f, 1f);
            characterMeshData.IsoTrunk = EditorGUILayout.Slider("Iso Trunk", characterMeshData.IsoTrunk, 0f, 1f);
            characterMeshData.IsoTrapezius = EditorGUILayout.Slider("Iso Trapezius", characterMeshData.IsoTrapezius, 0f, 1f);
            EditorGUILayout.EndVertical();
        }

        showLowerBodyProperties = EditorGUILayout.Foldout(showLowerBodyProperties, "Leg Properties", true);
        if (showLowerBodyProperties)
        {
            EditorGUILayout.BeginVertical();
            characterMeshData.LegUpperIsoCalves = EditorGUILayout.Slider("Leg Upper Iso Calves", characterMeshData.LegUpperIsoCalves, 0f, 1f);
            characterMeshData.LegUpperIsoThighs = EditorGUILayout.Slider("Leg Upper Iso Thighs", characterMeshData.LegUpperIsoThighs, 0f, 1f);
            characterMeshData.WaistIsoBulge = EditorGUILayout.Slider("Waist Iso Bulge", characterMeshData.WaistIsoBulge, 0f, 1f);
            EditorGUILayout.EndVertical();
        }
    }
}