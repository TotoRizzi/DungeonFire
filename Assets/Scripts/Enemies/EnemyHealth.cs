using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IDie
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] Enemy thisEnemy;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(float dmg, Vector3 damageDealer)
    {
        currentHealth -= dmg;


        thisEnemy.SetKnockBackToTrue();
        thisEnemy.SetDamageDealer(damageDealer);

        if (currentHealth <= 0) Die();
    }
    public virtual void Die()
    {
        Destroy(thisEnemy.gameObject);
    }
}
