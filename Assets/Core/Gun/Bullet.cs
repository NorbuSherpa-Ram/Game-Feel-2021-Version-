using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject deathParticle;
    private float _moveSpeed;
    [SerializeField] private int _damageAmount = 1;

    private Vector2 _fireDirection;
    private Rigidbody2D _rigidBody;

    ObjectPooler pooler;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    public void SetUp(Vector2 _bullateSpownPosition , Vector2 _mousePos,float _moveSpeed)
    {
        this._moveSpeed = _moveSpeed;
        _fireDirection = (_mousePos-_bullateSpownPosition).normalized  ;
    }
    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(deathParticle, transform.position, Quaternion.identity);
        pooler.GetObjectFormPool(deathParticle, transform.position);

        IHitable hitable = other.GetComponent<IHitable>();
        hitable?.OnHit();

        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(_damageAmount, Mathf.RoundToInt(_fireDirection.x));

        this.gameObject.SetActive(false);
    }
}