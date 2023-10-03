using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Sound", menuName = "SO_Data/Audio")]
public class SoundSO : ScriptableObject
{
    public AudioClip clip;
    public bool loop;
    public bool randomPitch;
    [SerializeField] [Range(0, 1)] private float minPitch;
    [SerializeField] [Range(1, 3)] private float maxPitch;
    [Range(0f, 1f)] public float volume = .5f;

    public float GetRandomPitch()
    {
        float pitch = Random.Range(minPitch, maxPitch);
        return pitch;
    }
}
