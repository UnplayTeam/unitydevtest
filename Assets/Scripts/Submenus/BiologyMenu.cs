using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiologyMenu : MonoBehaviour
{

    enum RaceOptions
	{
        Human,
        Orc,
        Elf,
        MixedHeritage
	}

    public CharacterDropDown _dd_race;

    public CharacterSlider _slider_human;
    public CharacterSlider _slider_orc;
    public CharacterSlider _slider_elf;

    public CharacterSlider _slider_sex;

    public CameraTarget _cameraTarget;

    private float m_race_orc = 0;
    private float m_race_elf = 0;

    private float m_femininity = 0;

    private bool m_adjusting = false;


    // Start is called before the first frame update
    void Start()
    {
        _dd_race.AddListener(UpdateRaceDD);

        _slider_human.AddListener(UpdateHumanSlider);
        _slider_orc.AddListener(UpdateOrcSlider);
        _slider_elf.AddListener(UpdateElfSlider);

        _slider_sex.AddListener(UpdateSexSlider);
    }

	private void OnDestroy()
	{
        _dd_race.RemoveListener(UpdateRaceDD);

        _slider_human.RemoveListener(UpdateHumanSlider);
        _slider_orc.RemoveListener(UpdateOrcSlider);
        _slider_elf.RemoveListener(UpdateElfSlider);
        _slider_sex.RemoveListener(UpdateSexSlider);
    }

	private void UpdateRaceDD(int value)
	{
        bool mixed = value == (int)RaceOptions.MixedHeritage;

        _slider_human.gameObject.SetActive(mixed);
        _slider_orc.gameObject.SetActive(mixed);
        _slider_elf.gameObject.SetActive(mixed);

		switch ((RaceOptions)value)
		{
            case RaceOptions.Human:
                m_race_orc = 0;
                m_race_elf = 0;
                break;
            case RaceOptions.Orc:
                m_race_orc = 1;
                m_race_elf = 0;
                break;
            case RaceOptions.Elf:
                m_race_orc = 0;
                m_race_elf = 1;
                break;
            case RaceOptions.MixedHeritage:
                m_adjusting = true;
                _slider_human.Value = CalculateHuman();
                _slider_orc.Value = m_race_orc;
                _slider_elf.Value = m_race_elf;
                m_adjusting = false;
                break;
		}

        UpdateModel();
    }

    private void UpdateHumanSlider(float value)
	{
        float diff = value - CalculateHuman();

        if (!m_adjusting)
        {
            AdjustOtherRaceSliders(_slider_human, diff);
            UpdateModel();
        }
	}
    
    private void UpdateOrcSlider(float value)
	{
        float diff = value - m_race_orc;

        m_race_orc = value;

        if (!m_adjusting)
        {
            AdjustOtherRaceSliders(_slider_orc, diff);
            UpdateModel();
        }
    }
    
    private void UpdateElfSlider(float value)
	{
        float diff = value - m_race_elf;

        m_race_elf = value;

        if (!m_adjusting)
        {
            AdjustOtherRaceSliders(_slider_elf, diff);
            UpdateModel();
        }
    }

    private void AdjustOtherRaceSliders(CharacterSlider ignore, float difference)
	{
        m_adjusting = true;

        List<CharacterSlider> otherRaces = new List<CharacterSlider> { _slider_human , _slider_orc, _slider_elf };

        otherRaces.Remove(ignore);

        AdjustOtherRaceSliders(otherRaces, difference);
        m_adjusting = false;
    }

    private void AdjustOtherRaceSliders(List<CharacterSlider> otherRaces, float difference)
    {
        m_adjusting = true;
        var nonEmptyRaces = otherRaces.FindAll(e => e.Value > 0);

        if (nonEmptyRaces.Count > 0)
            otherRaces = nonEmptyRaces;

        difference /= otherRaces.Count;

        float lowest = otherRaces.Min(e => e.Value - difference);
        float remainder = 0;

        if(lowest < 0)
		{
            remainder = -lowest * otherRaces.Count;
            difference += lowest;
		}

        for(int i = 0; i < otherRaces.Count; i++)
		{
            var race = otherRaces[i];

            race.Value -= difference;
            if (race.Value == 0)
            {
                nonEmptyRaces.Remove(race);
                i--;
            }
		}

        if (remainder > 0 && otherRaces.Count > 0)
            AdjustOtherRaceSliders(otherRaces,remainder);
        m_adjusting = false;
    }

    private void UpdateSexSlider(float value)
    {
        m_femininity = value;

        UpdateModel();
    }

    void UpdateModel()
	{
        BlendShapeCollection.Singleton.SetWeight("gender_fem", m_femininity);

        BlendShapeCollection.Singleton.SetWeight("facial_teeth_canine_bot", m_race_orc);
        BlendShapeCollection.Singleton.SetMultiBlendWeights("", "body_muscular_mid", "body_muscular_heavy", m_race_orc);
        BlendShapeCollection.Singleton.SetWeight("species_orc_fem", m_femininity * m_race_orc);
        BlendShapeCollection.Singleton.SetWeight("species_orc_masc", (1-m_femininity) * m_race_orc);

        BlendShapeCollection.Singleton.SetMultiBlendWeights("body_weight_heavy", "","body_weight_thin", Mathf.Max(m_race_elf * 0.8f,0.5f));
        BlendShapeCollection.Singleton.SetWeight("species_elf_fem", m_femininity * m_race_elf);
        BlendShapeCollection.Singleton.SetWeight("species_elf_masc", (1-m_femininity) * m_race_elf);
	}

	public void ApplyCameraTarget()
	{
        CharacterCameraController.Singleton.SetCameraTarget(_cameraTarget);
	}

    private float CalculateHuman()
	{
        return 1.0f - m_race_orc - m_race_elf; 
	}
}
