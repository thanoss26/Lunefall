using UnityEngine;

namespace StateMachine
{
    public class PlayerWalk : State
    {
        public PlayerWalk(StateMachine fsm, Player player, Animator animator, Rigidbody2D rb, State parentState = null) : base(fsm, player, animator, rb, parentState)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckTransition()
        {
            throw new System.NotImplementedException();
        }
    }
}