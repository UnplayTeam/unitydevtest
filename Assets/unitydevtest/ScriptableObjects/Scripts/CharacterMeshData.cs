using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    #region Enums

    public enum Race
    {
        Human,
        Elf,
        Orc
    }

    public enum CharacterProperty
    {
        IsHybrid,
        Race,
        FirstRace,
        SecondRace,
        HybridBlend,
        HumanRace,
        ElfRace,
        OrcRace,
        Female,
        Male,
        FemaleHuman,
        MaleHuman,
        FemaleElf,
        MaleElf,
        FemaleOrc,
        MaleOrc,
        EarScale,
        EarLobeSize,
        EarsOut,
        BrowWide,
        BrowForward,
        FacialCheekbonesInOut,
        FacialCheeksGauntFull,
        FacialChinTipLength,
        FacialChinTipWidth,
        FacialJawDown,
        FacialJawWide,
        FacialLipTopThinFull,
        FacialLipBotThinFull,
        FacialMouthCrease,
        FacialMouthWidth,
        FacialMouthOut,
        FacialNoseAngle,
        FacialNoseBulb,
        FacialNoseBridgeDepth,
        FacialNoseBridgeWidth,
        FacialNoseLength,
        FacialNoseTipWidthInOut,
        BodyMuscularMidHeavy,
        BodyWeightThinHeavy,
        IsoBack,
        IsoBelly,
        IsoBellyHeight,
        IsoBiceps,
        IsoBustSmallLarge,
        IsoButt,
        IsoDeltoids,
        IsoForearms,
        IsoPectorals,
        IsoRibcage,
        IsoTrunk,
        IsoTrapezius,
        LegUpperIsoCalves,
        LegUpperIsoThighs,
        WaistIsoBulge
    }

    #endregion Enums

    [CreateAssetMenu(fileName = "Character", menuName = "Characters/Data")]
    [Binding]
    public class CharacterMeshData : BindableScriptableObjectBase
    {
        #region Events

        public Action<string, float> PropertyChangedEvent;

        #endregion Events

        #region Overrides

        // Override for alerting non-weld members that a change has been made
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            float floatValue = Convert.ToSingle(value); // Convert the generic value to a float.
            PropertyChangedEvent?.Invoke(propertyName, floatValue);
            return true;
        }

        #endregion Overrides

        #region Racial and Gender Properties

        [SerializeField]
        private bool _isHybrid;

        [Binding]
        public bool IsHybrid
        {
            get => _isHybrid;
            set => SetProperty(ref _isHybrid, value);
        }

        private int _raceInt = -1;

        [Binding]
        public int RaceInt
        {
            get => _raceInt;
            set
            {
                SetProperty(ref _raceInt, value);
                byte tryVal = Convert.ToByte(value);
                Race = (Race)tryVal;
            }
        }

        [SerializeField]
        private Race _race = Race.Human;

        [Binding]
        public Race Race
        {
            get => _race;
            set
            {
                if (!_isHybrid)
                {
                    switch (value)
                    {
                        case Race.Human:
                            SetRaceProperties(1.0f, 0f, 0f);
                            break;

                        case Race.Elf:
                            SetRaceProperties(0f, 1.0f, 0f);
                            break;

                        case Race.Orc:
                            SetRaceProperties(0f, 0f, 1f);
                            break;

                        default:
                            break;
                    }
                    SetProperty(ref _race, value);
                }
            }
        }

        private void SetRaceProperties(float humanValue, float elfValue, float orcValue)
        {
            if (!_isHybrid)
            {
                HumanRace = humanValue;
                ElfRace = elfValue;
                OrcRace = orcValue;
            }
        }

        #region Disabled for now, out of scope.

        // Racial and Gender Properties
        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _firstRace;

        [Binding]
        public Race FirstRace { get => _firstRace; set => SetProperty(ref _firstRace, value); }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _secondRace;

        [Binding]
        public Race SecondRace { get => _secondRace; set => SetProperty(ref _secondRace, value); }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private float _hybridBlend;

        [Binding]
        public float HybridBlend { get => _hybridBlend; set => SetProperty(ref _hybridBlend, value); }

        #endregion Disabled for now, out of scope.

        [Binding]
        public float HumanRace { get; private set; }

        [Binding]
        public float ElfRace { get; private set; }

        [Binding]
        public float OrcRace { get; private set; }

        // Gender Properties
        [SerializeField] private float _femaleMale;

        public float FemaleMale { get => _femaleMale; set => SetProperty(ref _femaleMale, value); }

        [Binding] public float RacialFemaleHuman => ((FemaleMale < 0) ? Mathf.Abs(FemaleMale) : 0) * HumanRace;
        [Binding] public float RacialMaleHuman => ((FemaleMale > 0) ? FemaleMale : 0) * HumanRace;
        [Binding] public float RacialFemaleElf => ((FemaleMale < 0) ? Mathf.Abs(FemaleMale) : 0) * ElfRace;
        [Binding] public float RacialMaleElf => ((FemaleMale > 0) ? FemaleMale : 0) * ElfRace;
        [Binding] public float RacialFemaleOrc => ((FemaleMale < 0) ? Mathf.Abs(FemaleMale) : 0) * OrcRace;
        [Binding] public float RacialMaleOrc => ((FemaleMale > 0) ? FemaleMale : 0) * OrcRace;

        // Head Properties
        [SerializeField] private float _facialEarScale;

        [Binding] public float FacialEarScale { get => _facialEarScale; set => SetProperty(ref _facialEarScale, value); }

        [SerializeField] private float _facialEarLobeSize;
        [Binding] public float FacialEarLobeSize { get => _facialEarLobeSize; set => SetProperty(ref _facialEarLobeSize, value); }

        [SerializeField] private float _facialEarsOut;
        [Binding] public float FacialEarsOut { get => _facialEarsOut; set => SetProperty(ref _facialEarsOut, value); }

        [SerializeField] private float _facialBrowWide;
        [Binding] public float FacialBrowWide { get => _facialBrowWide; set => SetProperty(ref _facialBrowWide, value); }

        [SerializeField] private float _facialBrowForward;
        [Binding] public float FacialBrowForward { get => _facialBrowForward; set => SetProperty(ref _facialBrowForward, value); }

        [SerializeField] private float _facialCheekbonesInOut;
        [Binding] public float FacialCheekbonesInOut { get => _facialCheekbonesInOut; set => SetProperty(ref _facialCheekbonesInOut, value); }

        [SerializeField] private float _facialCheeksGauntFull;
        [Binding] public float FacialCheeksGauntFull { get => _facialCheeksGauntFull; set => SetProperty(ref _facialCheeksGauntFull, value); }

        [SerializeField] private float _facialChinTipLength;
        [Binding] public float FacialChinTipLength { get => _facialChinTipLength; set => SetProperty(ref _facialChinTipLength, value); }

        [SerializeField] private float _facialChinTipWidth;
        [Binding] public float FacialChinTipWidth { get => _facialChinTipWidth; set => SetProperty(ref _facialChinTipWidth, value); }

        [SerializeField] private float _facialJawDown;
        [Binding] public float FacialJawDown { get => _facialJawDown; set => SetProperty(ref _facialJawDown, value); }

        [SerializeField] private float _facialJawWide;
        [Binding] public float FacialJawWide { get => _facialJawWide; set => SetProperty(ref _facialJawWide, value); }

        [SerializeField] private float _facialLipTopThinFull;
        [Binding] public float FacialLipTopThinFull { get => _facialLipTopThinFull; set => SetProperty(ref _facialLipTopThinFull, value); }

        [SerializeField] private float _facialLipBotThinFull;
        [Binding] public float FacialLipBotThinFull { get => _facialLipBotThinFull; set => SetProperty(ref _facialLipBotThinFull, value); }

        [SerializeField] private float _facialMouthCrease;
        [Binding] public float FacialMouthCrease { get => _facialMouthCrease; set => SetProperty(ref _facialMouthCrease, value); }

        [SerializeField] private float _facialMouthWidth;
        [Binding] public float FacialMouthWidth { get => _facialMouthWidth; set => SetProperty(ref _facialMouthWidth, value); }

        [SerializeField] private float _facialMouthOut;
        [Binding] public float FacialMouthOut { get => _facialMouthOut; set => SetProperty(ref _facialMouthOut, value); }

        [SerializeField] private float _facialNoseAngle;
        [Binding] public float FacialNoseAngle { get => _facialNoseAngle; set => SetProperty(ref _facialNoseAngle, value); }

        [SerializeField] private float _facialNoseBulb;
        [Binding] public float FacialNoseBulb { get => _facialNoseBulb; set => SetProperty(ref _facialNoseBulb, value); }

        [SerializeField] private float _facialNoseBridgeDepth;
        [Binding] public float FacialNoseBridgeDepth { get => _facialNoseBridgeDepth; set => SetProperty(ref _facialNoseBridgeDepth, value); }

        [SerializeField] private float _facialNoseBridgeWidth;
        [Binding] public float FacialNoseBridgeWidth { get => _facialNoseBridgeWidth; set => SetProperty(ref _facialNoseBridgeWidth, value); }

        [SerializeField] private float _facialNoseLength;
        [Binding] public float FacialNoseLength { get => _facialNoseLength; set => SetProperty(ref _facialNoseLength, value); }

        [SerializeField] private float _facialNoseTipWidthInOut;
        [Binding] public float FacialNoseTipWidthInOut { get => _facialNoseTipWidthInOut; set => SetProperty(ref _facialNoseTipWidthInOut, value); }

        #endregion Racial and Gender Properties

        #region Upper Body Properties

        [SerializeField] private float _bodyMuscularMidHeavy;
        [Binding] public float BodyMuscularMidHeavy { get => _bodyMuscularMidHeavy; set => SetProperty(ref _bodyMuscularMidHeavy, value); }
        [SerializeField] private float _bodyWeightThinHeavy;
        [Binding] public float BodyWeightThinHeavy { get => _bodyWeightThinHeavy; set => SetProperty(ref _bodyWeightThinHeavy, value); }
        [SerializeField] private float _isoBack;
        [Binding] public float IsoBack { get => _isoBack; set => SetProperty(ref _isoBack, value); }
        [SerializeField] private float _isoBelly;
        [Binding] public float IsoBelly { get => _isoBelly; set => SetProperty(ref _isoBelly, value); }
        [SerializeField] private float _isoBellyHeight;
        [Binding] public float IsoBellyHeight { get => _isoBellyHeight; set => SetProperty(ref _isoBellyHeight, value); }
        [SerializeField] private float _isoBiceps;
        [Binding] public float IsoBiceps { get => _isoBiceps; set => SetProperty(ref _isoBiceps, value); }
        [SerializeField] private float _isoBustSmallLarge;
        [Binding] public float IsoBustSmallLarge { get => _isoBustSmallLarge; set => SetProperty(ref _isoBustSmallLarge, value); }
        [SerializeField] private float _isoButt;
        [Binding] public float IsoButt { get => _isoButt; set => SetProperty(ref _isoButt, value); }
        [SerializeField] private float _isoDeltoids;
        [Binding] public float IsoDeltoids { get => _isoDeltoids; set => SetProperty(ref _isoDeltoids, value); }
        [SerializeField] private float _isoForearms;
        [Binding] public float IsoForearms { get => _isoForearms; set => SetProperty(ref _isoForearms, value); }
        [SerializeField] private float _isoPectorals;
        [Binding] public float IsoPectorals { get => _isoPectorals; set => SetProperty(ref _isoPectorals, value); }
        [SerializeField] private float _isoRibcage;
        [Binding] public float IsoRibcage { get => _isoRibcage; set => SetProperty(ref _isoRibcage, value); }
        [SerializeField] private float _isoTrunk;
        [Binding] public float IsoTrunk { get => _isoTrunk; set => SetProperty(ref _isoTrunk, value); }
        [SerializeField] private float _isoTrapezius;
        [Binding] public float IsoTrapezius { get => _isoTrapezius; set => SetProperty(ref _isoTrapezius, value); }

        #endregion Upper Body Properties

        #region Lower Body Properties

        [SerializeField] private float _legUpperIsoCalves;
        [Binding] public float LegUpperIsoCalves { get => _legUpperIsoCalves; set => SetProperty(ref _legUpperIsoCalves, value); }
        [SerializeField] private float _legUpperIsoThighs;
        [Binding] public float LegUpperIsoThighs { get => _legUpperIsoThighs; set => SetProperty(ref _legUpperIsoThighs, value); }
        [SerializeField] private float _waistIsoBulge;
        [Binding] public float WaistIsoBulge { get => _waistIsoBulge; set => SetProperty(ref _waistIsoBulge, value); }

        #endregion Lower Body Properties

        #region Public Setters

        // Racial and Gender Properties
        public void SetIsHybrid(bool value) => IsHybrid = value;

        public void SetRaceInt(int value) => RaceInt = value;

        public void SetRace(Race value) => Race = value;

        public void SetFirstRace(Race value) => FirstRace = value;

        public void SetSecondRace(Race value) => SecondRace = value;

        public void SetHybridBlend(float value) => HybridBlend = value;

        public void SetFemaleMale(float value) => FemaleMale = value;

        // Head Properties
        public void SetFacialEarScale(float value) => FacialEarScale = value;

        public void SetFacialEarLobeSize(float value) => FacialEarLobeSize = value;

        public void SetFacialEarsOut(float value) => FacialEarsOut = value;

        public void SetFacialBrowWide(float value) => FacialBrowWide = value;

        public void SetFacialBrowForward(float value) => FacialBrowForward = value;

        public void SetFacialCheekbonesInOut(float value) => FacialCheekbonesInOut = value;

        public void SetFacialCheeksGauntFull(float value) => FacialCheeksGauntFull = value;

        public void SetFacialChinTipLength(float value) => FacialChinTipLength = value;

        public void SetFacialChinTipWidth(float value) => FacialChinTipWidth = value;

        public void SetFacialJawDown(float value) => FacialJawDown = value;

        public void SetFacialJawWide(float value) => FacialJawWide = value;

        public void SetFacialLipTopThinFull(float value) => FacialLipTopThinFull = value;

        public void SetFacialLipBotThinFull(float value) => FacialLipBotThinFull = value;

        public void SetFacialMouthCrease(float value) => FacialMouthCrease = value;

        public void SetFacialMouthWidth(float value) => FacialMouthWidth = value;

        public void SetFacialMouthOut(float value) => FacialMouthOut = value;

        public void SetFacialNoseAngle(float value) => FacialNoseAngle = value;

        public void SetFacialNoseBulb(float value) => FacialNoseBulb = value;

        public void SetFacialNoseBridgeDepth(float value) => FacialNoseBridgeDepth = value;

        public void SetFacialNoseBridgeWidth(float value) => FacialNoseBridgeWidth = value;

        public void SetFacialNoseLength(float value) => FacialNoseLength = value;

        public void SetFacialNoseTipWidthInOut(float value) => FacialNoseTipWidthInOut = value;

        // Upper Body Properties
        public void SetBodyMuscularMidHeavy(float value) => BodyMuscularMidHeavy = value;

        public void SetBodyWeightThinHeavy(float value) => BodyWeightThinHeavy = value;

        public void SetIsoBack(float value) => IsoBack = value;

        public void SetIsoBelly(float value) => IsoBelly = value;

        public void SetIsoBellyHeight(float value) => IsoBellyHeight = value;

        public void SetIsoBiceps(float value) => IsoBiceps = value;

        public void SetIsoBustSmallLarge(float value) => IsoBustSmallLarge = value;

        public void SetIsoButt(float value) => IsoButt = value;

        public void SetIsoDeltoids(float value) => IsoDeltoids = value;

        public void SetIsoForearms(float value) => IsoForearms = value;

        public void SetIsoPectorals(float value) => IsoPectorals = value;

        public void SetIsoRibcage(float value) => IsoRibcage = value;

        public void SetIsoTrunk(float value) => IsoTrunk = value;

        public void SetIsoTrapezius(float value) => IsoTrapezius = value;

        // Lower Body Properties
        public void SetLegUpperIsoCalves(float value) => LegUpperIsoCalves = value;

        public void SetLegUpperIsoThighs(float value) => LegUpperIsoThighs = value;

        public void SetWaistIsoBulge(float value) => WaistIsoBulge = value;

        #endregion Public Setters
    }
}