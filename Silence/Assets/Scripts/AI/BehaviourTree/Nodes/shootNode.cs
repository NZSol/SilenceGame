using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shootNode : Node
{
    NavMeshAgent agent;

    public shootNode(NavMeshAgent agent, EnemyAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }


    public override NodeState Evaluate()
    {
        ai.curNode = this;
        agent.isStopped = true;
        ai.SetColor(Color.green);
        return NodeState.RUNNING;
    }
}
