using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyMenu : MonoBehaviour
{
	enum BodyPartOptions{
        FullBody,
        Torso,
        Arms,
        Waist,
        Legs,
        None
	}

    public CharacterDropDown _dd_bodyPart;

    public CharacterSlider _slider_muscular;
    public CharacterSlider _slider_weight;
    public CharacterSlider _slider_bust;

    private CharacterSlider[] m_fullBodySliders;
    private CharacterSlider[] m_torsoSliders;
    private CharacterSlider[] m_armsSliders;
    private CharacterSlider[] m_waistSliders;
    private CharacterSlider[] m_legsSliders;

    private BodyPartOptions m_curBodyPart = BodyPartOptions.None;

    // Start is called before the first frame update
    void Start()
    {
        SortSliders();

        SetActiveSliders(m_fullBodySliders,false);
        SetActiveSliders(m_torsoSliders, false);
        SetActiveSliders(m_armsSliders, false);
        SetActiveSliders(m_waistSliders, false);
        SetActiveSliders(m_legsSliders, false);

        BodyPartUpdated(_dd_bodyPart.Value);

        _dd_bodyPart.AddListener(BodyPartUpdated);

        _slider_muscular.AddListener(UpdateModel);
        _slider_weight.AddListener(UpdateModel);
        _slider_bust.AddListener(UpdateModel);
    }

    private void SortSliders()
	{
        List<CharacterSlider> allSliders = new List<CharacterSlider>(GetComponentsInChildren<CharacterSlider>());

        //TODO: assign the other prefixes
        m_fullBodySliders = allSliders.Where(e => e.gameObject.name.StartsWith("FB")).ToArray();
        m_torsoSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Torso")).ToArray();
        m_armsSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Arms")).ToArray();
        m_waistSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Waist")).ToArray();
        m_legsSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Legs")).ToArray();
    }

	private void SetActiveSliders(CharacterSlider[] sliders, bool active)
	{
        foreach (CharacterSlider slider in sliders)
		{
            slider.gameObject.SetActive(active);
		}
    }

    #region LISTENERS_FUNCTIONS
    private void BodyPartUpdated(int i)
    {
        BodyPartOptions part = (BodyPartOptions)i;

        if (m_curBodyPart == part)
            return;

        switch (m_curBodyPart)
        {
            case BodyPartOptions.FullBody:
                SetActiveSliders(m_fullBodySliders, false);
                break;
            case BodyPartOptions.Torso:
                SetActiveSliders(m_torsoSliders, false);
                break;
            case BodyPartOptions.Arms:
                SetActiveSliders(m_armsSliders, false);
                break;
            case BodyPartOptions.Waist:
                SetActiveSliders(m_waistSliders, false);
                break;
            case BodyPartOptions.Legs:
                SetActiveSliders(m_legsSliders, false);
                break;
        }

        m_curBodyPart = part;

        switch (m_curBodyPart)
        {
            case BodyPartOptions.FullBody:
                SetActiveSliders(m_fullBodySliders, true);
                break;
            case BodyPartOptions.Torso:
                SetActiveSliders(m_torsoSliders, true);
                break;
            case BodyPartOptions.Arms:
                SetActiveSliders(m_armsSliders, true);
                break;
            case BodyPartOptions.Waist:
                SetActiveSliders(m_waistSliders, true);
                break;
            case BodyPartOptions.Legs:
                SetActiveSliders(m_legsSliders, true);
                break;
        }

    }

    void UpdateModel(float f)
    {
        BlendShapeCollection.Singleton.SetMultiBlendWeights("", "body_muscular_mid", "body_muscular_heavy", _slider_muscular.Value);

        BlendShapeCollection.Singleton.SetMultiBlendWeights("body_weight_thin", "", "body_weight_heavy", _slider_weight.Value);

        BlendShapeCollection.Singleton.SetMultiBlendWeights("iso_bust_small", "", "iso_bust_large", _slider_bust.Value);
    }

    #endregion
}
