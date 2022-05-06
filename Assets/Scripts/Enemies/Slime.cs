using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCd;
    private float currentJumpCd;

    [SerializeField] Collider enemyAttack;

    [SerializeField] private GroundCheck groundCheck;
    DirectedMovement directedMovement;
    Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        directedMovement = new DirectedMovement(speed, transform, player.transform);
        rb = GetComponent<Rigidbody>();
    }
    public override void Update()
    {
        base.Update();

        if (canMove)
        {
            if (SeePlayer())
            {
                LookAtPlayer();

                if (groundCheck.isGrounded)
                {
                    enemyAttack.enabled = false;

                    currentJumpCd += Time.deltaTime;

                    if(currentJumpCd >= jumpCd)
                    {
                        Jump();
                        currentJumpCd = 0;
                    }
                }
                else
                {
                    directedMovement.Move();
                    enemyAttack.enabled = true;
                }
            }
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        directedMovement.Move();
    }
}
