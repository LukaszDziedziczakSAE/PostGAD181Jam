using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Weapon : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fireSound;

    public void PlayerFireSFX()
    {
        audioSource.clip = fireSound;
        audioSource.Play();
    }
}
