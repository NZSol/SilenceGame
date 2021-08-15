﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Selector : Node
{
    protected List<Node> nodes = new List<Node>();
    public Selector(List<Node> nodes, EnemyAI ai)
    {
        this.ai = ai;
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        foreach(var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;

                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;

                case NodeState.FAILURE:
                    break;
            }
        }
        _nodeState = NodeState.FAILURE;
        return _nodeState;
    }
}
