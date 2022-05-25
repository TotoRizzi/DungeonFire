using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    StateMachine _fsm;
    FSMEnemy _myEnemy;

    StateName _nextState;

    public IdleState(FSMEnemy myEnemy, StateMachine fsm, StateName nextState)
    {
        _fsm = fsm;
        _myEnemy = myEnemy;
        _nextState = nextState;
    }
    public void OnEnter()
    {
        if (_myEnemy.anim != null) _myEnemy.anim.SetBool("isRunning", false);
    }

    public void OnExit()
    {
        if(_myEnemy.anim != null) _myEnemy.anim.SetBool("isRunning", true);
    }

    public void OnUpdate()
    {
        if (_myEnemy.SeePlayer())
        {
            _fsm.ChangeState(_nextState);
        }
    }
}
