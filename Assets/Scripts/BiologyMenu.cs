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

    public CharacterDropDown _DD_Race;

    public CharacterSlider _Slider_Human;
    public CharacterSlider _Slider_Orc;
    public CharacterSlider _Slider_Elf;

    public CharacterSlider _Slider_Sex;

    private float m_race_orc = 0;
    private float m_race_elf = 0;

    private float m_femininity = 0;

    private bool m_adjusting = false;


    // Start is called before the first frame update
    void Start()
    {
        _DD_Race.AddListener(UpdateRaceDD);

        _Slider_Human.AddListener(UpdateHumanSlider);
        _Slider_Orc.AddListener(UpdateOrcSlider);
        _Slider_Elf.AddListener(UpdateElfSlider);

        _Slider_Sex.AddListener(UpdateSexSlider);
    }

	private void OnDestroy()
	{
        _DD_Race.RemoveListener(UpdateRaceDD);

        _Slider_Human.RemoveListener(UpdateHumanSlider);
        _Slider_Orc.RemoveListener(UpdateOrcSlider);
        _Slider_Elf.RemoveListener(UpdateElfSlider);
        _Slider_Sex.RemoveListener(UpdateSexSlider);
    }

	private void UpdateRaceDD(int value)
	{
        bool mixed = value == (int)RaceOptions.MixedHeritage;

        _Slider_Human.gameObject.SetActive(mixed);
        _Slider_Orc.gameObject.SetActive(mixed);
        _Slider_Elf.gameObject.SetActive(mixed);

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
                _Slider_Human.Value = CalculateHuman();
                _Slider_Orc.Value = m_race_orc;
                _Slider_Elf.Value = m_race_elf;
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
            AdjustOtherRaceSliders(_Slider_Human, diff);
            UpdateModel();
        }
	}
    
    private void UpdateOrcSlider(float value)
	{
        float diff = value - m_race_orc;

        m_race_orc = value;

        if (!m_adjusting)
        {
            AdjustOtherRaceSliders(_Slider_Orc, diff);
            UpdateModel();
        }
    }
    
    private void UpdateElfSlider(float value)
	{
        float diff = value - m_race_elf;

        m_race_elf = value;

        if (!m_adjusting)
        {
            AdjustOtherRaceSliders(_Slider_Elf, diff);
            UpdateModel();
        }
    }

    private void AdjustOtherRaceSliders(CharacterSlider ignore, float difference)
	{
        m_adjusting = true;

        List<CharacterSlider> otherRaces = new List<CharacterSlider> { _Slider_Human , _Slider_Orc, _Slider_Elf };

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

        BlendShapeCollection.Singleton.SetWeight("species_orc_fem", m_femininity * m_race_orc);
        BlendShapeCollection.Singleton.SetWeight("species_orc_masc", (1-m_femininity) * m_race_orc);

        BlendShapeCollection.Singleton.SetWeight("species_elf_fem", m_femininity * m_race_elf);
        BlendShapeCollection.Singleton.SetWeight("species_elf_masc", (1-m_femininity) * m_race_elf);
	}

    private float CalculateHuman()
	{
        return 1.0f - m_race_orc - m_race_elf; 
	}
}
