// StatesInit.cs (CORRECTED)
using UnityEngine;

namespace StateMachine
{
    public class StatesInit : StateMachine
    {
        public Animator animator { get; private set; }
        public  Rigidbody2D rb { get; private set; }
        private PlayerContext playerContext;


        protected override void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            base.Awake();
        }
        protected override void InitializeStates()
        {
            playerContext = new PlayerContext(rb, animator);
            states = StateFactory.CreateStates(this, playerContext);
            
            ChangeState(PlayerStates.Grounded);
        }
        protected override void FixedUpdate()
        {
        }
    }
}