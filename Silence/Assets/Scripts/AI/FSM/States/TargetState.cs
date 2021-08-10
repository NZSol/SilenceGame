using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "TargetState", menuName = "Unity-FSM/States/Target", order = 3)]
public class TargetState : abstractFSMState
{
    GameObject playerChar;

    private void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.TARGET;
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
        }
        return EnteredState;
    }

    public override void Action()
    {
        base.Action();
        if (EnteredState)
        {
            SetDestination(playerChar);
            float dist = Vector3.Distance(agent.transform.position, playerChar.transform.position);
            if (dist < 2f)
            {
                machine.EnterState(FSMStateType.ATTACK);
            }


            Vector3 dir = (player.transform.position - agent.transform.position);
            RaycastHit hit;
            Debug.DrawRay(agent.transform.position, dir, Color.red);
            if (Physics.Raycast(agent.transform.position, dir, out hit))
            {
                if (hit.transform.gameObject != playerChar)
                {
                    machine.EnterState(FSMStateType.SEARCH);
                }
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
