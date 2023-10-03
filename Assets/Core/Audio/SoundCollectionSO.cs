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
    public SoundSO[] pickClip;
    public SoundSO[]  playerHurtClip;
    public SoundSO[]  megaKillClip;

    [Header("Luncher Info  ")]
    public SoundSO[] beepClip;
    public SoundSO[] launcherClip;
    public SoundSO[] explosionClip;
    public SoundSO[] breakableClip;
    public SoundSO[] breakableHitClip;
    public SoundSO[] enemyHitClip;
}
