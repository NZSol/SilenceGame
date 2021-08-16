using System;
using System.Collections.Generic;
using UnityEngine;

class RangeNode : Node
{
    float range;
    Transform target;
    Transform origin;

    public RangeNode(float range, Transform target, Transform origin, EnemyAI ai)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.curNode = this;
        float dist = Vector3.Distance(target.position, origin.position);
        return dist <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}