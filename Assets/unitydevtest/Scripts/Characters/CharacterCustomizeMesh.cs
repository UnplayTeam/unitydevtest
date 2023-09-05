using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    /// <summary>
    /// Bindable mono for exposing current Mesh information for Read/Write, and gives a binding pointer for any children
    /// utilizing UnityWeld that need specific data.
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
            set => SetProperty(ref _characterMeshData, value);
        }

        #endregion Properties

        #region Racial and Gender Properties

        private bool _isHybrid;

        [Binding]
        public bool IsHybrid
        {
            get => _isHybrid;
            set => SetProperty(ref _isHybrid, value);
        }

        [Binding]
        public int RaceInt
        {
            get => CharacterMeshData.RaceInt;
            set => CharacterMeshData.SetRaceInt(value);
        }

        [Binding]
        public Race Race
        {
            get => CharacterMeshData.Race;
        }

        #region Currently out of scope

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

        #endregion Currently out of scope

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

        [Binding]
        public float FemaleMale
        {
            get => CharacterMeshData.FemMasc;
            set => CharacterMeshData.SetFemMasc(value);
        }

        [Binding]
        public float RacialFemaleHuman
        {
            get => CharacterMeshData.RacialFemHuman;
        }

        [Binding]
        public float RacialMaleHuman
        {
            get => CharacterMeshData.RacialMascHuman;
        }

        [Binding]
        public float RacialFemaleElf
        {
            get => CharacterMeshData.RacialFemElf;
        }

        [Binding]
        public float RacialMaleElf
        {
            get => CharacterMeshData.RacialMascElf;
        }

        [Binding]
        public float RacialFemaleOrc
        {
            get => CharacterMeshData.RacialFemOrc;
        }

        [Binding]
        public float RacialMaleOrc
        {
            get => CharacterMeshData.RacialMascOrc;
        }

        #endregion Racial and Gender Properties

        #region Head Properties

        [Binding]
        public float FacialEarScale
        {
            get => CharacterMeshData.FacialEarScale;
            set => CharacterMeshData.SetFacialEarScale(value);
        }

        [Binding]
        public float FacialEarLobeSize
        {
            get => CharacterMeshData.FacialEarLobeSize;
            set => CharacterMeshData.SetFacialEarLobeSize(value);
        }

        [Binding]
        public float FacialEarsOut
        {
            get => CharacterMeshData.FacialEarsOut;
            set => CharacterMeshData.SetFacialEarsOut(value);
        }

        [Binding]
        public float FacialBrowWide
        {
            get => CharacterMeshData.FacialBrowWide;
            set => CharacterMeshData.SetFacialBrowWide(value);
        }

        [Binding]
        public float FacialBrowForward
        {
            get => CharacterMeshData.FacialBrowForward;
            set => CharacterMeshData.SetFacialBrowForward(value);
        }

        [Binding]
        public float FacialCheekbonesInOut
        {
            get => CharacterMeshData.FacialCheekbonesInOut;
            set => CharacterMeshData.SetFacialCheekbonesInOut(value);
        }

        [Binding]
        public float FacialCheeksGauntFull
        {
            get => CharacterMeshData.FacialCheeksGauntFull;
            set => CharacterMeshData.SetFacialCheeksGauntFull(value);
        }

        [Binding]
        public float FacialChinTipLength
        {
            get => CharacterMeshData.FacialChinTipLength;
            set => CharacterMeshData.SetFacialChinTipLength(value);
        }

        [Binding]
        public float FacialChinTipWidth
        {
            get => CharacterMeshData.FacialChinTipWidth;
            set => CharacterMeshData.SetFacialChinTipWidth(value);
        }

        [Binding]
        public float FacialJawDown
        {
            get => CharacterMeshData.FacialJawDown;
            set => CharacterMeshData.SetFacialJawDown(value);
        }

        [Binding]
        public float FacialJawWide
        {
            get => CharacterMeshData.FacialJawWide;
            set => CharacterMeshData.SetFacialJawWide(value);
        }

        [Binding]
        public float FacialLipTopThinFull
        {
            get => CharacterMeshData.FacialLipTopThinFull;
            set => CharacterMeshData.SetFacialLipTopThinFull(value);
        }

        [Binding]
        public float FacialLipBotThinFull
        {
            get => CharacterMeshData.FacialLipBotThinFull;
            set => CharacterMeshData.SetFacialLipBotThinFull(value);
        }

        [Binding]
        public float FacialMouthCrease
        {
            get => CharacterMeshData.FacialMouthCrease;
            set => CharacterMeshData.SetFacialMouthCrease(value);
        }

        [Binding]
        public float FacialMouthWidth
        {
            get => CharacterMeshData.FacialMouthWidth;
            set => CharacterMeshData.SetFacialMouthWidth(value);
        }

        [Binding]
        public float FacialMouthOut
        {
            get => CharacterMeshData.FacialMouthOut;
            set => CharacterMeshData.SetFacialMouthOut(value);
        }

        [Binding]
        public float FacialNoseAngle
        {
            get => CharacterMeshData.FacialNoseAngle;
            set => CharacterMeshData.SetFacialNoseAngle(value);
        }

        [Binding]
        public float FacialNoseBulb
        {
            get => CharacterMeshData.FacialNoseBulb;
            set => CharacterMeshData.SetFacialNoseBulb(value);
        }

        [Binding]
        public float FacialNoseBridgeDepth
        {
            get => CharacterMeshData.FacialNoseBridgeDepth;
            set => CharacterMeshData.SetFacialNoseBridgeDepth(value);
        }

        [Binding]
        public float facialNoseBridgeWidth
        {
            get => CharacterMeshData.FacialNoseBridgeWidth;
            set => CharacterMeshData.SetFacialNoseBridgeWidth(value);
        }

        [Binding]
        public float FacialNoseLength
        {
            get => CharacterMeshData.FacialNoseLength;
            set => CharacterMeshData.SetFacialNoseLength(value);
        }

        [Binding]
        public float FacialNoseTipWidthInOut
        {
            get => CharacterMeshData.FacialNoseTipWidthInOut;
            set => CharacterMeshData.SetFacialNoseTipWidthInOut(value);
        }

        #endregion Head Properties

        #region Upper Body Properties

        [Binding]
        public float BodyMuscularMidHeavy
        {
            get => CharacterMeshData.BodyMuscularMidHeavy;
            set => CharacterMeshData.SetBodyMuscularMidHeavy(value);
        }

        [Binding]
        public float BodyWeightThinHeavy
        {
            get => CharacterMeshData.BodyWeightThinHeavy;
            set => CharacterMeshData.SetBodyWeightThinHeavy(value);
        }

        [Binding]
        public float IsoBack
        {
            get => CharacterMeshData.IsoBack;
            set => CharacterMeshData.SetIsoBack(value);
        }

        [Binding]
        public float IsoBelly
        {
            get => CharacterMeshData.IsoBelly;
            set => CharacterMeshData.SetIsoBelly(value);
        }

        [Binding]
        public float IsoBellyHeight
        {
            get => CharacterMeshData.IsoBellyHeight;
            set => CharacterMeshData.SetIsoBellyHeight(value);
        }

        [Binding]
        public float IsoBiceps
        {
            get => CharacterMeshData.IsoBiceps;
            set => CharacterMeshData.SetIsoBiceps(value);
        }

        [Binding]
        public float IsoBustSmallLarge
        {
            get => CharacterMeshData.IsoBustSmallLarge;
            set => CharacterMeshData.SetIsoBustSmallLarge(value);
        }

        [Binding]
        public float IsoButt
        {
            get => CharacterMeshData.IsoButt;
            set => CharacterMeshData.SetIsoButt(value);
        }

        [Binding]
        public float IsoDeltoids
        {
            get => CharacterMeshData.IsoDeltoids;
            set => CharacterMeshData.SetIsoDeltoids(value);
        }

        [Binding]
        public float IsoForearms
        {
            get => CharacterMeshData.IsoForearms;
            set => CharacterMeshData.SetIsoForearms(value);
        }

        [Binding]
        public float IsoPectorals
        {
            get => CharacterMeshData.IsoPectorals;
            set => CharacterMeshData.SetIsoPectorals(value);
        }

        [Binding]
        public float IsoRibcage
        {
            get => CharacterMeshData.IsoRibcage;
            set => CharacterMeshData.SetIsoRibcage(value);
        }

        [Binding]
        public float IsoTrunk
        {
            get => CharacterMeshData.IsoTrunk;
            set => CharacterMeshData.SetIsoTrunk(value);
        }

        [Binding]
        public float IsoTrapezius
        {
            get => CharacterMeshData.IsoTrapezius;
            set => CharacterMeshData.SetIsoTrapezius(value);
        }

        [Binding]
        public float IsoTriceps
        {
            get => CharacterMeshData.IsoTriceps;
            set => CharacterMeshData.SetIsoTriceps(value);
        }

        #endregion Upper Body Properties

        #region Lower Body Properties

        [Binding]
        public float LegUpperIsoCalves
        {
            get => CharacterMeshData.LegUpperIsoCalves;
            set => CharacterMeshData.SetLegUpperIsoCalves(value);
        }

        [Binding]
        public float LegUpperIsoThighs
        {
            get => CharacterMeshData.LegUpperIsoThighs;
            set => CharacterMeshData.SetLegUpperIsoThighs(value);
        }

        [Binding]
        public float WaistIsoBulge
        {
            get => CharacterMeshData.WaistIsoBulge;
            set => CharacterMeshData.SetWaistIsoBulge(value);
        }

        #endregion Lower Body Properties
    }
}