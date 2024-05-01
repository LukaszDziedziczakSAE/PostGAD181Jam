using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] fireClips;

    public void PlayFiringSound()
    {
        if (fireClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = fireClips[Random.Range(0, fireClips.Length)];
        audioSource.Play();
    }

}
