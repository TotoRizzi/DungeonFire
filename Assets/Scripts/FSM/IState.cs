using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter();
    public void OnExit();
    public void OnUpdate();
}
