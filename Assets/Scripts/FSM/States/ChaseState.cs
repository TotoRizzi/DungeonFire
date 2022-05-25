using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    StateName _nextState;

    FSMEnemy _myEnemy;
    StateMachine _fsm;

    float _attackRange;

    public ChaseState(FSMEnemy myEnemy, StateMachine fsm, float attackRange, StateName nextState)
    {
        _fsm = fsm;
        _myEnemy = myEnemy;
        _attackRange = attackRange;
        _nextState = nextState;
    }
    public void OnEnter()
    {
        if(_myEnemy.anim != null) _myEnemy.anim.SetBool("isRunning", true);
    }

    public void OnExit()
    {
        if (_myEnemy.anim != null)  _myEnemy.anim.SetBool("isRunning", false);
    }

    public void OnUpdate()
    {
        if (!_myEnemy.SeePlayer()) _fsm.ChangeState(StateName.Idle);
        _myEnemy.LookAtPlayer();

        _myEnemy.myMovement.Move();
        if (_myEnemy.GetDistanceToPlayer() <= _attackRange)
        {
            _fsm.ChangeState(_nextState);
        }
    }
}
