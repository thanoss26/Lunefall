using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public static class StateFactory
    {
        public static Dictionary<PlayerStates, State> CreateStates(StateMachine fsm, Player player, Animator animator, Rigidbody2D rb)
        {
            var states = new Dictionary<PlayerStates, State>
            {
                { PlayerStates.Idle, new PlayerIdle(fsm, player, animator, rb) },
                {PlayerStates.Walk, new PlayerWalk(fsm, player, animator, rb)},
            };


           

            return states;
        }
    }
}