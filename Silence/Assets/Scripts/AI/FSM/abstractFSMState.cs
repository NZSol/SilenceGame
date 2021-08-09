using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum execState
{
    None,
    Active,
    Complete,
    Terminated,
}

public abstract class abstractFSMState : ScriptableObject
{
    public execState ExecutionState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = execState.None;
    }


    public virtual bool EnterState()
    {
        ExecutionState = execState.Active;
        return true;
    }

    public abstract void Action();

    public virtual bool ExitState()
    {
        ExecutionState = execState.Complete;
        return true;
    }

}
