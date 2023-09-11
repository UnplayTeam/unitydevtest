using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    private bool sfxEnabled = true;

    public void ToggleMusic(bool isOn)
    {
        musicSource.enabled = isOn;
    }

    public void ToggleSFX(bool isOn)
    {
        sfxEnabled = isOn;
    }

    public void PlayClip(AudioClip clip)
    {
        if (!sfxEnabled) return;

        sfxSource.PlayOneShot(clip);
    }
}
