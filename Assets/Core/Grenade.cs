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
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

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
            AudioManager.instance.GrenadeBeepSound();
            yield return new WaitForSeconds(.5f);
        }
        Explode();
    }
    private void Explode()
    {
        OnGrenadExplode?.Invoke(this);
        myImpulsSource.GenerateImpulse();
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        AudioManager.instance.ExplosionSound();
    }
    private void DamageEnemy(Grenade _grenade)
    {
        if (_grenade != this) return;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 2, enemyLayer);
        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<IDamageable>().TakeDamage(3, Mathf.RoundToInt(lunchDirection.normalized.x));
            enemy.GetComponent<IHitable>().OnHit();
        }
        this.gameObject.SetActive(false);
        //  Destroy(this.gameObject)
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position , 2);
    }
}
