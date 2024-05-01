using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Footstep : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] walkingFootstepClips;
    [SerializeField] AudioClip[] runningFootstepClips;

    public void PlayWalkingFootstep()
    {
        if (walkingFootstepClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = walkingFootstepClips[Random.Range(0, walkingFootstepClips.Length)];
        audioSource.Play();
    }

    public void PlayRunningFootstep()
    {
        if (runningFootstepClips.Length == 0)
        {
            PlayWalkingFootstep();
            return;
        }
        audioSource.loop = false;
        audioSource.clip = runningFootstepClips[Random.Range(0, runningFootstepClips.Length)];
        audioSource.Play();
    }
}
