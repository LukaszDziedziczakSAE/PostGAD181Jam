using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_UI : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip[] pickUpClips;

    public void PlayPickUpSound()
    {
        if (pickUpClips.Length == 0) return;
        audioSource.loop = false;
        audioSource.clip = pickUpClips[Random.Range(0, pickUpClips.Length)];
        audioSource.Play();
    }
}
