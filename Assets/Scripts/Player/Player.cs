using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMovement
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
    #endregion

    #region Shooting
    [SerializeField] float ShotCd = .3f;
    float currentShotCd = 0;

    [SerializeField] Transform shootingPoint;
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

    public virtual void Knockback()
    {
        if (isInKnockback)
        {
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
    }
    IEnumerator ResetTime(float t)
    {
        yield return new WaitForSeconds(t);

        isInKnockback = false;
        canMove = true;
    }

    private void Awake()
    {
        knockBack = new KnockBackStrategy(transform, knockbackForce);
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            Look();
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

        transform.position += velocity * Time.deltaTime;
        if (velocity == Vector3.zero)
            return;
        transform.forward = velocity;
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
            PlayerBasicBullet_Factory.instance.pool.GetObject().SetPosition(shootingPoint.position).SetRotation(transform.rotation);
            currentShotCd = 0;
        }
    }

    public void SetDamageDealer(Vector3 dmgDealer)
    {
        lastDamageDealer = dmgDealer;
    }

    public void SetKnockBackToTrue()
    {
        if(!isInKnockback) isInKnockback = true;
    }
}
