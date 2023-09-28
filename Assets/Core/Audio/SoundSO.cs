using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ClipType
{
    SFX,
    Music
}

[CreateAssetMenu(fileName = "new Sound", menuName = "SO_Data/Audio")]
public class SoundSO : ScriptableObject
{

    public ClipType clipType;

    public AudioClip clip;
    public bool loop;
    public bool randomPitch;
    [Range(0, 1)] public float randomPitchRange=.1f;
    [Range(0f, 1f)] public float pitch;
    [Range(0f, 1f)] public float volume=.5f;
}
