namespace StateMachine
{
    public class PlayerGround : State<PlayerContext, PlayerStateId>
    {
        public PlayerGround(HFSM<PlayerContext, PlayerStateId> fsm, PlayerContext ctx, State<PlayerContext, PlayerStateId> parentState = null) : base(fsm, ctx, parentState)
        {
            
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            activeSubState?.Update();
        }

        public override void Exit()
        {
        }
    }
}