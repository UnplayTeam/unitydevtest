using UnityEngine;

namespace JoshBowersDEV.Characters
{
    public enum Race
    {
        Human,
        Elf,
        Orc,
        Hybrid
    }

    public class CharacterMeshData : ScriptableObject
    {
        #region General Properties

        private Race _race;

        public Race Race
        {
            get => _race;
            set => _race = value;
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _firstRace;

        public Race FirstRace
        {
            get => _firstRace;
            set => _firstRace = value;
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _secondRace;

        public Race SecondRace
        {
            get => _secondRace;
            set => _secondRace = value;
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private float _hybridBlend;

        public float HybridBlend
        {
            get => _hybridBlend;
            set => _hybridBlend = value;
        }

        private float _gender;

        public float Gender
        {
            get => _gender;
            set => _gender = value;
        }

        #endregion General Properties

        #region Head Properties

        [SerializeField]
        private float _earScale;

        public float EarScale
        {
            get => _earScale;
            set => _earScale = value;
        }

        [SerializeField]
        private float _earLobeSize;

        public float EarLobeSize
        {
            get => _earLobeSize;
            set => _earLobeSize = value;
        }

        [SerializeField]
        private float _earsOut;

        public float EarsOut
        {
            get => _earsOut;
            set => _earsOut = value;
        }

        [SerializeField]
        private float _browWide;

        public float BrowWide
        {
            get => _browWide;
            set => _browWide = value;
        }

        [SerializeField]
        private float _browForward;

        public float BrowForward
        {
            get => _browForward;
            set => _browForward = value;
        }

        [SerializeField]
        private float _facialCheekbonesInOut;

        public float FacialCheekbonesInOut
        {
            get => _facialCheekbonesInOut;
            set => _facialCheekbonesInOut = value;
        }

        [SerializeField]
        private float _facialCheeksGauntFull;

        public float FacialCheeksGauntFull
        {
            get => _facialCheeksGauntFull;
            set => _facialCheeksGauntFull = value;
        }

        [SerializeField]
        private float _facialChinTipLength;

        public float FacialChinTipLength
        {
            get => _facialChinTipLength;
            set => _facialChinTipLength = value;
        }

        [SerializeField]
        private float _facialChinTipWidth;

        public float FacialChinTipWidth
        {
            get => _facialChinTipWidth;
            set => _facialChinTipWidth = value;
        }

        [SerializeField]
        private float _facialJawDown;

        public float FacialJawDown
        {
            get => _facialJawDown;
            set => _facialJawDown = value;
        }

        [SerializeField]
        private float _facialJawWide;

        public float FacialJawWide
        {
            get => _facialJawWide;
            set => _facialJawWide = value;
        }

        [SerializeField]
        private float _facialLipTopThinFull;

        public float FacialLipTopThinFull
        {
            get => _facialLipTopThinFull;
            set => _facialLipTopThinFull = value;
        }

        [SerializeField]
        private float _facialLipBotThinFull;

        public float FacialLipBotThinFull
        {
            get => _facialLipBotThinFull;
            set => _facialLipBotThinFull = value;
        }

        [SerializeField]
        private float _facialMouthCrease;

        public float FacialMouthCrease
        {
            get => _facialMouthCrease;
            set => _facialMouthCrease = value;
        }

        [SerializeField]
        private float _facialMouthWidth;

        public float FacialMouthWidth
        {
            get => _facialMouthWidth;
            set => _facialMouthWidth = value;
        }

        [SerializeField]
        private float _facialMouthOut;

        public float FacialMouthOut
        {
            get => _facialMouthOut;
            set => _facialMouthOut = value;
        }

        [SerializeField]
        private float _facialNoseAngle;

        public float FacialNoseAngle
        {
            get => _facialNoseAngle;
            set => _facialNoseAngle = value;
        }

        [SerializeField]
        private float _facialNoseBulb;

        public float FacialNoseBulb
        {
            get => _facialNoseBulb;
            set => _facialNoseBulb = value;
        }

        [SerializeField]
        private float _facialNoseBridgeDepth;

        public float FacialNoseBridgeDepth
        {
            get => _facialNoseBridgeDepth;
            set => _facialNoseBridgeDepth = value;
        }

        [SerializeField]
        private float _facialNoseBridgeWidth;

        public float facialNoseBridgeWidth
        {
            get => _facialNoseBridgeWidth;
            set => _facialNoseBridgeWidth = value;
        }

        [SerializeField]
        private float _facialNoseLength;

        public float FacialNoseLength
        {
            get => _facialNoseLength;
            set => _facialNoseLength = value;
        }

        [SerializeField]
        private float _facialNoseTipWidthInOut;

        public float FacialNoseTipWidthInOut
        {
            get => _facialNoseTipWidthInOut;
            set => _facialNoseTipWidthInOut = value;
        }

        #endregion Head Properties

        #region Upper Body Properties

        [SerializeField]
        private float _bodyMuscularMidHeavy;

        public float BodyMuscularMidHeavy
        {
            get => _bodyMuscularMidHeavy;
            set => _bodyMuscularMidHeavy = value;
        }

        [SerializeField]
        private float _bodyWeightThinHeavy;

        public float BodyWeightThinHeavy
        {
            get => _bodyWeightThinHeavy;
            set => _bodyWeightThinHeavy = value;
        }

        [SerializeField]
        private float _isoBack;

        public float IsoBack
        {
            get => _isoBack;
            set => _isoBack = value;
        }

        [SerializeField]
        private float _isoBelly;

        public float IsoBelly
        {
            get => _isoBelly;
            set => _isoBelly = value;
        }

        [SerializeField]
        private float _isoBellyHeight;

        public float IsoBellyHeight
        {
            get => _isoBellyHeight;
            set => _isoBellyHeight = value;
        }

        [SerializeField]
        private float _isoBiceps;

        public float IsoBiceps
        {
            get => _isoBiceps;
            set => _isoBiceps = value;
        }

        [SerializeField]
        private float _isoBustSmallLarge;

        public float IsoBustSmallLarge
        {
            get => _isoBustSmallLarge;
            set => _isoBustSmallLarge = value;
        }

        [SerializeField]
        private float _isoButt;

        public float IsoButt
        {
            get => _isoButt;
            set => _isoButt = value;
        }

        [SerializeField]
        private float _isoDeltoids;

        public float IsoDeltoids
        {
            get => _isoDeltoids;
            set => _isoDeltoids = value;
        }

        [SerializeField]
        private float _isoForearms;

        public float IsoForearms
        {
            get => _isoForearms;
            set => _isoForearms = value;
        }

        [SerializeField]
        private float _isoPectorals;

        public float IsoPectorals
        {
            get => _isoPectorals;
            set => _isoPectorals = value;
        }

        [SerializeField]
        private float _isoRibcage;

        public float IsoRibcage
        {
            get => _isoRibcage;
            set => _isoRibcage = value;
        }

        [SerializeField]
        private float _isoTrunk;

        public float IsoTrunk
        {
            get => _isoTrunk;
            set => _isoTrunk = value;
        }

        [SerializeField]
        private float _isoTrapezius;

        public float IsoTrapezius
        {
            get => _isoTrapezius;
            set => _isoTrapezius = value;
        }

        #endregion Upper Body Properties

        #region Lower Body Properties

        [SerializeField]
        private float _legUpperIsoCalves;

        public float LegUpperIsoCalves
        {
            get => _legUpperIsoCalves;
            set => _legUpperIsoCalves = value;
        }

        [SerializeField]
        private float _legUpperIsoThighs;

        public float LegUpperIsoThighs
        {
            get => _legUpperIsoThighs;
            set => _legUpperIsoThighs = value;
        }

        [SerializeField]
        private float _waistIsoBulge;

        public float WaistIsoBulge
        {
            get => _waistIsoBulge;
            set => _waistIsoBulge = value;
        }

        #endregion Lower Body Properties
    }
}