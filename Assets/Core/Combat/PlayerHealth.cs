using System.Collections;
using System;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Instantiate(myEntity.myFx.deathParticle, transform.position, Quaternion.identity);
        Instantiate(myEntity.myFx.deathRemaining, transform.position, Quaternion.identity);
        base.Die();
    }
    public override void OnGetHit(int _damageAmount, int _knockDir)
    {
        if (myEntity.isKnockback) return;
        base.OnGetHit(_damageAmount, _knockDir);

    }
    private void OnDestroy()
    {
        FindObjectOfType<Respawn_Player>().RespawnPlayer();
    }
}
