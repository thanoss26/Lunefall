using UnityEngine;

namespace StateMachine
{
   
    public abstract class State : IState
    {
        protected StateMachine fsm;
        protected PlayerContext ctx;
        
        protected State parentState;
        protected State activeSubState;
        protected State defaultSubState;

        public State(StateMachine fsm, PlayerContext context, State parentState = null)
        {
            this.fsm = fsm;
            this.ctx = context;
            this.parentState = parentState;
        }
        
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public abstract void CheckTransition();
        
        public virtual void FixedUpdate()
        {
            activeSubState?.FixedUpdate();
        }

        
        protected void SetDefaultSubState(State sub)
        {
            defaultSubState = sub;
        }
        
        public void SetSubState(State sub)
        {
            activeSubState?.Exit();
            activeSubState = sub;
            activeSubState?.Enter();
        }
        
        protected void TransitionTo(PlayerStates id)
        {
            fsm.ChangeState(id);
        }
        
        public State GetParent()
        {
            return parentState;
        }
    }
}