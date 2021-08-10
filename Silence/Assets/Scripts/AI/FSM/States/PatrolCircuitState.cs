using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI.FSM.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
    public class PatrolCircuitState : abstractFSMState
    {
        GameObject[] patrolPoints = null;
        int patrolPntIndex = 0;


        private void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            patrolPntIndex = -1;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                patrolPoints = npc.patrolPoints;
                if (patrolPoints == null || patrolPoints.Length == 0)
                {
                    Debug.LogError("No Patrol Points From NPC");
                }
                else
                {
                    
                    if (patrolPntIndex < 0)
                    {
                        patrolPntIndex = Random.Range(0, patrolPoints.Length);
                    }
                    else
                    {
                        patrolPntIndex = (patrolPntIndex + 1) % patrolPoints.Length;
                    }

                    SetDestination(patrolPoints[patrolPntIndex]);
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
                if (Vector3.Distance(agent.transform.position, patrolPoints[patrolPntIndex].transform.position) <= 1f)
                {
                    machine.EnterState(FSMStateType.IDLE);
                }
                Vector3 dir = (player.transform.position - agent.transform.position);
                float playerDist = Vector3.Distance(agent.transform.position, player.transform.position);
                RaycastHit hit;
                Debug.DrawRay(agent.transform.position, dir, Color.green);
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

        void SetDestination(GameObject destination)
        {
            if (agent != null && destination != null)
            {
                agent.SetDestination(destination.transform.position);
            }
        }
    }
}
