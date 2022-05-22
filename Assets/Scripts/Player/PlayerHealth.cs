using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<float,float> onLifeChanged = delegate { };

    [SerializeField] float maxHealth = 10;
    float currentHealth;

    [SerializeField] HealthBar healthBar;

    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        onLifeChanged(currentHealth, maxHealth);
    }
    public void TakeDamage(float dmg, Vector3 damageDealer)
    {
        //player.SetKnockBackToTrue();

        player.Knockback();

        currentHealth -= dmg;

        onLifeChanged(currentHealth, maxHealth);
        //healthBar.SetHealthBar(currentHealth, maxHealth);

        player.SetDamageDealer(damageDealer);

        if (currentHealth <= 0) player.Die();
    }
}
