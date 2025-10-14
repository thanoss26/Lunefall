using UnityEngine;

namespace StateMachine
{
    public class PlayerIdle : State
    {
        public PlayerIdle(StateMachine fsm, Player player, Animator animator, Rigidbody2D rb, State parentState = null) : base(fsm, player, animator, rb, parentState)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Player is idle");
            
            rb.linearVelocity = Vector3.zero;
            animator.Play("Idle");
        }

        public override void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontal) > 0.1)
            {
                parentState.SetSubState(new PlayerWalk(fsm, player, animator, rb, parentState));
            }
        }
        public override void Exit()
        {
            Debug.Log("Player is Exiting");
        }

        public override void CheckTransition()
        {
            
        }
    }
}