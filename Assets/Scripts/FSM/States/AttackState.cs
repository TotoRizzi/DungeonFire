using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    StateMachine _fsm;
    FSMEnemy _myEnemy;


    public AttackState(FSMEnemy myEnemy, StateMachine fsm)
    {
        _fsm = fsm;
        _myEnemy = myEnemy;
    }

    public void OnEnter()
    {
        _myEnemy.anim.SetBool("attack", true);
    }

    public void OnExit()
    {
        _myEnemy.anim.SetBool("attack", false);
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer();
    }
}
