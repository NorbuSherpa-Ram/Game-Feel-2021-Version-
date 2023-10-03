using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health
{

    public static Action<int > OnEnemyDeath; // for score //
     public UnityEvent OnDeathDropItem;

    [SerializeField] private int level = 1;
    [SerializeField] private int healthModifire;
    [SerializeField] private int scorePont=1;


    [SerializeField] SoundSO[] hurtClip;
    [SerializeField] SoundSO[] deathClip;
    [SerializeField] AudioSource source;
    GameAudioManager audioManager;
    protected override void Start()
    {
        audioManager = GameAudioManager.instance;
        ModifieHealth();
        base.Start();

    }
    protected override void Die()
    {
        audioManager.PlayRandomSFX(null , deathClip);
        base.Die();
        OnDeathDropItem?.Invoke();
        OnEnemyDeath?.Invoke(scorePont);
        myEntity.myFx.SplatParticle();
        myEntity.myFx.SplatterEffect();
    }
    void ModifieHealth()
    {
        for (int i = 0; i < level; i++)
        {
            _startingHealth += healthModifire;
        }
    }

    public override void OnGetHit(int _damageAmount, int _knockDir)
    {
        audioManager.PlayRandomSFX(source , hurtClip);
        base.OnGetHit(_damageAmount, _knockDir);
    }
}
