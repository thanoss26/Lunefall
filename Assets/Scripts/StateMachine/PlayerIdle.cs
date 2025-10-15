using UnityEngine;

namespace StateMachine
{
    public class PlayerIdle : State
    {
        public PlayerIdle(StateMachine fsm, PlayerContext context, State parentState = null) : base(fsm, context, parentState)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Player is idle");
            
            ctx.anim.SetBool("Idle", true);
        }

        public override void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            
            if (Mathf.Abs(horizontal) > 0.1)
            { 
                // Transition to existing Walk state instance
                if (parentState is GroundedState groundedParent)
                {
                    State walkState = groundedParent.GetSubState(PlayerStates.Walk);
                    parentState.SetSubState(walkState);
                }
            }
        }
        public override void Exit()
        {
            Debug.Log("Player is Exiting");
            ctx.anim.SetBool("Idle", false);
        }

        public override void CheckTransition()
        {
           
        }
    }
}