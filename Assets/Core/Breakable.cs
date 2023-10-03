using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Breakable : MonoBehaviour,IHitable
{
    [SerializeField] public int health = 3;
    [SerializeField] Collider2D myCollider;
    [SerializeField] SpriteRenderer mySr;
    [SerializeField] Rigidbody2D myRb;

    [SerializeField] GameObject  breakePice;
    [SerializeField] Rigidbody2D[] pices ;

    [SerializeField] SoundSO hitClip;
    [SerializeField] SoundSO breakeClip;
    private void Start()
    {
        pices = breakePice.GetComponentsInChildren<Rigidbody2D>();
    }

    public void OnGetHit(int _dmageAmount, int _knockDir)
    {
        health -= _dmageAmount;
        if(health <= 0)
        {
            int force = _dmageAmount < 3 ? 20 : 50;
            mySr.enabled = false;
            myCollider.enabled = false;
            myRb.bodyType = RigidbodyType2D.Static;
            breakePice.SetActive(true);
            ApplyForce(force);
            GameAudioManager.instance.PlaySFX(null, breakeClip, breakeClip.volume, false);
            return;
        }
        GameAudioManager.instance.PlaySFX(null, hitClip, hitClip.volume, false);

    }

    private void ApplyForce(int force)
    {
        foreach (Rigidbody2D item in pices)
        {
            int ranX = UnityEngine.Random.Range(-force, force);
            int ranY = UnityEngine.Random.Range(15, 20);
            Vector2 newForce = new Vector2(ranX, ranY);
            item.AddForce(newForce, ForceMode2D.Impulse);
            item.AddTorque(1000);
        }
    }
}
