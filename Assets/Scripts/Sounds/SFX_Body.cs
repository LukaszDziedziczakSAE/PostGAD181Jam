using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Body : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] hurtClips;

    public void PlayHurtSound()
    {
        if (hurtClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = hurtClips[Random.Range(0, hurtClips.Length)];
        audioSource.Play();
    }
}
