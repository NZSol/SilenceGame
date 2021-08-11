using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum execState
{
    None,
    Active,
    Complete,
    Terminated,
}

public enum FSMStateType
{
    IDLE,
    PATROL,
    TARGET,
    ATTACK,
    SEARCH,
}

public abstract class abstractFSMState : ScriptableObject
{
    protected NavMeshAgent agent;
    protected NPC npc;
    protected FiniteStateMachine machine;
    protected GameObject player;
    protected float incomingAudio;


    public float DetectionRange;
    public float fovRange;

    public execState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = execState.None;
    }


    public virtual bool EnterState()
    {
        bool successNav = true;
        bool successNPC = true;
        ExecutionState = execState.Active;

        if (EnteredState)
        {
            Debug.Log(agent.name + " Has Entered state: " + StateType);
        }

        //Does Agent exist
        successNav = agent != null;
        //Does Executing agent exist
        successNPC = (npc != null);
        return successNav && successNPC;

    }

    public float extraRotSpeed = 5f;
    public virtual void Action()
    {
        Vector3 lookRot = agent.steeringTarget - agent.transform.position;
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(lookRot), extraRotSpeed * Time.deltaTime);
    }

    public virtual bool ExitState()
    {
        ExecutionState = execState.Complete;
        //Debug.Log("Exiting State: " + StateType);
        return true;
    }

    public virtual void SetAgent(NavMeshAgent inputAgent)
    {
        if (inputAgent != null)
        {
            agent = inputAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            machine = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC inputNPC)
    {
        if(inputNPC != null)
        {
            npc = inputNPC;
        }
    }

    public virtual void SetTargetPlayer(GameObject inputPlayer)
    {
        if (inputPlayer != null)
        {
            player = inputPlayer;
        }
    }


}
