using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();
    protected List<Node> memoryNodes = new List<Node>();
    public Sequence(List<Node> nodes, EnemyAI ai, List<Node> memoryNodes)
    {
        this.nodes = nodes;
        this.ai = ai;
        this.memoryNodes = memoryNodes;
    }
    public override NodeState Evaluate()
    {
        bool isNodeRunning = false;
        foreach(var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isNodeRunning = true;
                    break;
                case NodeState.SUCCESS:

                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
            }
        }
        _nodeState = isNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }
}
