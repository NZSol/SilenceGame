using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaseNode : Node
{
    Transform target;
    NavMeshAgent agent;

    public chaseNode(Transform target, NavMeshAgent agent, EnemyAI ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.curNode = this;
        ai.SetColor(Color.yellow);
        float dist = Vector3.Distance(target.position, agent.transform.position);
        if (dist > 0.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            Debug.Log("hitting catch");
            return NodeState.SUCCESS;
        }
    }
}
