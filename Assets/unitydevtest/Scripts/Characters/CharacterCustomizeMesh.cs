using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    /// <summary>
    /// Bindable mono for exposing current Mesh information.
    /// </summary>
    [Binding]
    public class CharacterCustomizeMesh : BindableBehaviourBase
    {
        #region Properties

        [SerializeField]
        private CharacterMeshData _characterMeshData;

        [Binding]
        public CharacterMeshData CharacterMeshData
        {
            get => _characterMeshData;
            set => _characterMeshData = value;
        }

        #endregion Properties

        #region Racial and Gender Properties

        [SerializeField]
        private bool _isHybrid;

        [Binding]
        public bool IsHybrid
        {
            get => _isHybrid;
            set => SetProperty(ref _isHybrid, value);
        }

        [Binding]
        public Race Race
        {
            get => CharacterMeshData.Race;
        }

        [Binding]
        public Race FirstRace
        {
            get => CharacterMeshData.FirstRace;
        }

        [Binding]
        public Race SecondRace
        {
            get => CharacterMeshData.SecondRace;
        }

        [Binding]
        public float HybridBlend
        {
            get => CharacterMeshData.HybridBlend;
        }

        // Do not serialize the below race sliders, these will be determined elsewhere.
        [Binding]
        public float HumanRace
        {
            get => CharacterMeshData.HumanRace;
        }

        [Binding]
        public float ElfRace
        {
            get => CharacterMeshData.ElfRace;
        }

        [Binding]
        public float OrcRace
        {
            get => CharacterMeshData.OrcRace;
        }

        public float Female
        {
            get => CharacterMeshData.Female;
        }

        public float Male
        {
            get => CharacterMeshData.Male;
        }

        [Binding]
        public float FemaleHuman
        {
            get => CharacterMeshData.FemaleHuman;
        }

        [Binding]
        public float MaleHuman
        {
            get => CharacterMeshData.MaleHuman;
        }

        [Binding]
        public float FemaleElf
        {
            get => CharacterMeshData.FemaleElf;
        }

        [Binding]
        public float MaleElf
        {
            get => Male * ElfRace;
        }

        [Binding]
        public float FemaleOrc
        {
            get => Female * OrcRace;
        }

        [Binding]
        public float MaleOrc
        {
            get => Male * OrcRace;
        }

        #endregion Racial and Gender Properties

        #region Head Properties

        [Binding]
        public float EarScale
        {
            get => CharacterMeshData.EarScale;
        }

        [Binding]
        public float EarLobeSize
        {
            get => CharacterMeshData.EarLobeSize;
        }

        [Binding]
        public float EarsOut
        {
            get => CharacterMeshData.EarsOut;
        }

        [Binding]
        public float BrowWide
        {
            get => CharacterMeshData.BrowWide;
        }

        [Binding]
        public float BrowForward
        {
            get => CharacterMeshData.BrowForward;
        }

        [Binding]
        public float FacialCheekbonesInOut
        {
            get => CharacterMeshData.FacialCheekbonesInOut;
        }

        [Binding]
        public float FacialCheeksGauntFull
        {
            get => CharacterMeshData.FacialCheeksGauntFull;
        }

        [Binding]
        public float FacialChinTipLength
        {
            get => CharacterMeshData.FacialChinTipLength;
        }

        [Binding]
        public float FacialChinTipWidth
        {
            get => CharacterMeshData.FacialChinTipWidth;
        }

        [Binding]
        public float FacialJawDown
        {
            get => CharacterMeshData.FacialJawDown;
        }

        [Binding]
        public float FacialJawWide
        {
            get => CharacterMeshData.FacialJawWide;
        }

        [Binding]
        public float FacialLipTopThinFull
        {
            get => CharacterMeshData.FacialLipTopThinFull;
        }

        [Binding]
        public float FacialLipBotThinFull
        {
            get => CharacterMeshData.FacialLipBotThinFull;
        }

        [Binding]
        public float FacialMouthCrease
        {
            get => CharacterMeshData.FacialMouthCrease;
        }

        [Binding]
        public float FacialMouthWidth
        {
            get => CharacterMeshData.FacialMouthWidth;
        }

        [Binding]
        public float FacialMouthOut
        {
            get => CharacterMeshData.FacialMouthOut;
        }

        [Binding]
        public float FacialNoseAngle
        {
            get => CharacterMeshData.FacialNoseAngle;
        }

        [Binding]
        public float FacialNoseBulb
        {
            get => CharacterMeshData.FacialNoseBulb;
        }

        [Binding]
        public float FacialNoseBridgeDepth
        {
            get => CharacterMeshData.FacialNoseBridgeDepth;
        }

        [Binding]
        public float facialNoseBridgeWidth
        {
            get => CharacterMeshData.FacialNoseBridgeWidth;
        }

        [Binding]
        public float FacialNoseLength
        {
            get => CharacterMeshData.FacialNoseLength;
        }

        [Binding]
        public float FacialNoseTipWidthInOut
        {
            get => CharacterMeshData.FacialNoseTipWidthInOut;
        }

        #endregion Head Properties

        #region Upper Body Properties

        [Binding]
        public float BodyMuscularMidHeavy
        {
            get => CharacterMeshData.BodyMuscularMidHeavy;
        }

        [Binding]
        public float BodyWeightThinHeavy
        {
            get => CharacterMeshData.BodyWeightThinHeavy;
        }

        [Binding]
        public float IsoBack
        {
            get => CharacterMeshData.IsoBack;
        }

        [Binding]
        public float IsoBelly
        {
            get => CharacterMeshData.IsoBelly;
        }

        [Binding]
        public float IsoBellyHeight
        {
            get => CharacterMeshData.IsoBellyHeight;
        }

        [Binding]
        public float IsoBiceps
        {
            get => CharacterMeshData.IsoBiceps;
        }

        [Binding]
        public float IsoBustSmallLarge
        {
            get => CharacterMeshData.IsoBustSmallLarge;
        }

        [Binding]
        public float IsoButt
        {
            get => CharacterMeshData.IsoButt;
        }

        [Binding]
        public float IsoDeltoids
        {
            get => CharacterMeshData.IsoDeltoids;
        }

        [Binding]
        public float IsoForearms
        {
            get => CharacterMeshData.IsoForearms;
        }

        [Binding]
        public float IsoPectorals
        {
            get => CharacterMeshData.IsoPectorals;
        }

        [Binding]
        public float IsoRibcage
        {
            get => CharacterMeshData.IsoRibcage;
        }

        [Binding]
        public float IsoTrunk
        {
            get => CharacterMeshData.IsoTrunk;
        }

        [Binding]
        public float IsoTrapezius
        {
            get => CharacterMeshData.IsoTrapezius;
        }

        #endregion Upper Body Properties

        #region Lower Body Properties

        [Binding]
        public float LegUpperIsoCalves
        {
            get => CharacterMeshData.LegUpperIsoCalves;
        }

        [Binding]
        public float LegUpperIsoThighs
        {
            get => CharacterMeshData.LegUpperIsoThighs;
        }

        [Binding]
        public float WaistIsoBulge
        {
            get => CharacterMeshData.WaistIsoBulge;
        }

        #endregion Lower Body Properties
    }
}