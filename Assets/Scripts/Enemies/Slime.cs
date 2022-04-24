using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    //LA RECALCADA CONCHA DE TU REPUTISIMA MADRE

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCd;
    private float currentJumpCd;

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
        LookAtPlayer();

        if (groundCheck.isGrounded)
        {
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
        }
        
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        directedMovement.Move();
    }
}
