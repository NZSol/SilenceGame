using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SearchState", menuName = "Unity-FSM/States/Search", order = 5)]
public class SearchState : abstractFSMState
{
    GameObject playerChar;
    public float rotationSpeed = 10f;
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.SEARCH;
    }

    public override bool EnterState()
    {

        if (base.EnterState())
        {
            playerChar = npc.player;
            if (playerChar == null)
            {
                Debug.LogError("No Player Received");
            }
            else
            {
                EnteredState = true;
            }
            SetDestination(playerChar);
        }
        return EnteredState;

    }

    float timer = 3f;
    public override void Action()
    {
        base.Action();
        float Dist = Vector3.Distance(agent.transform.position, playerChar.transform.position);
        Vector3 dir = playerChar.transform.position - agent.transform.position;
        RaycastHit hit;
        if (Vector3.Angle(agent.transform.position, dir) < fovRange)
        if (Physics.Raycast(agent.transform.position, dir, out hit))
        {
            if (hit.transform.gameObject != playerChar)
            {
                return;
            }
            else
            {
                machine.EnterState(FSMStateType.TARGET);
            }
        }
        if(agent.remainingDistance < 1f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += 3;
                machine.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        return base.ExitState();
    }


    void SetDestination(GameObject destination)
    {
        if (agent != null && destination != null)
        {
            agent.SetDestination(destination.transform.position);
        }
    }
}
