using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour, IHitable
{
    [SerializeField] protected int _startingHealth = 3;
    [SerializeField] protected Entity myEntity;
    public int _currentHealth { get; private set; }

    public event Action OnHurt;
    public event Action OnDeath;

    protected virtual void Start()
    {

        myEntity = GetComponent<Entity>();
        ResetHealth();
    }

    protected  void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    protected  virtual  void DoDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }
        OnHurt?.Invoke();
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject,0.1f);
    }
    public virtual  void OnGetHit(int _damageAmount , int _knockDir)
    {
        myEntity.myFx.Flash();
        myEntity.Knockback(_knockDir);
        DoDamage(_damageAmount);
    }
    public string GetCurrentHealthString() => _currentHealth.ToString();
}
