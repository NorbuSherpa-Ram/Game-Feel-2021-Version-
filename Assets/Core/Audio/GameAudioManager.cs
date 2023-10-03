using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{

    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource secondMusicSource;
    [SerializeField] SoundSO mainTheam;

    public static GameAudioManager instance;
    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        SetUpMainMusic();
    }
    private void SetUpMainMusic()
    {
        musicSource.clip  = mainTheam.clip ;
        musicSource.loop = mainTheam.loop;
        musicSource.volume  = mainTheam.volume ;
        musicSource.Play();
    }
    public void ChangeBGM(AudioSource _src, SoundSO _clip)
    {
        AudioSource source = _src ?? secondMusicSource;

        if (_clip != mainTheam)
            MuteMusic();
        else if (_clip == mainTheam)
        {
            UnMuteMusic();
            return;
        }
        PlayOtherBGM(_clip, source);
    }

    private void PlayOtherBGM(SoundSO _clip, AudioSource source)
    {
        source.clip = _clip.clip;
        source.volume = _clip.volume;
        source.loop = _clip.loop;
        source.Play();
        //Fnish and Start  main Sound 

        Invoke(nameof(UnMuteMusic), _clip.clip.length  );
    }

    private void UnMuteMusic()=> musicSource.mute  =false   ;
    private void MuteMusic() => musicSource.mute  =true   ;


    public void PlaySFX(AudioSource _source, SoundSO _clip, float _volume = 1, bool _randomPitch = false)
    {
        if (_clip == null) return;

        float pitch = _randomPitch ? _clip.GetRandomPitch() : 1;
        PlaySFXWithRanPitch(_source, _clip, _volume, pitch);
    }

    public void PlayRandomSFX(AudioSource _source, SoundSO[] _clip)
    {
        if (_clip.Length < 1) return;
        int clipIndex = Random.Range(0, _clip.Length);
        float pitch = _clip[clipIndex].randomPitch ? _clip[clipIndex].GetRandomPitch(): 1;
        PlaySFXWithRanPitch(_source, _clip[clipIndex], _clip[clipIndex].volume , pitch);
    }
    private void PlaySFXWithRanPitch(AudioSource _source, SoundSO _clip, float _volume, float pitch)
    {
        AudioSource source = _source ?? SFXSource;
        source.pitch = pitch;
        source.PlayOneShot(_clip.clip, _volume);
    }
}
