using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState
{
    protected float startTime;
    private string stateName;
    public AIState(string stateName)
    {
        this.stateName = stateName;
    }

    public virtual void Enter(CompOverlord AI)
    {
        startTime = Time.time;
        Debug.Log($"AI in " + stateName + " state");
    }

    public virtual void RegUpdate(CompOverlord AI)
    {
        
    }

    public virtual void PhysUpdate(CompOverlord AI)
    {

    }

    public virtual void Exit(CompOverlord AI)
    {
        
    }



}
