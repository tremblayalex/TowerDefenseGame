using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    int playing;
    public int maximumSound = 20;
    public 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*
    public void PlaySound(AudioSoundEffect audioSoundEffect)
    {
        audioSource.PlayOneShot(audioSoundEffect.soundToPlay, audioSoundEffect.volumeScale);  
    }
    */
    public void PlaySound(AudioSoundEffect audioSoundEffect)
    {
        if (playing > maximumSound) return;
        StartCoroutine(Playclip(audioSoundEffect));
    }
    IEnumerator Playclip(AudioSoundEffect audioSoundEffect)
    {
        playing++;
        audioSource.PlayOneShot(audioSoundEffect.soundToPlay, audioSoundEffect.volumeScale);
        yield return new WaitForSeconds(audioSoundEffect.soundToPlay.length);
        playing--;
    }
}
