using UnityEngine;
using UnityWeld.Binding;

namespace JoshBowersDEV.Characters
{
    public enum Race
    {
        Human,
        Elf,
        Orc
    }

    [CreateAssetMenu(fileName = "Character", menuName = "Characters/Data")]
    public class CharacterMeshData : BindableScriptableObjectBase
    {
        #region Racial and Gender Properties

        [SerializeField]
        private bool _isHybrid;

        [Binding]
        public bool IsHybrid
        {
            get => _isHybrid;
            set => SetProperty(ref _isHybrid, value);
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
                            HumanRace = 1.0f;
                            ElfRace = 0f;
                            OrcRace = 0f;
                            break;

                        case Race.Elf:
                            HumanRace = 0f;
                            ElfRace = 1.0f;
                            OrcRace = 0f;
                            break;

                        case Race.Orc:
                            HumanRace = 0f;
                            ElfRace = 0f;
                            OrcRace = 1f;
                            break;

                        default:
                            break;
                    }
                    SetProperty(ref _race, value);
                }
            }
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _firstRace;

        [Binding]
        public Race FirstRace
        {
            get => _firstRace;
            set => SetProperty(ref _firstRace, value);
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private Race _secondRace;

        [Binding]
        public Race SecondRace
        {
            get => _secondRace;
            set => SetProperty(ref _secondRace, value);
        }

        [Tooltip("Only valid if Race is hybrid")]
        [SerializeField]
        private float _hybridBlend;

        [Binding]
        public float HybridBlend
        {
            get => _hybridBlend;
            set => SetProperty(ref _hybridBlend, value);
        }

        private float _humanRace;

        // Do not serialize the below race sliders, these will be determined elsewhere.
        [Binding]
        public float HumanRace
        {
            get => _humanRace;
            set => SetProperty(ref _humanRace, value);
        }

        private float _elfRace;

        [Binding]
        public float ElfRace
        {
            get => _elfRace;
            set => SetProperty(ref _elfRace, value);
        }

        private float _orcRace;

        [Binding]
        public float OrcRace
        {
            get => _orcRace;
            set => SetProperty(ref _orcRace, value);
        }

        private float _gender;

        public float Gender
        {
            get => _gender;
            set
            {
                SetProperty(ref _gender, value);
            }
        }

        [SerializeField]
        private float _humanFemale;

        [Binding]
        public float HumanGender
        {
            get => Gender * HumanRace;
        }

        [SerializeField]
        private float _humanMale;

        [Binding]
        public float HumanMale
        {
            get => _humanMale;
        }

        [SerializeField]
        private float _orcFemale;

        [Binding]
        public float OrcFemale
        {
            get => _orcFemale;
            set
            {
            }
        }

        [SerializeField]
        private float _orcMale;

        [Binding]
        public float OrcMale
        {
            get => _orcMale;
            set
            {
            }
        }

        [SerializeField]
        private float _elfFemale;

        [Binding]
        public float ElfFemale
        {
            get => _elfFemale;
            set
            {
            }
        }

        [SerializeField]
        private float _elfMale;

        [Binding]
        public float ElfMale
        {
            get => _elfMale;
            set
            {
            }
        }

        #endregion Racial and Gender Properties

        #region Head Properties

        [SerializeField]
        private float _earScale;

        [Binding]
        public float EarScale
        {
            get => _earScale;
            set => SetProperty(ref _earScale, value);
        }

        [SerializeField]
        private float _earLobeSize;

        [Binding]
        public float EarLobeSize
        {
            get => _earLobeSize;
            set => SetProperty(ref _earLobeSize, value);
        }

        [SerializeField]
        private float _earsOut;

        [Binding]
        public float EarsOut
        {
            get => _earsOut;
            set => SetProperty(ref _earsOut, value);
        }

        [SerializeField]
        private float _browWide;

        [Binding]
        public float BrowWide
        {
            get => _browWide;
            set => SetProperty(ref _browWide, value);
        }

        [SerializeField]
        private float _browForward;

        [Binding]
        public float BrowForward
        {
            get => _browForward;
            set => SetProperty(ref _browForward, value);
        }

        [SerializeField]
        private float _facialCheekbonesInOut;

        [Binding]
        public float FacialCheekbonesInOut
        {
            get => _facialCheekbonesInOut;
            set => SetProperty(ref _facialCheekbonesInOut, value);
        }

        [SerializeField]
        private float _facialCheeksGauntFull;

        [Binding]
        public float FacialCheeksGauntFull
        {
            get => _facialCheeksGauntFull;
            set => SetProperty(ref _facialCheeksGauntFull, value);
        }

        [SerializeField]
        private float _facialChinTipLength;

        [Binding]
        public float FacialChinTipLength
        {
            get => _facialChinTipLength;
            set => SetProperty(ref _facialChinTipLength, value);
        }

        [SerializeField]
        private float _facialChinTipWidth;

        [Binding]
        public float FacialChinTipWidth
        {
            get => _facialChinTipWidth;
            set => SetProperty(ref _facialChinTipWidth, value);
        }

        [SerializeField]
        private float _facialJawDown;

        [Binding]
        public float FacialJawDown
        {
            get => _facialJawDown;
            set => SetProperty(ref _facialJawDown, value);
        }

        [SerializeField]
        private float _facialJawWide;

        [Binding]
        public float FacialJawWide
        {
            get => _facialJawWide;
            set => SetProperty(ref _facialJawWide, value);
        }

        [SerializeField]
        private float _facialLipTopThinFull;

        [Binding]
        public float FacialLipTopThinFull
        {
            get => _facialLipTopThinFull;
            set => SetProperty(ref _facialLipTopThinFull, value);
        }

        [SerializeField]
        private float _facialLipBotThinFull;

        [Binding]
        public float FacialLipBotThinFull
        {
            get => _facialLipBotThinFull;
            set => SetProperty(ref _facialLipBotThinFull, value);
        }

        [SerializeField]
        private float _facialMouthCrease;

        [Binding]
        public float FacialMouthCrease
        {
            get => _facialMouthCrease;
            set => SetProperty(ref _facialMouthCrease, value);
        }

        [SerializeField]
        private float _facialMouthWidth;

        [Binding]
        public float FacialMouthWidth
        {
            get => _facialMouthWidth;
            set => SetProperty(ref _facialMouthWidth, value);
        }

        [SerializeField]
        private float _facialMouthOut;

        [Binding]
        public float FacialMouthOut
        {
            get => _facialMouthOut;
            set => SetProperty(ref _facialMouthOut, value);
        }

        [SerializeField]
        private float _facialNoseAngle;

        [Binding]
        public float FacialNoseAngle
        {
            get => _facialNoseAngle;
            set => SetProperty(ref _facialNoseAngle, value);
        }

        [SerializeField]
        private float _facialNoseBulb;

        [Binding]
        public float FacialNoseBulb
        {
            get => _facialNoseBulb;
            set => SetProperty(ref _facialNoseBulb, value);
        }

        [SerializeField]
        private float _facialNoseBridgeDepth;

        [Binding]
        public float FacialNoseBridgeDepth
        {
            get => _facialNoseBridgeDepth;
            set => SetProperty(ref _facialNoseBridgeDepth, value);
        }

        [SerializeField]
        private float _facialNoseBridgeWidth;

        [Binding]
        public float facialNoseBridgeWidth
        {
            get => _facialNoseBridgeWidth;
            set => SetProperty(ref _facialNoseBridgeWidth, value);
        }

        [SerializeField]
        private float _facialNoseLength;

        [Binding]
        public float FacialNoseLength
        {
            get => _facialNoseLength;
            set => SetProperty(ref _facialNoseLength, value);
        }

        [SerializeField]
        private float _facialNoseTipWidthInOut;

        [Binding]
        public float FacialNoseTipWidthInOut
        {
            get => _facialNoseTipWidthInOut;
            set => SetProperty(ref _facialNoseTipWidthInOut, value);
        }

        #endregion Head Properties

        #region Upper Body Properties

        [SerializeField]
        private float _bodyMuscularMidHeavy;

        [Binding]
        public float BodyMuscularMidHeavy
        {
            get => _bodyMuscularMidHeavy;
            set => SetProperty(ref _bodyMuscularMidHeavy, value);
        }

        [SerializeField]
        private float _bodyWeightThinHeavy;

        [Binding]
        public float BodyWeightThinHeavy
        {
            get => _bodyWeightThinHeavy;
            set => SetProperty(ref _bodyWeightThinHeavy, value);
        }

        [SerializeField]
        private float _isoBack;

        [Binding]
        public float IsoBack
        {
            get => _isoBack;
            set => SetProperty(ref _isoBack, value);
        }

        [SerializeField]
        private float _isoBelly;

        [Binding]
        public float IsoBelly
        {
            get => _isoBelly;
            set => SetProperty(ref _isoBelly, value);
        }

        [SerializeField]
        private float _isoBellyHeight;

        [Binding]
        public float IsoBellyHeight
        {
            get => _isoBellyHeight;
            set => SetProperty(ref _isoBellyHeight, value);
        }

        [SerializeField]
        private float _isoBiceps;

        [Binding]
        public float IsoBiceps
        {
            get => _isoBiceps;
            set => SetProperty(ref _isoBiceps, value);
        }

        [SerializeField]
        private float _isoBustSmallLarge;

        [Binding]
        public float IsoBustSmallLarge
        {
            get => _isoBustSmallLarge;
            set => SetProperty(ref _isoBustSmallLarge, value);
        }

        [SerializeField]
        private float _isoButt;

        [Binding]
        public float IsoButt
        {
            get => _isoButt;
            set => SetProperty(ref _isoButt, value);
        }

        [SerializeField]
        private float _isoDeltoids;

        [Binding]
        public float IsoDeltoids
        {
            get => _isoDeltoids;
            set => SetProperty(ref _isoDeltoids, value);
        }

        [SerializeField]
        private float _isoForearms;

        [Binding]
        public float IsoForearms
        {
            get => _isoForearms;
            set => SetProperty(ref _isoForearms, value);
        }

        [SerializeField]
        private float _isoPectorals;

        [Binding]
        public float IsoPectorals
        {
            get => _isoPectorals;
            set => SetProperty(ref _isoPectorals, value);
        }

        [SerializeField]
        private float _isoRibcage;

        [Binding]
        public float IsoRibcage
        {
            get => _isoRibcage;
            set => SetProperty(ref _isoRibcage, value);
        }

        [SerializeField]
        private float _isoTrunk;

        [Binding]
        public float IsoTrunk
        {
            get => _isoTrunk;
            set => SetProperty(ref _isoTrunk, value);
        }

        [SerializeField]
        private float _isoTrapezius;

        [Binding]
        public float IsoTrapezius
        {
            get => _isoTrapezius;
            set => SetProperty(ref _isoTrapezius, value);
        }

        #endregion Upper Body Properties

        #region Lower Body Properties

        [SerializeField]
        private float _legUpperIsoCalves;

        [Binding]
        public float LegUpperIsoCalves
        {
            get => _legUpperIsoCalves;
            set => SetProperty(ref _legUpperIsoCalves, value);
        }

        [SerializeField]
        private float _legUpperIsoThighs;

        [Binding]
        public float LegUpperIsoThighs
        {
            get => _legUpperIsoThighs;
            set => SetProperty(ref _legUpperIsoThighs, value);
        }

        [SerializeField]
        private float _waistIsoBulge;

        [Binding]
        public float WaistIsoBulge
        {
            get => _waistIsoBulge;
            set => SetProperty(ref _waistIsoBulge, value);
        }

        #endregion Lower Body Properties
    }
}