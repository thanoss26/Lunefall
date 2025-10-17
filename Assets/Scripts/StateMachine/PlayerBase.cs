using UnityEngine;
using StateMachine;

public abstract class PlayerBase : MonoBehaviour
{
    protected HFSM<PlayerContext, PlayerStateId> fsm;
    protected PlayerContext context;

    [SerializeField] private bool autoInitialize = true;
    protected abstract PlayerContext CreateContext();
    
    protected abstract PlayerStateId GetInitialState();
    protected abstract void UpdateInput();

    protected virtual void Awake()
    {
        if (!autoInitialize) return;

        Initialize();
    }

    public void Initialize()
    {
        context = CreateContext();
        fsm = new HFSM<PlayerContext, PlayerStateId>();
        var factory = new StateFactory(fsm, context);
        fsm.SetStates(factory.BuildStates());
        fsm.ChangeState(GetInitialState());
    }

    protected virtual void Update()
    {
        UpdateInput();
        fsm.Update();
    }

    protected virtual void FixedUpdate()
    {
        fsm.FixedUpdate();
    }
}