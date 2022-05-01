using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy , IShoot , IKnockback
{
    DirectedMovement directedMovement;

    #region ShootVariables IShoot
    [SerializeField] float maxShootCd;
    float currentShootCd;
    [SerializeField] Transform shootingPoint;
    #endregion

    public override void Awake()
    {
        base.Awake();
        directedMovement = new DirectedMovement(speed, transform, player.transform);
    }

    public override void Update()
    {
        base.Update();

        if (canMove)
        {
            Vector3 vectorToPlayer = (player.transform.position - transform.position);

            if (SeePlayer(shootingPoint.position))
            {
                if (vectorToPlayer.magnitude <= shootRange)
                {
                    LookAtPlayer();
                    Shoot();
                }
                else if (vectorToPlayer.magnitude <= sightRange)
                {
                    directedMovement.Move();
                    LookAtPlayer();
                    currentShootCd = 0;
                }
            }
        }
    }

    public void Shoot()
    {
        currentShootCd += Time.deltaTime;

        if (currentShootCd >= maxShootCd)
        {
            ArcherBullet_Factory.instance.pool.GetObject().SetPosition(shootingPoint.position).SetRotation(transform.rotation);
            currentShootCd = 0;
        }
    }
}
