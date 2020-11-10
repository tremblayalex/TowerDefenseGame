using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioSoundEffect audioSoundEffect)
    {
        audioSource.PlayOneShot(audioSoundEffect.soundToPlay, audioSoundEffect.volumeScale);  
    }
}
