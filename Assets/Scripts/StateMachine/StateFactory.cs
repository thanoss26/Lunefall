using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public static class StateFactory
    {
        public static Dictionary<PlayerStates, State> CreateStates(StateMachine fsm, PlayerContext ctx)
        {
            var groundedStates = new GroundedState(fsm, ctx);
            
            var states = new Dictionary<PlayerStates, State>
            {
                { PlayerStates.Grounded, groundedStates },
                { PlayerStates.Walk, new PlayerWalk(fsm, ctx) },
            };
            
            return states;
        }
    }
}