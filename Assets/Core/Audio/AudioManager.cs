using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundCollectionSO soundCollectionSO;
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] AudioMixerGroup SFXAudioMixer;

    [SerializeField] private AudioSource currentSource;

    [SerializeField] GameObject audioSourcePrefab;
    [SerializeField] ObjectPooler pooler;


    #region UNITY_METHODS
    private void OnEnable()
    {
        Gun.OnShoot += GunShootSound;
        PlayerController.OnJump += JumpSound;
        Health.OnDeath  += DeathSound;
        DiscoballManager.OnDiscoballHit += PlayPartySound;
    }
    private void OnDisable()
    {
        Gun.OnShoot -= GunShootSound;
        PlayerController.OnJump -= JumpSound;
        Health.OnDeath -= DeathSound;
        DiscoballManager.OnDiscoballHit -= PlayPartySound;
    }
    private void Start()
    {
        PlayFightingSound();
    }

    #endregion 

    private void PlayRandomClip(SoundSO[] _clips)
    {
        int ranClip = 0;
        if (_clips.Length >0 && _clips != null)
        {
            PlayToSound(_clips[Random.Range(0, _clips.Length)]);
            ranClip = Random.Range(0, _clips.Length);
        }
    }
    private void PlayToSound(SoundSO _sound)
    {
        AudioClip clip = _sound.clip;
        float volume = _sound.volume;
        float pitch = _sound.pitch;
        bool loop = _sound.loop;
        pitch = CalculateRandomPitch(_sound, pitch);
        AudioMixerGroup mixer = SetAudioMixer(_sound);

        PlayClip(clip, volume, pitch, loop, mixer);
    }

    private  float CalculateRandomPitch(SoundSO _sound, float pitch)
    {
        if (_sound.randomPitch)
            pitch += Random.Range(-_sound.randomPitchRange, _sound.randomPitchRange);
        return pitch;
    }
    private AudioMixerGroup SetAudioMixer(SoundSO _sound)
    {
        if (_sound.clipType == ClipType.Music)
            return musicMixer;
        else if (_sound.clipType == ClipType.SFX)
            return SFXAudioMixer;
        return null;
    }

    private void PlayClip(AudioClip _clip, float _vol, float _pitch, bool _loop,AudioMixerGroup _mixer)
    {
        AudioSource source;

        if (_mixer == musicMixer)
        {
            if (currentSource != null)
            {
                currentSource.Stop();
            }
            source = currentSource;
        }
        else
        {
            //GameObject newSource = new GameObject(_clip.name);
            //source = newSource.AddComponent<AudioSource>();
          
            source = pooler.GetObjectFormPool(audioSourcePrefab, transform.position).GetComponent<AudioSource>();
            DeactivateObject deactive = source.gameObject.GetComponent<DeactivateObject>();
            if (deactive != null)
                deactive.SetLifeTime(_clip.length);
            else
                source.gameObject.AddComponent<DeactivateObject>().SetLifeTime(_clip.length);
        }

     
        //   AudioSource source = pooler.GetObjectFormPool(audioSourcePrefab, transform.position).GetComponent<AudioSource>();
        source.outputAudioMixerGroup = _mixer;
        source.clip = _clip;
        source.loop = _loop;
        source.pitch = _pitch;
        source.Play();

    //    if (!_loop) { Destroy(source.gameObject, _clip.length); }
    }

    #region PLAYING CLIP
    private void PlayFightingSound() => PlayRandomClip(soundCollectionSO.fightingClip);
    private void PlayPartySound()
    {
        PlayRandomClip(soundCollectionSO.partyClip);
        Invoke(nameof(PlayFightingSound), currentSource.clip.length);
    }
    private void GunShootSound() => PlayRandomClip(soundCollectionSO.attackClip);
    private void JumpSound() => PlayRandomClip(soundCollectionSO.jumpClip);
    private void DeathSound() => PlayRandomClip(soundCollectionSO.splatterClip);
    #endregion

}
