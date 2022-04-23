using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy , IShoot , IKnockback
{
    protected DirectedMovement directedMovement;

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
            if (directedMovement.GetDirectionToTarget().magnitude <= shootRange)
            {
                Shoot();
                LookAtTarget();
            }
            else if (directedMovement.GetDirectionToTarget().magnitude <= sightRange)
            {
                directedMovement.Move();
                LookAtTarget();
                currentShootCd = 0;
            }
        }
        
    }

    public void Shoot()
    {
        currentShootCd += Time.deltaTime;

        if (currentShootCd >= maxShootCd)
        {
            //Crea las balas llamandolas al factory

            var b = ArcherBullet_Factory.instance.pool.GetObject();
            b.transform.position = shootingPoint.position;
            b.transform.rotation = transform.rotation;
            currentShootCd = 0;
        }
    }

    void LookAtTarget()
    {
        transform.forward = directedMovement.GetDirectionToTarget();
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }
}
