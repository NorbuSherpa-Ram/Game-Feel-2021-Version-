using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Grenade : MonoBehaviour
{
    private Rigidbody2D myRb;
    private CinemachineImpulseSource myImpulsSource;

    [SerializeField] private GameObject explosionParticle;
    private Vector2 lunchDirection;
    private int beepCount = 3;
    [SerializeField] LayerMask enemyLayer;
    private Coroutine beepRoutine;

    // public static Action<Grenade > OnGrenadActivate;
    public static Action<Grenade> OnGrenadExplode;

    [SerializeField] SoundSO beepClip;
    [SerializeField] SoundSO explodeClip;

    private void OnEnable()
    {
        OnGrenadExplode += DamageEnemy;
    }
    private void OnDisable()
    {
        OnGrenadExplode -= DamageEnemy;
    }
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myImpulsSource = GetComponent<CinemachineImpulseSource>();
    }
    public void LaunchGrenade(Vector2 _playerPos , Vector2 mousePos, float _moveSpeed)
    {
        lunchDirection = (mousePos -_playerPos)  ;
        myRb.velocity = new Vector2(lunchDirection.normalized.x * _moveSpeed, lunchDirection.normalized.y * _moveSpeed);
        myRb.AddTorque(10,ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
     //   IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        IHitable damageable = other.gameObject.GetComponent<IHitable >();

        if (damageable != null)
        {
            Explode();
            return;
        }
        if (beepRoutine != null) return;
        beepRoutine = StartCoroutine(nameof(BeepSound));
    }
    private IEnumerator BeepSound()
    {
        while (beepCount > 0)
        {
            beepCount--;
            yield return new WaitForSeconds(.6f);
            GameAudioManager.instance.PlaySFX(null, beepClip, beepClip.volume, false);
        }
        Explode();
    }
    private void Explode()
    {
        OnGrenadExplode?.Invoke(this);
        myImpulsSource.GenerateImpulse();
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        GameAudioManager.instance.PlaySFX(null, explodeClip, explodeClip.volume, false);
    }
    private void DamageEnemy(Grenade _grenade)
    {
        if (_grenade != this) return;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 2.5f);
        foreach (Collider2D col in hit)
        {
            int dir = col.transform.position.x < transform.position.x ? -1 : 1;
            if (col.GetComponent<IHitable>() != null)
                col.GetComponent<IHitable>().OnGetHit(3, dir *4);
        }
        this.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position , 2.5f)
            ;
    }
}
