using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSkeleton : FSMEnemy
{
    [Header("Skeleton Variables")]
    [SerializeField] private float _attackRange;
    public override void Awake()
    {
        base.Awake();

        enemyHealth = new FSMEnemyHealth(FlyweightPointer.Skeleton.maxHealth, this);
        myMovement = new DirectedMovement(FlyweightPointer.Skeleton.speed, transform, player.transform, rb);
        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Chase));
        fsm.AddState(StateName.Chase, new ChaseState(this, fsm, _attackRange, StateName.Attack));
        fsm.AddState(StateName.Attack, new AttackState(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
    public override void Start()
    {
        base.Start();
        ponitsToGive = Mathf.RoundToInt(FlyweightPointer.Skeleton.maxHealth);
    }
}
