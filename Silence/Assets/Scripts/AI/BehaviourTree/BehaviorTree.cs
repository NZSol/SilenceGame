using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{

    BTNode mRoot;
    bool startedBehavior;
    Coroutine behavior;

    public Dictionary<string, object> Blackboard { get; set; }
    public BTNode Root { get { return mRoot; } }

    // Start is called before the first frame update
    void Start()
    {
        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("WorldBounds", new Rect(0, 0, 5, 5));

        //initial behaviour is stopped
        startedBehavior = false;

        mRoot = new BTRepeater(this, new BTSequencer(this, new BTNode[] { new BTRandomWalk(this) } ));
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedBehavior)
        {
            behavior = StartCoroutine(RunBehavior());
        }
    }

    IEnumerator RunBehavior()
    {
        BTNode.result result = Root.Execute();
        while (result == BTNode.result.Running)
        {
            Debug.Log("Root result:" + result);
            yield return null;
            result = Root.Execute();
        }
        Debug.Log("Behaviour has finished with: " + result);
    }
}
