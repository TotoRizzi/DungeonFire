using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StateName
{
    Idle,
    Jump,
    Chase,
    Attack
}

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private Dictionary<StateName, IState> allStates = new Dictionary<StateName, IState>();

    public void Update()
    {
        if (currentState != null) currentState.OnUpdate();
    }
    public void AddState(StateName key, IState state)
    {
        if (!allStates.ContainsKey(key)) allStates.Add(key, state);
    }
    public void ChangeState(StateName key)
    {
        if (!allStates.ContainsKey(key)) return;

        if (currentState != null) currentState.OnExit();
        currentState = allStates[key];
        currentState.OnEnter();
    }
}
