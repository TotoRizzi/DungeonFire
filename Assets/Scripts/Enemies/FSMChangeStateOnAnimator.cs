using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMChangeStateOnAnimator : MonoBehaviour
{
    [SerializeField] FSMEnemy _myEnemy;

    public void ChangeFSMState()
    {
        _myEnemy.fsm.ChangeState(StateName.Idle);
    }
}
