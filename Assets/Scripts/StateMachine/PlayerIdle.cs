using UnityEngine;

namespace StateMachine
{
    public class PlayerIdle : State<PlayerContext, PlayerStateId>
    {
        public PlayerIdle(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx, State<PlayerContext, PlayerStateId> parentState = null) : base(fsm, ctx, parentState)
        {
            
        }

        public override void Enter()
        {
            ctx.Animator.SetBool("Idle", true);
            Debug.Log("Entered Idle");
        }

        public override void Update()
        {
            if (Mathf.Abs(ctx.moveX) > 0.1f)
            {
                TransitionTo(PlayerStateId.Walk);
            }
        }

        public override void Exit()
        {
            ctx.Animator.SetBool("Idle", false);
            Debug.Log("Exited Idle");
        }
    }
}