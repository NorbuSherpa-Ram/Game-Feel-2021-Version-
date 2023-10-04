using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SFX : MonoBehaviour
{
    [SerializeField] private SoundSO[] hurtClip;
    [SerializeField] private SoundSO[] deathClip;

    private Health myHealth;
    private GameAudioManager audioManager;

    [SerializeField] AudioSource source;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        audioManager = GameAudioManager.instance ;
    }
    private void OnEnable()
    {
        myHealth.OnHurt += HurtSound;
        myHealth.OnDeath  += DeathSound;
    }
    private void OnDisable()
    {
        myHealth.OnHurt -= HurtSound;
        myHealth.OnDeath  -= DeathSound;
    }
    public void HurtSound() => audioManager.PlayRandomSFX(source, hurtClip);
    public void DeathSound() =>audioManager.PlayRandomSFX(null, deathClip);
}
