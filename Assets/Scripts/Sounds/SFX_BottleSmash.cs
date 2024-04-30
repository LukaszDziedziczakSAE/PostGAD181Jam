using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_BottleSmash : MonoBehaviour
{
    [SerializeField] AudioClip[] BottleSmashClip;
    [SerializeField] AudioSource AudioSource;



    private void Start()
    {
        if (BottleSmashClip.Length == 0) return;
        PlaySmashClip();
    }
    public void PlaySmashClip()
    {
        int random = Random.Range(0, BottleSmashClip.Length);
        AudioSource.clip = BottleSmashClip[random];
        AudioSource.Play();

    }
}
