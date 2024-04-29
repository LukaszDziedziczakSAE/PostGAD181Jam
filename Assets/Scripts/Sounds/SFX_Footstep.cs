using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Footstep : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip footstepClip;

    public void PlayFootstepSound()
    {
        audioSource.loop = false;
        audioSource.clip = footstepClip;
        audioSource.Play();
    }
}
