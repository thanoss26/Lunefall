using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        
        protected Dictionary<PlayerStates, State> states = new();
        protected State currentState;

        protected abstract void InitializeStates();


        protected virtual void Awake()
        {
            InitializeStates();
        }
        protected virtual void Update()
        {
            currentState?.Update();
            currentState?.CheckTransition();
        }
        
        protected virtual void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void ChangeState(PlayerStates id)
        {
            if (!states.ContainsKey(id)) return;
            
            currentState?.Exit();
            currentState = states[id];
            currentState.Enter();
        }

        public State GetState(PlayerStates id)
        {
            if(states.ContainsKey(id))
                return states[id];
            return null;
        }

    }
}