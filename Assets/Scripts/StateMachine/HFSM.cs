using System.Collections.Generic;

namespace StateMachine
{
    public class HFSM<TContext, TStateId>
    {
        private Dictionary<TStateId, State<TContext, TStateId>> states;
        private State<TContext, TStateId> currentState;

        public void SetStates(Dictionary<TStateId, State<TContext, TStateId>> states)
        {
            this.states = states;
        }

        public void ChangeState(TStateId id)
        {
            if (states == null || !states.ContainsKey(id))
                return;

            currentState?.Exit();
            currentState = states[id];
            currentState.Enter();
        }

        public void Update()
        {
            currentState?.Update();
            currentState?.CheckTransitions();
        }

        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }
    }
}