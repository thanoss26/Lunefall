using System;
using System.Collections.Generic;
using UnityEngine;

namespace HFSM
{
    public class StateMachine<TContext>
    {
        public TContext Context { get; private set; }
        private State<TContext> _currentState;
        private readonly Dictionary<Type, State<TContext>> _states = new();

        public StateMachine(TContext context)
        {
            Context = context;
        }

        public void AddState(State<TContext> state)
        {
            var type = state.GetType();
            if (!_states.ContainsKey(type))
            {
                state.SetMachine(this);
                _states[type] = state;
            }
        }

        public void ChangeState<T>() where T : State<TContext>
        {
            var type = typeof(T);
            ChangeState(type);
        }
        public void ChangeState(Type stateType)
        {
            if (!_states.TryGetValue(stateType, out var newState))
            {
                Debug.LogError($"State {stateType.Name} not found in state machine.");
                return;
            }

            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState?.OnUpdate();

            var next = _currentState?.CheckTransitionsDeep();
            if (next != null && next != _currentState)
            {
                ChangeState(next.GetType());
            }
        }
        
        public State<TContext> GetCurrentState() => _currentState;
    }
}