using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{

    abstractFSMState curState;
        
    abstractFSMState prevState;


    [SerializeField]
    List<abstractFSMState> validStates;
    Dictionary<FSMStateType, abstractFSMState> fsmStates;

    public float incomingAudioLevel;
    public GameObject activeAudioCaster;
    public bool InAudioRange = false;

    public void Awake()
    {
        curState = null;

        fsmStates = new Dictionary<FSMStateType, abstractFSMState>();

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        NPC npc = GetComponent<NPC>();
        GameObject player = npc.player;
        foreach (abstractFSMState state in validStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC(npc);
            state.SetAgent(agent);
            state.SetTargetPlayer(player);
            fsmStates.Add(state.StateType, state);
        }
    }

    public void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    public void Update()
    {
        if (incomingAudioLevel <= 0)
        {
            activeAudioCaster = null;
        }
        if (activeAudioCaster == null)
        {
            InAudioRange = false;
        }
        if (curState != null)
        {
            curState.Action();
        }
    }


    #region STATE MANAGEMENT
    public void EnterState(abstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }
        if (curState != null)
        {
            curState.ExitState();
        }
        curState = nextState;
        curState.EnterState();

    }

    public void EnterState(FSMStateType stateType)
    {

        if (fsmStates.ContainsKey(stateType))
        {
            abstractFSMState nextState = fsmStates[stateType];

            EnterState(nextState);
        }
    }
    #endregion
}