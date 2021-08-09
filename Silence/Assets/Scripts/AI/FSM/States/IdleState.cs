using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
    class IdleState : abstractFSMState
    {

        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("ENTERED IDLE STATE");
            return true;
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("EXITING IDLE STATE");
            return true;
        }

        public override void Action()
        {
            throw new System.NotImplementedException();
        }
    }
}
