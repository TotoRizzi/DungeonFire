using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IDie
{
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
        healthBar.SetHealthBar(currentHealth, maxHealth);
    }
    public void TakeDamage(float dmg, Vector3 damageDealer)
    {
        player.SetKnockBackToTrue();

        currentHealth -= dmg;

        healthBar.SetHealthBar(currentHealth, maxHealth);

        player.SetDamageDealer(damageDealer);

        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("MORISTE");
    }
}
