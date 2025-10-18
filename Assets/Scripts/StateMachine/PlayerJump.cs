using UnityEngine;

namespace StateMachine
{
    public class PlayerJump : State<PlayerContext, PlayerStateId>
    {
        public PlayerJump(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx, State<PlayerContext, PlayerStateId> parentState = null) : base(fsm, ctx, parentState)
        {
            
        }

        public override void Enter()
        {
            ctx.Rigidbody.linearVelocity = Vector2.up * ctx.Data.jumpForce;
            Debug.Log(ctx.Data.jumpForce);
        }
        public override void Update()
        {
            if (ctx.Rigidbody.linearVelocity.y < 0)
            {
                TransitionTo(PlayerStateId.Airborne);
            }
        }

        public override void Exit()
        {
        }

      
    }
}