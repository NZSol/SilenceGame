using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCoveredNode : Node
{
    Transform target;
    Transform origin;
    float timer = 2;
    public isCoveredNode(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        origin.GetComponent<EnemyAI>().curHealth += Time.deltaTime;
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
