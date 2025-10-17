using UnityEngine;

namespace StateMachine
{
    public abstract class State<TContext, TStateId>
    {
        protected HFSM<TContext, TStateId> fsm;
        protected TContext ctx;

        protected State<TContext, TStateId> parentState;
        protected State<TContext, TStateId> activeSubState;
        protected State<TContext, TStateId> defaultSubState;

        public State(HFSM<TContext, TStateId> fsm, TContext ctx, State<TContext, TStateId> parentState = null)
        {
            this.fsm = fsm;
            this.ctx = ctx;
            this.parentState = parentState;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public virtual void FixedUpdate()
        {
            activeSubState?.FixedUpdate();
        }
        
        public virtual void CheckTransitions() { }

        protected void SetSubState(State<TContext, TStateId> sub)
        {
            activeSubState?.Exit();
            activeSubState = sub;
            activeSubState?.Enter();
        }

        public void SetDefaultSubState(State<TContext, TStateId> sub)
        {
            defaultSubState = sub;
        }

        protected void TransitionTo(TStateId id)
        {
            fsm.ChangeState(id);
        }
    }
}