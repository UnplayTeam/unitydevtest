using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FaceMenu : MonoBehaviour
{
    enum FocusOptions
	{
        FullHead,
        Brow,
        Cheeks,
        Ears,
        Nose,
        Mouth,
        Jawbone,
        None
	}

    public CharacterDropDown _dd_focus;

    private CharacterSlider[] m_fullHeadSliders;
    private CharacterSlider[] m_browSliders;
    private CharacterSlider[] m_cheeksSliders;
    private CharacterSlider[] m_earsSliders;
    private CharacterSlider[] m_noseSliders;
    private CharacterSlider[] m_mouthSliders;
    private CharacterSlider[] m_jawboneSliders;

    private FocusOptions m_curFocus = FocusOptions.None;

    void Start()
    {
        SortSlideres();

		SetActiveSliders(m_fullHeadSliders, false);
		SetActiveSliders(m_browSliders, false);
		SetActiveSliders(m_cheeksSliders, false);
		SetActiveSliders(m_earsSliders, false);
		SetActiveSliders(m_noseSliders, false);
		SetActiveSliders(m_mouthSliders, false);
		SetActiveSliders(m_jawboneSliders, false);

        FocusUpdated(_dd_focus.Value);

		_dd_focus.AddListener(FocusUpdated);
    }

	private void SortSlideres()
	{
        List<CharacterSlider> allSliders = new List<CharacterSlider>(GetComponentsInChildren<CharacterSlider>());

        m_fullHeadSliders = allSliders.Where(e => e.gameObject.name.StartsWith("FH")).ToArray();
        m_browSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Brow")).ToArray();
        m_cheeksSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Cheeks")).ToArray();
        m_noseSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Nose")).ToArray();
        m_earsSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Ears")).ToArray();
        m_mouthSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Mouth")).ToArray();
        m_jawboneSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Jawbone")).ToArray();
    }

    private void SetActiveSliders(CharacterSlider[] sliders, bool active)
    {
        foreach (CharacterSlider slider in sliders)
        {
            slider.gameObject.SetActive(active);
        }
    }

    private void FocusUpdated(int i)
    {
        FocusOptions newFocus = (FocusOptions) i;

        if (m_curFocus == newFocus)
            return;

		switch (m_curFocus)
		{
			case FocusOptions.FullHead:
                SetActiveSliders(m_fullHeadSliders, false);
                break;
			case FocusOptions.Brow:
                SetActiveSliders(m_browSliders, false);
                break;
			case FocusOptions.Cheeks:
                SetActiveSliders(m_cheeksSliders, false);
                break;
			case FocusOptions.Ears:
                SetActiveSliders(m_earsSliders, false);
                break;
			case FocusOptions.Nose:
                SetActiveSliders(m_noseSliders, false);
				break;
            case FocusOptions.Mouth:
                SetActiveSliders(m_mouthSliders, false);
				break;
            case FocusOptions.Jawbone:
                SetActiveSliders(m_jawboneSliders, false);
				break;
        }

        m_curFocus = newFocus;

        switch (m_curFocus)
        {
            case FocusOptions.FullHead:
                SetActiveSliders(m_fullHeadSliders, true);
                break;
            case FocusOptions.Brow:
                SetActiveSliders(m_browSliders, true);
                break;
            case FocusOptions.Cheeks:
                SetActiveSliders(m_cheeksSliders, true);
                break;
            case FocusOptions.Ears:
                SetActiveSliders(m_earsSliders, true);
                break;
            case FocusOptions.Nose:
                SetActiveSliders(m_noseSliders, true);
                break;
            case FocusOptions.Mouth:
                SetActiveSliders(m_mouthSliders, true);
                break;
            case FocusOptions.Jawbone:
                SetActiveSliders(m_jawboneSliders, true);
                break;
        }
    }
}
