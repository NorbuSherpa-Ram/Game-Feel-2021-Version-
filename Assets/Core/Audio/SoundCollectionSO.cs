using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Collection", menuName = "SO_Data/Audio_Collection")]
public class SoundCollectionSO : ScriptableObject
{
    [Header ("Music Data  ")]
    public SoundSO[] fightingClip;
    public SoundSO[] partyClip;

    [Header("SFX Data  ")]
    public SoundSO[] attackClip;
    public SoundSO[] jumpClip;
    public SoundSO[] splatterClip;
}
