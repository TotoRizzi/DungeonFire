using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy , IShoot , IKnockback
{
    DirectedMovement directedMovement;

    #region ShootVariables IShoot
    [SerializeField] float maxShootCd;
    float currentShootCd;
    #endregion

    Animator anim;

    public override void Awake()
    {
        base.Awake();
        directedMovement = new DirectedMovement(speed, transform, player.transform);
        anim = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (canMove)
        {
            Vector3 vectorToPlayer = (player.transform.position - transform.position);

            if (SeePlayer())
            {
                if (vectorToPlayer.magnitude <= shootRange)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("attack", false);

                    LookAtPlayer();
                    Shoot();
                }
                else if (vectorToPlayer.magnitude <= sightRange)
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("attack", false);

                    directedMovement.Move();
                    LookAtPlayer();
                    currentShootCd = 0;
                }
            }
            else anim.SetBool("isRunning", false);
        }
         
    }

    public void Shoot()
    {
        currentShootCd += Time.deltaTime;

        if (currentShootCd >= maxShootCd)
        {
            anim.SetBool("attack", true);
            currentShootCd = 0;
        }
    }
}
