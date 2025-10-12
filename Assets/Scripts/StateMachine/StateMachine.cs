using System;
using System.Collections.Generic;
using UnityEngine;

namespace HFSM
{
    public class StateMachine<TContext>
    {
        private TContext _context;
        private Dictionary<PlayerStates, IState> _states = new Dictionary<PlayerStates, IState>();
        
        public IState CurrentState { get; private set; }

        public StateMachine(TContext context)
        {
            _context = context;
        }

        public void AddState(string name, IState state)
        {
            _states.Add(name, state);
        }

        public void ChangeState(string newStateName)
        {
            CurrentState?.OnExit();
            if (_states.TryGetValue(newStateName, out IState newState))
            {
                CurrentState = newState;
                CurrentState.OnEnter();
            }
        }
    }
}