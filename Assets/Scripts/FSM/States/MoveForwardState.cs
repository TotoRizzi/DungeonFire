using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardState : IState
{
    StateName _nextState;

    FSMEnemy _myEnemy;
    StateMachine _fsm;

    public MoveForwardState(FSMEnemy myEnemy, StateMachine fsm, StateName nextState)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
        _nextState = nextState;
    }

    public void OnEnter()
    {
        _myEnemy.LookAtPlayer();
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        _myEnemy.myMovement.Move();
    }
}
