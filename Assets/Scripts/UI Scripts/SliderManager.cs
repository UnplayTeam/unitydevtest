using BlendshapeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Monobehaviour which manages all slider inputs in the character editor UI
/// </summary>
public class SliderManager : MonoBehaviour, BlendshapeInterface
{
    #region Member data
    // Object to abstract the calculations for gender and race - everything else is simple enough to compute here
    private GenderAndRaceManager genderAndRaceManager = new GenderAndRaceManager();

    [Header("Sliders")]
    // Race sliders which control racial features
    [SerializeField] private Slider _humanSlider = null;
    [SerializeField] private Slider _orcSlider = null;
    [SerializeField] private Slider _elfSlider = null;

    // Gender slider which controls how feminine or masculine the character appears
    [SerializeField] private Slider _genderSlider = null;

    // Weight slider which controls the weight of the character
    [SerializeField] private Slider _weightSlider = null;

    // Muscle tone slider which controls the muscle mass of the character
    [SerializeField] private Slider _muscleSlider = null;

    // Bust slider which controls how big the breasts are
    [SerializeField] private Slider _bustSlider = null;

    // Cheek fullness slider which controls whether the cheeks are full or gaunt
    [SerializeField] private Slider _cheekFullnessSlider = null;

    #region inspector-accessible inputs which modify the sliders' impact on character creation
    [Header("Muscle Slider Effects")]
    // Animation curve that determines how to translate slider values to mid muscle blendshape values
    [SerializeField] private AnimationCurve muscleMidCurve = new AnimationCurve();
    // Animation curve that determines how to translate slider values to heavy muscle blendshape values
    [SerializeField] private AnimationCurve muscleHeavyCurve = new AnimationCurve();

    [Header("Weight Slider Effects")]
    // Animation curve that determines how to translate slider values to thin weight blendshape values
    [SerializeField] private AnimationCurve weightThinCurve = new AnimationCurve();
    // Animation curve that determines how to translate slider values to heavy weight blendshape values
    [SerializeField] private AnimationCurve weightHeavyCurve = new AnimationCurve();
    #endregion

    #endregion

    #region Monobehaviour Functions

    /// <summary>
    /// Iniitalize all slider values when the applications starts
    /// </summary>
    private void Start()
    {
        Initialize();
    }

    #endregion

    #region Setup and preset slider values

    // Default variables for the sliders
    [SerializeField] private static readonly float[] defaultSliderValues = new float[]
    {
        1,      // Default gender
        1,      // Default human
        0,      // Default orc
        0,      // Default elf
        0.2f,   // Default muscle
        0.25f,  // Default weight
        0,      // Default bust
        0.15f   // Default cheek fullness
    };

    /// <summary>
    /// Initialize the slider UI by setting all slider values to default values defined by defaultSliderValues
    /// </summary>
    private void Initialize()
    {
        SetSliderValues(defaultSliderValues);
    }

    // Set the value for a single slider, if the slider exists and the value is valid
    void SetSliderValue(Slider target, float value)
    {
        if (target != null && !float.IsNaN(value))
        {
            target.value = Mathf.Clamp(value, 0, 1);
        }
    }

    // Set the value for all sliders at once
    private void SetSliderValues(params float[] values)
    {
        // If the list of values is the correct length, set all slider values
        if (values.Length == defaultSliderValues.Length && values.Length == 8)
        {
            SetSliderValue(_genderSlider,           values[0]);
            SetSliderValue(_humanSlider,            values[1]);
            SetSliderValue(_orcSlider,              values[2]);
            SetSliderValue(_elfSlider,              values[3]);
            SetSliderValue(_muscleSlider,           values[4]);
            SetSliderValue(_weightSlider,           values[5]);
            SetSliderValue(_bustSlider,             values[6]);
            SetSliderValue(_cheekFullnessSlider,    values[7]);
        }
    }

    #endregion

    #region Blendshape Interface Implementation
    /// <summary>
    /// Makes calling the interface function UpdateBlendshape easier
    /// </summary>
    private void UpdateBlendshape(BlendKey key, float value)
    {
        (this as BlendshapeInterface).UpdateBlendshape(key, value);
    }

    #endregion

    #region Gender and Race Handling
    /// <summary>
    /// Updates race sliders to match calculated values from the genderAndRaceManager
    /// Then updates blendshape values
    /// </summary>
    public void UpdateRaceSliders()
    {
        _humanSlider.SetValueWithoutNotify(genderAndRaceManager.human);
        _elfSlider.SetValueWithoutNotify(genderAndRaceManager.elf);
        _orcSlider.SetValueWithoutNotify(genderAndRaceManager.orc);

        SetGenderAndRaceBlendKeys();
    }

    /// <summary>
    /// Updates blendshape values to reflect the interplay of race and gender in the way that
    /// blendshapes were set up on the given 3D model.
    /// </summary>
    private void SetGenderAndRaceBlendKeys()
    {
        UpdateBlendshape(BlendKey.gender_fem,       genderAndRaceManager[BlendKey.gender_fem]);
        UpdateBlendshape(BlendKey.species_elf_fem,  genderAndRaceManager[BlendKey.species_elf_fem]);
        UpdateBlendshape(BlendKey.species_elf_masc, genderAndRaceManager[BlendKey.species_elf_masc]);
        UpdateBlendshape(BlendKey.species_orc_fem,  genderAndRaceManager[BlendKey.species_orc_fem]);
        UpdateBlendshape(BlendKey.species_orc_masc, genderAndRaceManager[BlendKey.species_orc_masc]);
    }
    #endregion

    #region Slider accessible setters
    // These functions respond to slider changes and update blendshape values accordingly

    // Sets the gender of the character being edited
    public void SetGender(float value)
    {
        genderAndRaceManager.gender = value;

        SetGenderAndRaceBlendKeys();
    }

    // Sets the human value of the character being edited
    public void SetHuman(float value)
    {
        genderAndRaceManager.human = value;
        UpdateRaceSliders();
    }

    // sets the orcish value of the character being edited
    public void SetOrc(float value)
    {
        genderAndRaceManager.orc = value;
        UpdateRaceSliders();
    }

    // Sets the elven value of the character being edited
    public void SetElf(float value)
    {
        genderAndRaceManager.elf = value;
        UpdateRaceSliders();
    }

    // Sets the muscle tone of the character being edited - first passed through animation curves
    public void SetMuscle(float value)
    {
        UpdateBlendshape(BlendKey.body_muscular_mid, muscleMidCurve.Evaluate(value));
        UpdateBlendshape(BlendKey.body_muscular_heavy, muscleHeavyCurve.Evaluate(value));
    }

    // Sets the weight of the character being edited - first passed through animation curves
    public void SetWeight(float value)
    {
        UpdateBlendshape(BlendKey.body_weight_thin, weightThinCurve.Evaluate(value));
        UpdateBlendshape(BlendKey.body_weight_thin_head, weightThinCurve.Evaluate(value));
        UpdateBlendshape(BlendKey.body_weight_heavy, weightHeavyCurve.Evaluate(value));
    }

    // Sets the bust size of the character being edited
    public void SetBust(float value)
    {
        UpdateBlendshape(BlendKey.iso_bust_large, value);
    }

    // Sets the cheek fullness of the character being edited
    public void SetCheeksGaunt(float value)
    {
        UpdateBlendshape(BlendKey.facial_cheeks_gaunt, value);
    }
    #endregion
}
