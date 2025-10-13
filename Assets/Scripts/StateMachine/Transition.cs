using System;

namespace HFSM
{
    public class Transition<TContext>
    {
        public Func<TContext, bool> Condition { get; }
        public State<TContext> TargetState { get; }

        public Transition(State<TContext> target, Func<TContext, bool> condition)
        {
            TargetState = target;
            Condition = condition;
        }
    }
}