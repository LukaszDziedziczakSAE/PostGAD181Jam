using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Voice : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] dyingClips;
    [SerializeField] AudioClip[] hurtClips;
    [SerializeField] AudioClip[] introClips;
    [SerializeField] AudioClip[] dePoliceClips;

    public void PlayDyingSound()
    {
        if (dyingClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = dyingClips[Random.Range(0, dyingClips.Length)];
        audioSource.Play();
    }

    public void PlayHurtSound()
    {
        if (hurtClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = hurtClips[Random.Range(0, hurtClips.Length)];
        audioSource.Play();
    }

    public void PlayIntroSpeach()
    {
        if (introClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = introClips[Random.Range(0, introClips.Length)];
        audioSource.Play();
    }

    public void PlayDaPoliceClips()
    {
        if (dePoliceClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = dePoliceClips[Random.Range(0, dePoliceClips.Length)];
        audioSource.Play();
    }
}
