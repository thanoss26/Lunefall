using UnityEngine;

namespace StateMachine
{
    public class PlayerWalk : State<PlayerContext, PlayerStateId>
    {
        public PlayerWalk(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx, State<PlayerContext, PlayerStateId> parentState = null) : base(fsm, ctx, parentState)
        {
            
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            ctx.Rigidbody.linearVelocity = new Vector2(ctx.moveX * ctx.Data.moveSpeed, ctx.Rigidbody.linearVelocity.y);
            ctx.Animator.SetFloat("speed", Mathf.Abs(ctx.moveX));
            
            FlipPlayer();
            
            if (Mathf.Abs(ctx.moveX) < 0.1f)
            {
                TransitionTo(PlayerStateId.Idle);
            }
        }

        private void FlipPlayer()
        {
            if(ctx.moveX > 0.1f)
            {
                ctx.SpriteRenderer.flipX = false;
            }
            else if (ctx.moveX < -0.1f)
            {
                ctx.SpriteRenderer.flipX = true;
            }
        }

        public override void Exit()
        {
        }
    }
}