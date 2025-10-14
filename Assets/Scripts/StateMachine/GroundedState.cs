using UnityEngine;

namespace StateMachine
{
    public class GroundedState : State
    {
        public GroundedState(StateMachine fsm, Player player, Animator animator, Rigidbody2D rb, State parentState = null) : base(fsm, player, animator, rb, parentState)
        {
         
            var idle = new PlayerIdle(fsm, player, animator, rb, this);
            var walk = new PlayerWalk(fsm, player, animator, rb, this);
            //var run = new RunState(fsm, player, animator, rb, this);
        }

        public override void Enter()
        {
            defaultSubState?.Enter();
        }

        public override void Update()
        {
            activeSubState?.Update();
        }

        public override void Exit()
        {
            activeSubState?.Exit();
        }

        public override void CheckTransition()
        {
            activeSubState?.CheckTransition();
        }
    }
}