using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] musicClips;

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicClips.Length == 0) return;
        audioSource.loop = true;
        audioSource.clip = musicClips[Random.Range(0, musicClips.Length)];
        audioSource.Play();
    }
}
