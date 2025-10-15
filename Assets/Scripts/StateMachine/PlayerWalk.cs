using UnityEngine;

namespace StateMachine
{
    public class PlayerWalk : State
    {
        public PlayerWalk(StateMachine fsm, PlayerContext context, State parentState = null) : base(fsm, context, parentState)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Player walk entered");
            ctx.anim.SetBool("Move", true);
        }

        public override void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            

            if (Mathf.Abs(horizontalInput) < 0.1f)
            {
                if (parentState is GroundedState groundedParent)
                {
                    State idleState = groundedParent.GetSubState(PlayerStates.Idle);
                    parentState.SetSubState(idleState);
                }
                return;
            }
        }

        public override void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            ctx.rb.linearVelocity = new Vector2(horizontalInput * ctx.playerSpeed, ctx.rb.linearVelocity.y);
        }

        public override void Exit()
        {
            ctx.anim.SetBool("Move", false);
        }

        public override void CheckTransition()
        {
        }
    }
}