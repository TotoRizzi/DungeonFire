using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMGolem : FSMEnemy
{
    [Header("Golem Variables")]
    [SerializeField] private float _attackRange;
    public override void Awake()
    {
        base.Awake();

        myMovement = new DirectedMovement(speed, transform, player.transform, rb);
        fsm.AddState(StateName.Idle, new IdleState(this, fsm, StateName.Chase));
        fsm.AddState(StateName.Chase, new ChaseState(this, fsm, _attackRange, StateName.Attack));
        fsm.AddState(StateName.Attack, new AttackState(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
}
