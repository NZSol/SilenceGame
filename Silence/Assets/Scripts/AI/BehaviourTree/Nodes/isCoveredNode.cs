using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCoveredNode : Node
{
    Transform target;
    Transform origin;
    float timer = 2;
    public isCoveredNode(Transform target, Transform origin, EnemyAI ai)
    {
        this.target = target;
        this.origin = origin;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.curNode = this;
        RaycastHit hit;
        if (Physics.Raycast(origin.position, target.position - origin.position, out hit))
        {
            if (hit.collider.transform == target)
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
