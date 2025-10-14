using UnityEngine;

namespace StateMachine
{
   
    public abstract class State : IState
    {
        protected StateMachine fsm;
        protected Player player;
        protected Animator animator;
        protected Rigidbody2D rb;
        
        protected State parentState;
        protected State activeSubState;
        protected State defaultSubState;

        public State(StateMachine fsm, Player player, Animator animator, Rigidbody2D rb, State parentState = null)
        {
            this.fsm = fsm;
            this.player = player;
            this.animator = animator;
            this.rb = rb;
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