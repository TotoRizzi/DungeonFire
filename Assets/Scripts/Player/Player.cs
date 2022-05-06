using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMovement, IDie
{
    #region Movement
    [SerializeField] Controller moveController;
    [SerializeField] Controller lookAndFireController;

    Vector3 dirToMove;
    Vector3 dirToLook;
    Vector3 velocity;

    [SerializeField] float maxSpeed;
    [SerializeField] float maxMovingForce;

    bool canMove = true;
    bool isDead;
    #endregion

    #region Shooting
    [SerializeField] float ShotCd = .3f;
    float currentShotCd;

    #endregion

    #region KnockBack

    KnockBackStrategy knockBack;

    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;
    [SerializeField] float resetTime;
    float currentKnockbackDuration;

    Vector3 lastDamageDealer;

    bool isInKnockback;

    #endregion

    [SerializeField] Animator anim;


    private void Awake()
    {
        knockBack = new KnockBackStrategy(transform, knockbackForce);
        currentShotCd = ShotCd;
    }

    private void Update()
    {
        if (canMove)
        {
            Debug.Log("CanMove");
            Move();
            Look();
        }
        else
        {
            Debug.Log("Cant Move");
        }
        Knockback();
    }

    public void Move()
    {

        dirToMove = moveController.GetDir();
        dirToMove.Normalize();
        dirToMove *= maxSpeed;

        Vector3 steering = dirToMove - velocity;
        steering = Vector3.ClampMagnitude(steering, maxMovingForce);
        ApplyForce(steering);

        if (velocity == Vector3.zero)
        {
            anim.SetBool("isRunning", false);
            return;
        }
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity;

        anim.SetBool("isRunning", true);
    }

    private void Look()
    {
        if (lookAndFireController.GetDir() != Vector3.zero)
        {
            dirToLook = lookAndFireController.GetDir();

            transform.forward = dirToLook;

            Shoot();
        }
        else
        {
            dirToLook = moveController.GetDir();
        }
    }

    private void ApplyForce(Vector3 force)
    {
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    private void Shoot()
    {
        currentShotCd += Time.deltaTime;

        if (currentShotCd >= ShotCd)
        {
            anim.SetBool("attack", true);
            currentShotCd = 0;
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    public void SetDamageDealer(Vector3 dmgDealer)
    {
        lastDamageDealer = dmgDealer;
    }

    public void SetKnockBackToTrue()
    {
        if (!isInKnockback) isInKnockback = true;
    }

    public void Die()
    {
        anim.SetTrigger("die");
        canMove = false;
        isDead = true;
    }
    public void Revive()
    {
        isDead = false;
        canMove = true;
        anim.SetTrigger("revive");
    }
    public virtual void Knockback()
    {
        if (isInKnockback)
        {
            anim.SetBool("hit", true);

            if (currentKnockbackDuration < knockbackDuration)
            {
                currentKnockbackDuration += Time.deltaTime;
                knockBack.SetVariables(lastDamageDealer);
                knockBack.Move();
                canMove = false;
            }
            else
            {
                currentKnockbackDuration = 0;

                StartCoroutine(ResetTime(resetTime));
            }
        }
        else anim.SetBool("hit", false);
    }
    IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        isInKnockback = false;
        if(!isDead) canMove = true;
    }
}