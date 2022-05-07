using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    DirectedMovement directedMovement;

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
            if (SeePlayer())
            {
                if (directedMovement.GetDirectionToTarget().magnitude <= attackRange)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("attack", false);

                    LookAtPlayer();
                    Attack();
                }
                else if (directedMovement.GetDirectionToTarget().magnitude  <= sightRange)
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("attack", false);

                    directedMovement.Move();
                    LookAtPlayer();
                }
            }
            else anim.SetBool("isRunning", false);
        }
    }


    private void Attack()
    {
        anim.SetBool("attack", true);
    }
}
