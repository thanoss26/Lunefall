using UnityEngine;

namespace HFSM
{
    public class StateBase<TContext> : IState
    {
        protected TContext _context { get; private set; }
        protected StateMachine<TContext> _stateMachine { get; private set; }

        public StateBase(TContext context, _stateMachine<TContext> stateMachine)
        {
            _context = context;
            _stateMachine = stateMachine;
        }
    
        public void OnEnter() { }

        public void OnExit() { }

        public void OnUpdate() { }

        public void OnFixedUpdate() { }
    }
}

