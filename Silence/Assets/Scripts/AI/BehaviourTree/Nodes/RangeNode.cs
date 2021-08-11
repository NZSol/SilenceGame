using System;
using System.Collections.Generic;
using UnityEngine;

class RangeNode : Node
{
    float range;
    Transform target;
    Transform origin;

    public RangeNode(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        float dist = Vector3.Distance(target.position, origin.position);
        return dist <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}