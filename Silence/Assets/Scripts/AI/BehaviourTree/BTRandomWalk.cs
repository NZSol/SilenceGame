using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BTRandomWalk : BTNode
{
    protected Vector3 NextDestination { get; set; }
    public float speed;
    public BTRandomWalk(BehaviorTree t) : base(t)
    {
        NextDestination = Vector3.zero;
        FindNextDestination();
    }


    public bool FindNextDestination()
    {
        object o;
        bool found = false;
        found = Tree.Blackboard.TryGetValue("WorldBounds", out o);
        if (found)
        {
            Rect bounds = (Rect)o;
            float x = Random.value * bounds.width;
            float y = Random.value * bounds.height;
            NextDestination = new Vector3(x, 0, y);
        }

        return found;
    }

    public override result Execute()
    {
        //if at point, find new one
        if (Tree.gameObject.transform.position == NextDestination)
        {
            if (!FindNextDestination())
            {
                return result.Failure;
            }
            else
            {
                return result.Success;
            }
        }
        else
        {
            Tree.gameObject.transform.position = Vector3.MoveTowards(Tree.transform.position, NextDestination, speed * Time.deltaTime);

            return result.Running;
        }
    }

}
