using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpState : IState
{
    StateName _nextState;

    StateMachine _fsm;
    FSMEnemy _myEnemy;
    Action _jumpFunction;
    GroundCheck _myGroundCheck;

    float _checkGroundTimer = .3f;
    float _currentCheckGroundTimer;
    float _jumpCd;
    float _currentJumpCd;

    bool _hasAlreadyJumped;
    bool _moveOnJump;

    public JumpState(FSMEnemy myEnemy, StateMachine fsm, float jumpCd, Action jumpFunction, GroundCheck myGroundCheck, StateName nextState, bool moveOnJump = false)
    {
        _nextState = nextState;
        
        _fsm = fsm;
        _myEnemy = myEnemy;
        _jumpFunction = jumpFunction;
        _myGroundCheck = myGroundCheck;
        
        _jumpCd = jumpCd;

        _moveOnJump = moveOnJump;
    }

    public void OnEnter()
    {
        _hasAlreadyJumped = false;
        _currentCheckGroundTimer = 0;
        _currentJumpCd = 0;
    }

    public void OnExit()
    {
        //Sacar la animacion de salto
    }

    public void OnUpdate()
    {
        _currentJumpCd += Time.deltaTime;

        if (!_hasAlreadyJumped) _myEnemy.LookAtPlayer();

        if(_currentJumpCd >= _jumpCd)
        {

            if(!_hasAlreadyJumped)
            {
                if (_myGroundCheck.isGrounded)
                    _jumpFunction();

                _hasAlreadyJumped = true;
            }

            if(_moveOnJump) _myEnemy.myMovement.Move();
            
            _currentCheckGroundTimer += Time.deltaTime;

            if (_currentCheckGroundTimer >= _checkGroundTimer)
                if (_myGroundCheck.isGrounded) _fsm.ChangeState(_nextState);
        }
    }
}
