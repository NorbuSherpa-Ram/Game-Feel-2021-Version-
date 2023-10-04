using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health
{

    public static Action<int > OnEnemyDeath; // for score //
    [SerializeField] private int level = 1;
    [SerializeField] private int healthModifire;
    [SerializeField] private int scorePont=1;

    protected override void Start()
    {
        ModifieHealth();
        base.Start();

    }
    protected override void Die()
    {
        base.Die();
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
        base.OnGetHit(_damageAmount, _knockDir);
    }
}
