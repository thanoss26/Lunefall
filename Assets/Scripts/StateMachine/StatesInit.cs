using UnityEngine;

namespace StateMachine
{
    public class StatesInit : StateMachine
    {
        [SerializeField] private Player player;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        protected override void InitializeStates()
        {
            states = StateFactory.CreateStates(this, player, animator, rb);
            ChangeState(PlayerStates.Idle);
        }
    }
}