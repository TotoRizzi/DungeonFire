using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMSlime : FSMEnemy
{
    [Header("Slime variables")]
    [SerializeField] private float _jumpForce;
    public float jumpCd;

    GroundCheck _groundCheck;
    Action _jumpFunction;
    public override void Awake()
    {
        base.Awake();

        _jumpFunction = Jump;
        _groundCheck = GetComponentInChildren<GroundCheck>();
        myMovement = new ForwardMovement(speed, this.transform, rb);

        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Jump));
        fsm.AddState(StateName.Jump, new JumpState(this, fsm, jumpCd, _jumpFunction, _groundCheck, StateName.Idle, true));
        fsm.ChangeState(StateName.Idle);
    }

    public  void Jump()
    {
        rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

}
