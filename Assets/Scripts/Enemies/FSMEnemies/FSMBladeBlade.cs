using UnityEngine;
using System;

public class FSMBladeBlade : FSMEnemy
{
    [Header("BladeBlade variables")]
    [SerializeField] private float jumpCd;
    [SerializeField] BoxCollider _myAttackCollider;

    GroundCheck _groundCheck;
    Action _jumpFunction;
    public override void Awake()
    {
        base.Awake();

        _jumpFunction = Jump;
        _groundCheck = GetComponentInChildren<GroundCheck>();
        myMovement = new ForwardMovement(FlyweightPointer.BladeBlade.speed, this.transform, rb);
        enemyHealth = new FSMEnemyHealth(FlyweightPointer.BladeBlade.maxHealth, this);

        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Jump));
        fsm.AddState(StateName.Jump, new JumpState(this, fsm, jumpCd, _jumpFunction, _groundCheck, StateName.Chase));
        fsm.AddState(StateName.Chase, new MoveForwardState(this, fsm, StateName.Idle));
        fsm.ChangeState(StateName.Idle);
    }
    public override void Start()
    {
        base.Start();
        ponitsToGive = Mathf.RoundToInt(FlyweightPointer.BladeBlade.maxHealth);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * FlyweightPointer.BladeBlade.jumpForce, ForceMode.Impulse);
    }

    public override void Die()
    {
        base.Die();
        _myAttackCollider.enabled = false;
    }
}
