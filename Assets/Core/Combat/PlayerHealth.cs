using System.Collections;
using System;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] PlayerSFXHandler sfx;
    protected override void Die()
    {
        sfx.DeathSound();
        Instantiate(myEntity.myFx.deathParticle, transform.position, Quaternion.identity);
        Instantiate(myEntity.myFx.deathRemaining, transform.position, Quaternion.identity);
        base.Die();
    }

    public override void OnGetHit(int _damageAmount, int _knockDir)
    {
        if (myEntity.isKnockback) return;
        sfx.HurtSound();
        base.OnGetHit(_damageAmount, _knockDir);

    }
}
