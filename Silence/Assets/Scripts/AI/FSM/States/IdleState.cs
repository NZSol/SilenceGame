using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
    class IdleState : abstractFSMState
    {
        [SerializeField]
        float idleTimer = 3f;
        float totalDuration;


        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();

            if (EnteredState)
            {
                totalDuration = 0f;
            }
            return EnteredState;
        }

        public override void Action()
        {
            base.Action();
            if (EnteredState)
            {
                totalDuration += Time.deltaTime;

                if (totalDuration >= idleTimer)
                {
                    machine.EnterState(FSMStateType.PATROL);
                }

                Vector3 dir = (player.transform.position - agent.transform.position);
                float playerDist = Vector3.Distance(agent.transform.position, player.transform.position);
                RaycastHit hit;
                Debug.DrawRay(agent.transform.position, dir, Color.yellow);
                if (Vector3.Angle(dir, agent.transform.forward) < fovRange)
                {
                    if (Physics.Raycast(agent.transform.position, dir, out hit))
                    {
                        if (hit.transform.gameObject == player && playerDist < DetectionRange)
                        {
                            machine.EnterState(FSMStateType.TARGET);
                        }
                    }
                }
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }

    }
}
