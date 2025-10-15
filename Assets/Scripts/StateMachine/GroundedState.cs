using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class GroundedState : State
    {
        private Dictionary<PlayerStates, State> subStates = new();
        public GroundedState(StateMachine fsm, PlayerContext context, State parentState = null) : base(fsm, context, parentState)
        {
            var idle = new PlayerIdle(fsm, context, this);
            var walk = new PlayerWalk(fsm, context, this);
            
            subStates.Add(PlayerStates.Idle, idle);
            subStates.Add(PlayerStates.Walk, walk);
            
            SetDefaultSubState(idle); 
        }

        public override void Enter()
        {
            Debug.Log("Entering Grounded Parent State");
            ctx.rb.linearVelocity = Vector3.zero;
            
            activeSubState = defaultSubState;
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
        
        public State GetSubState(PlayerStates id)
        {
            if(subStates.TryGetValue(id, out State state))
                return state;
            return null;
        }
    }
}