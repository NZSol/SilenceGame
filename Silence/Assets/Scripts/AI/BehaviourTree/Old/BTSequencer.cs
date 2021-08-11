using System.Collections;
using System.Collections.Generic;

class BTSequencer : BTComposite
{
    int curNode = 0;
    public BTSequencer(BehaviorTree t, BTNode[] children) : base(t, children)
    {

    }

    public override result Execute()
    {
        if (curNode < Children.Count)
        {
            result _result = Children[curNode].Execute();

            switch (_result)
            {
                case result.Running:
                    return result.Running;
                case result.Failure:
                    curNode = 0;
                    return result.Failure;
                case result.Success:
                    curNode++;
                    if (curNode < Children.Count)
                    {
                        return result.Running;
                    }
                    else
                    {
                        curNode = 0;
                        return result.Success;
                    }
            }
        }
        return result.Success;
    }
}
