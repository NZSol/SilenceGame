using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    NavMeshAgent agent;

    public GoToCoverNode(NavMeshAgent agent, EnemyAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.curNode = this;
        Transform cover = ai.GetBestCover();
        if(cover == null)
        {
            return NodeState.FAILURE;
        }
        ai.SetColor(Color.yellow);
        float dist = Vector3.Distance(cover.position, agent.transform.position);
        if (dist > 0.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(cover.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }

}
