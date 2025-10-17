namespace StateMachine
{
    public class PlayerAirborne : State<PlayerContext, PlayerStateId>
    {
        public PlayerAirborne(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx, State<PlayerContext, PlayerStateId> parentState = null) : base(fsm, ctx, parentState)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}