using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour , IDamageable, IDie
{
    protected Player player;

    [SerializeField] protected float maxHealth;
    protected float currentHealth;

    protected bool canMove = true;
    
    #region MovementVariables

    [SerializeField] protected float speed;
    [SerializeField] protected float sightRange;
    [SerializeField] protected float shootRange;
    #endregion

    #region KnockbackVariables

    [SerializeField] protected float knockbackForce;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected float resetTime;
    
    protected float currentKnockbackDuration;
    protected DirectedMovement KnockBack;
    protected bool isInKnockback;

    #endregion

    public virtual void Awake()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<Player>();
        KnockBack = new DirectedMovement(-knockbackForce, transform, player.transform);
    }
    
    public virtual void Update()
    {
        Knockback();
    }
    public virtual void Die()
    {
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        isInKnockback = true;

        if (currentHealth <= 0) Die();
    }

    public virtual void Knockback()
    {
        if (isInKnockback)
        {
            if (currentKnockbackDuration < knockbackDuration)
            {
                currentKnockbackDuration += Time.deltaTime;
                KnockBack.Move();
                canMove = false;
            }
            else
            {
                isInKnockback = false;
                currentKnockbackDuration = 0;

                StartCoroutine(ResetTime(resetTime));
            }
        }
    }
    IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        canMove = true;
    }


}
