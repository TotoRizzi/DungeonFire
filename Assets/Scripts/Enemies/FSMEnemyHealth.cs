using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMEnemyHealth
{
    private float _maxHealth;
    private float _currentHealth;

    FSMEnemy _thisEnemy;

    public FSMEnemyHealth(float maxhealth, FSMEnemy thisEnemy)
    {
        _maxHealth = maxhealth;
        _currentHealth = _maxHealth;
        _thisEnemy = thisEnemy;
    }
    public void TakeDamage(float dmg, Vector3 damageDealer)
    {
        _currentHealth -= dmg;
        _thisEnemy.Knockback(damageDealer);

        if (_currentHealth <= 0) _thisEnemy.Die();
    }
}
