using UnityEngine;
using UnityEditor;

namespace JoshBowersDEV.Characters.Editor
{
    public class CharacterMeshDataEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private CharacterMeshData meshData;

        [MenuItem("unitydevtest/Character Mesh Data Editor")]
        public static void ShowWindow()
        {
            GetWindow<CharacterMeshDataEditorWindow>("Character Mesh Data Editor");
        }

        private void OnGUI()
        {
            meshData = EditorGUILayout.ObjectField("Character Mesh Data", meshData, typeof(CharacterMeshData), false) as CharacterMeshData;

            Undo.RecordObject(meshData, "Modified Character Mesh Data");

            if (meshData == null)
            {
                EditorGUILayout.LabelField("Please assign a Character Mesh Data.");
                return;
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Assign Values"))
            {
                meshData.InitializeListeners();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            EditorGUILayout.LabelField("Set Slider Values:");

            // Set Race
            meshData.SetRace((Race)EditorGUILayout.EnumPopup("Race", meshData.Race));

            //// Set First Race
            //meshData.SetFirstRace((Race)EditorGUILayout.EnumPopup("First Race", meshData.FirstRace));

            //// Set Second Race
            //meshData.SetSecondRace((Race)EditorGUILayout.EnumPopup("Second Race", meshData.SecondRace));

            //// Set Hybrid Blend
            //meshData.SetHybridBlend(EditorGUILayout.Slider("Hybrid Blend", meshData.HybridBlend, -100, 100));

            // Set FemMasc
            meshData.SetFemMasc(EditorGUILayout.Slider("FemMasc", meshData.FemMasc, -100, 100));

            // Head Properties
            // Head Properties
            meshData.SetFacialEarScale(EditorGUILayout.Slider("Facial Ear Scale", meshData.FacialEarScale, 0, 100));
            meshData.SetFacialEarLobeSize(EditorGUILayout.Slider("Facial Ear Lobe Size", meshData.FacialEarLobeSize, 0, 100));
            meshData.SetFacialEarsOut(EditorGUILayout.Slider("Facial Ears Out", meshData.FacialEarsOut, 0, 100));
            meshData.SetFacialBrowWide(EditorGUILayout.Slider("Facial Brow Wide", meshData.FacialBrowWide, 0, 100));
            meshData.SetFacialBrowForward(EditorGUILayout.Slider("Facial Brow Forward", meshData.FacialBrowForward, 0, 100));
            meshData.SetFacialCheekbonesInOut(EditorGUILayout.Slider("Facial Cheekbones In-Out", meshData.FacialCheekbonesInOut, -100, 100));
            meshData.SetFacialCheeksGauntFull(EditorGUILayout.Slider("Facial Cheeks Gaunt-Full", meshData.FacialCheeksGauntFull, -100, 100));
            meshData.SetFacialChinTipLength(EditorGUILayout.Slider("Facial Chin Tip Length", meshData.FacialChinTipLength, 0, 100));
            meshData.SetFacialChinTipWidth(EditorGUILayout.Slider("Facial Chin Tip Width", meshData.FacialChinTipWidth, 0, 100));
            meshData.SetFacialJawDown(EditorGUILayout.Slider("Facial Jaw Down", meshData.FacialJawDown, 0, 100));
            meshData.SetFacialJawWide(EditorGUILayout.Slider("Facial Jaw Wide", meshData.FacialJawWide, 0, 100));
            meshData.SetFacialLipTopThinFull(EditorGUILayout.Slider("Facial Lip Top Thin-Full", meshData.FacialLipTopThinFull, -100, 100));
            meshData.SetFacialLipBotThinFull(EditorGUILayout.Slider("Facial Lip Bot Thin-Full", meshData.FacialLipBotThinFull, -100, 100));
            meshData.SetFacialMouthCrease(EditorGUILayout.Slider("Facial Mouth Crease", meshData.FacialMouthCrease, 0, 100));
            meshData.SetFacialMouthWidth(EditorGUILayout.Slider("Facial Mouth Width", meshData.FacialMouthWidth, 0, 100));
            meshData.SetFacialMouthOut(EditorGUILayout.Slider("Facial Mouth Out", meshData.FacialMouthOut, 0, 100));
            meshData.SetFacialNoseAngle(EditorGUILayout.Slider("Facial Nose Angle", meshData.FacialNoseAngle, 0, 100));
            meshData.SetFacialNoseBulb(EditorGUILayout.Slider("Facial Nose Bulb", meshData.FacialNoseBulb, 0, 100));
            meshData.SetFacialNoseBridgeDepth(EditorGUILayout.Slider("Facial Nose Bridge Depth", meshData.FacialNoseBridgeDepth, 0, 100));
            meshData.SetFacialNoseBridgeWidth(EditorGUILayout.Slider("Facial Nose Bridge Width", meshData.FacialNoseBridgeWidth, 0, 100));
            meshData.SetFacialNoseLength(EditorGUILayout.Slider("Facial Nose Length", meshData.FacialNoseLength, 0, 100));
            meshData.SetFacialNoseTipWidthInOut(EditorGUILayout.Slider("Facial Nose Tip Width In-Out", meshData.FacialNoseTipWidthInOut, -100, 100));

            // Upper Body Properties
            meshData.SetBodyMuscularMidHeavy(EditorGUILayout.Slider("Body Muscular Mid-Heavy", meshData.BodyMuscularMidHeavy, -100, 100));
            meshData.SetBodyWeightThinHeavy(EditorGUILayout.Slider("Body Weight Thin-Heavy", meshData.BodyWeightThinHeavy, -100, 100));
            meshData.SetIsoBack(EditorGUILayout.Slider("Iso Back", meshData.IsoBack, 0, 100));
            meshData.SetIsoBelly(EditorGUILayout.Slider("Iso Belly", meshData.IsoBelly, 0, 100));
            meshData.SetIsoBellyHeight(EditorGUILayout.Slider("Iso Belly Height", meshData.IsoBellyHeight, 0, 100));
            meshData.SetIsoBiceps(EditorGUILayout.Slider("Iso Biceps", meshData.IsoBiceps, 0, 100));
            meshData.SetIsoBustSmallLarge(EditorGUILayout.Slider("Iso Bust Small-Large", meshData.IsoBustSmallLarge, -100, 100));
            meshData.SetIsoButt(EditorGUILayout.Slider("Iso Butt", meshData.IsoButt, 0, 100));
            meshData.SetIsoDeltoids(EditorGUILayout.Slider("Iso Deltoids", meshData.IsoDeltoids, 0, 100));
            meshData.SetIsoForearms(EditorGUILayout.Slider("Iso Forearms", meshData.IsoForearms, 0, 100));
            meshData.SetIsoNeck(EditorGUILayout.Slider("Iso Neck", meshData.IsoNeck, 0, 100));
            meshData.SetIsoPectorals(EditorGUILayout.Slider("Iso Pectorals", meshData.IsoPectorals, 0, 100));
            meshData.SetIsoRibcage(EditorGUILayout.Slider("Iso Ribcage", meshData.IsoRibcage, 0, 100));
            meshData.SetIsoTrunk(EditorGUILayout.Slider("Iso Trunk", meshData.IsoTrunk, 0, 100));
            meshData.SetIsoTrapezius(EditorGUILayout.Slider("Iso Trapezius", meshData.IsoTrapezius, 0, 100));
            meshData.SetIsoTriceps(EditorGUILayout.Slider("Iso Triceps", meshData.IsoTriceps, 0, 100));

            // Lower Body Properties
            meshData.SetLegUpperIsoCalves(EditorGUILayout.Slider("Leg Upper ISO Calves", meshData.LegUpperIsoCalves, 0, 100));
            meshData.SetLegUpperIsoThighs(EditorGUILayout.Slider("Leg Upper ISO Thighs", meshData.LegUpperIsoThighs, 0, 100));
            meshData.SetWaistIsoBulge(EditorGUILayout.Slider("Waist ISO Bulge", meshData.WaistIsoBulge, 0, 100));

            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            if (GUILayout.Button("Apply"))
            {
                // Save changes to the ScriptableObject asset
                EditorUtility.SetDirty(meshData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}