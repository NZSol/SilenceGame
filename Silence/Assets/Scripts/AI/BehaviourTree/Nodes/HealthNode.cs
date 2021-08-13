using System;
using System.Collections.Generic;
using UnityEngine;
class HealthNode : Node
{
    private EnemyAI ai;
    private float threshhold;

    public HealthNode(EnemyAI ai, float threshhold)
    {
        this.ai = ai;
        this.threshhold = threshhold;
    }

    public override NodeState Evaluate()
    {
        return ai.curHealth <= threshhold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
