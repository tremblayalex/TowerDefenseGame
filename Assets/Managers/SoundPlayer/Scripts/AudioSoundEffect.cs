using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSoundEffect", menuName = "ScriptableObjects/AudioSoundEffect", order = 1)]
public class AudioSoundEffect : ScriptableObject
{
    public AudioClip soundToPlay;
    public float volumeScale = 1f;
}
