using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action OnDeath;
    [SerializeField] private int _startingHealth = 3;
    [SerializeField ] private Entity myEntity;
    private int _currentHealth;

    private void Start() {

        myEntity = GetComponent<Entity>();
        ResetHealth();
    }

    public void ResetHealth() {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int amount) {
        _currentHealth -= amount;

        if (_currentHealth <= 0) {
            myEntity.DeathEffect();
           OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
