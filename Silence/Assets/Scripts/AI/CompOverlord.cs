using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompOverlord : MonoBehaviour
{
    public AIState curState { get; private set; }

    private void Awake()
    {
        
    }

    void Initialize(AIState startState)
    {
        curState = startState;
        curState.Enter(this);
    }

    public void ChangeState(AIState newState)
    {
        curState.Exit(this);
        curState = newState;
        curState.Enter(this);
    }

    void Update()
    {
        curState.RegUpdate(this);
    }

    void FixedUpdate()
    {
        curState.PhysUpdate(this);
    }

    #region Functions

    #endregion
}
