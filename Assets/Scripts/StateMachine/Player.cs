using UnityEngine;
using StateMachine;

public class Player : PlayerBase
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private PlayerData playerData;

    protected override PlayerContext CreateContext()
    { 
        return new PlayerContext(rb, animator, spriteRenderer, playerData);
    }

    protected override PlayerStateId GetInitialState() => PlayerStateId.Idle;

    protected override void UpdateInput()
    {
        context.moveX = Input.GetAxisRaw("Horizontal");
        context.jumpInput = Input.GetKeyDown(KeyCode.Space);
    }
}