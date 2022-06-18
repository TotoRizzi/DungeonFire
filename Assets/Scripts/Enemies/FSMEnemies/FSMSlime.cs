using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMSlime : FSMEnemy
{
    [Header("Slime variables")]
    public float jumpCd;

    GroundCheck _groundCheck;
    Action _jumpFunction;
    public override void Awake()
    {
        base.Awake();

        _jumpFunction = Jump;
        _groundCheck = GetComponentInChildren<GroundCheck>();

        myMovement = new ForwardMovement(FlyweightPointer.Slime.speed, this.transform, rb);
        enemyHealth = new FSMEnemyHealth(FlyweightPointer.Slime.maxHealth, this);

        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Jump));
        fsm.AddState(StateName.Jump, new JumpState(this, fsm, jumpCd, _jumpFunction, _groundCheck, StateName.Idle, true));
        fsm.ChangeState(StateName.Idle);
    }
    public override void Start()
    {
        base.Start();
        ponitsToGive = Mathf.RoundToInt(FlyweightPointer.Slime.maxHealth);
    }

    public  void Jump()
    {
        rb.AddForce(Vector3.up * FlyweightPointer.Slime.jumpForce, ForceMode.Impulse);
    }

}
