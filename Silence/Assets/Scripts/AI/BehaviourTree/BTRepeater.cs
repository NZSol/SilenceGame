using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BTRepeater : Decorator
{
    public BTRepeater(BehaviorTree t, BTNode c) : base(t, c)
    {
    }

    public override result Execute()
    {
        Debug.Log("Child Returned: " + Child.Execute());
        return result.Running;
    }
}
