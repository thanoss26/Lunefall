using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateFactory
    {
        private HFSM<PlayerContext, PlayerStateId> fsm;
        private PlayerContext ctx;

        public StateFactory(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx)
        {
            this.fsm = fsm;
            this.ctx = ctx;
        }

        public Dictionary<PlayerStateId, State<PlayerContext, PlayerStateId>> BuildStates()
        {
            var states = new Dictionary<PlayerStateId, State<PlayerContext, PlayerStateId>>();

            // Grounded hierarchy
            var grounded = new PlayerGround(fsm, ctx);
            var idle = new PlayerIdle(fsm, ctx, grounded);
            var walk = new PlayerWalk(fsm, ctx, grounded);
            grounded.SetDefaultSubState(idle);

            // Airborne hierarchy
            var airborne = new PlayerAirborne(fsm, ctx);
            var jump = new PlayerJump(fsm, ctx, airborne);
            airborne.SetDefaultSubState(jump);

            // Register states
            states[PlayerStateId.Grounded] = grounded;
            states[PlayerStateId.Idle] = idle;
            states[PlayerStateId.Walk] = walk;
            states[PlayerStateId.Airborne] = airborne;
            states[PlayerStateId.Jump] = jump;

            return states;
        }
    }
}