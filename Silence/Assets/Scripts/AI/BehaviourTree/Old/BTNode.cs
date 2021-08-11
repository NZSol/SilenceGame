using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode
{
    public enum result { Running, Failure, Success};

    public BehaviorTree Tree { get; set; }

    public BTNode(BehaviorTree t)
    {
        Tree = t;
    }

    public virtual result Execute()
    {
        return result.Failure;
    }
}
