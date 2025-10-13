using UnityEngine;
using System.Collections.Generic;

namespace HFSM
{
    public abstract class State<TContext>
    {
        protected TContext Context { get; private set; }
        protected StateMachine<TContext> Machine { get; private set; }

        private List<Transition<TContext>> _transitions = new();
        protected State<TContext> ParentState;
        protected State<TContext> SubState;

        public void SetMachine(StateMachine<TContext> machine)
        {
            Machine = machine;
            Context = machine.Context;
        }

        public void AddTransition(Transition<TContext> transition)
        {
            _transitions.Add(transition);
        }

        public void SetParent(State<TContext> parent)
        {
            ParentState = parent;
        }

        public virtual void OnEnter()
        {
            SubState?.OnEnter();
        }

        public virtual void OnUpdate()
        {
            SubState?.OnUpdate();
        }

        public virtual void OnExit()
        {
            SubState?.OnExit();
        }

        public State<TContext> CheckTransitionsDeep()
        {
            var fromChild = SubState?.CheckTransitionsDeep();
            if (fromChild != null)
            {
                return fromChild;
            }

            foreach (var t in _transitions)
            {
                if (t.Condition(Context))
                {
                    return t.TargetState;
                }
            }

            return null;
        }

        public void SetSubState(State<TContext> sub)
        {
            SubState = sub;
            sub.SetParent(this);
            sub.SetMachine(Machine);
            SubState.OnEnter();
        }

        public void ClearSubState()
        {
            SubState?.OnExit();
            SubState = null;
        }
    }
}