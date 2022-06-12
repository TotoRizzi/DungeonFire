using UnityEngine;
using System;

public class FSMBladeBlade : FSMEnemy
{
    [Header("BladeBlade variables")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float jumpCd;
    [SerializeField] private float _attackRange;
    [SerializeField] BoxCollider _myAttackCollider;

    GroundCheck _groundCheck;
    Action _jumpFunction;
    public override void Awake()
    {
        base.Awake();

        _attackRange = sightRange;

        _jumpFunction = Jump;
        _groundCheck = GetComponentInChildren<GroundCheck>();
        myMovement = new ForwardMovement(speed, this.transform, rb);

        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Jump));
        fsm.AddState(StateName.Jump, new JumpState(this, fsm, jumpCd, _jumpFunction, _groundCheck, StateName.Chase));
        fsm.AddState(StateName.Chase, new MoveForwardState(this, fsm, StateName.Idle));
        fsm.ChangeState(StateName.Idle);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public override void Die()
    {
        base.Die();
        _myAttackCollider.enabled = false;
    }
}
