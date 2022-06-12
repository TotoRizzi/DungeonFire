using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<float,float> onLifeChanged = delegate { };

    public float maxHealth = 10;
    public float currentHealth;

    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        onLifeChanged(currentHealth, maxHealth);
    }
    public void TakeDamage(float dmg, Vector3 damageDealer)
    {
        player.Knockback();

        currentHealth -= dmg;
        GameManager.instance.SavePlayerHealth(currentHealth);
        ChangeHealthBar();

        player.SetDamageDealer(damageDealer);

        if (currentHealth <= 0) player.Die();
    }
    public void ChangeHealthBar()
    {
        onLifeChanged(currentHealth, maxHealth);

    }
}
