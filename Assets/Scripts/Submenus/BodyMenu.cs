using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyMenu : MonoBehaviour
{
	enum FocusOptions{
        FullBody,
        Torso,
        Arms,
        Legs,
        None
	}

    public CharacterDropDown _dd_focus;

    public CameraTarget _fullBodyTarget;
    public CameraTarget _torsoTarget;
    public CameraTarget _armsTarget;
    public CameraTarget _legsTarget;

    private CharacterSlider[] m_fullBodySliders;
    private CharacterSlider[] m_torsoSliders;
    private CharacterSlider[] m_armsSliders;
    private CharacterSlider[] m_legsSliders;

    private FocusOptions m_curFocus = FocusOptions.None;

    // Start is called before the first frame update
    void Start()
    {
        SortSliders();

        SetActiveSliders(m_fullBodySliders,false);
        SetActiveSliders(m_torsoSliders, false);
        SetActiveSliders(m_armsSliders, false);
        SetActiveSliders(m_legsSliders, false);

        FocusUpdated(_dd_focus.Value);

        _dd_focus.AddListener(FocusUpdated);
    }

	private void SortSliders()
	{
        List<CharacterSlider> allSliders = new List<CharacterSlider>(GetComponentsInChildren<CharacterSlider>());

        m_fullBodySliders = allSliders.Where(e => e.gameObject.name.StartsWith("FB")).ToArray();
        m_torsoSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Torso")).ToArray();
        m_armsSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Arms")).ToArray();
        m_legsSliders = allSliders.Where(e => e.gameObject.name.StartsWith("Legs")).ToArray();
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
        FocusOptions part = (FocusOptions)i;

        if (m_curFocus == part)
            return;

        switch (m_curFocus)
        {
            case FocusOptions.FullBody:
                SetActiveSliders(m_fullBodySliders, false);
                break;
            case FocusOptions.Torso:
                SetActiveSliders(m_torsoSliders, false);
                break;
            case FocusOptions.Arms:
                SetActiveSliders(m_armsSliders, false);
                break;
            case FocusOptions.Legs:
                SetActiveSliders(m_legsSliders, false);
                break;
        }

        m_curFocus = part;

        switch (m_curFocus)
        {
            case FocusOptions.FullBody:
                SetActiveSliders(m_fullBodySliders, true);
                break;
            case FocusOptions.Torso:
                SetActiveSliders(m_torsoSliders, true);
                break;
            case FocusOptions.Arms:
                SetActiveSliders(m_armsSliders, true);
                break;
            case FocusOptions.Legs:
                SetActiveSliders(m_legsSliders, true);
                break;
        }

        ApplyCameraTarget();
    }

    public void ApplyCameraTarget()
	{
        switch (m_curFocus)
        {
            case FocusOptions.FullBody:
                CharacterCameraController.SetCameraTarget(_fullBodyTarget);
                break;
            case FocusOptions.Torso:
                CharacterCameraController.SetCameraTarget(_torsoTarget);
                break;
            case FocusOptions.Arms:
                CharacterCameraController.SetCameraTarget(_armsTarget);
                break;
            case FocusOptions.Legs:
                CharacterCameraController.SetCameraTarget(_legsTarget);
                break;
        }
    }
}