using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/States/Attack", order = 4)]
public class AttackState : abstractFSMState
{
    GameObject playerChar;
    float timer = 1.5f;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.ATTACK;
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
        Debug.Log("Performing Actions!");
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            timer += 1.5f;
            machine.EnterState(FSMStateType.TARGET);
        }
    }


    public override bool ExitState()
    {
        return base.ExitState();
    }


}
