using System;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data Profile")]
    public class PlayerData : ScriptableObject
    {
        public float moveSpeed = 5.0f;
        public float jumpForce = 12f;

        [Header("Ground Check")]
        public float groundCheckRadius = 0.2f;
        public LayerMask groundLayer;
    }
    
    public class PlayerContext
    {
        public Animator Animator { get; }
        public SpriteRenderer SpriteRenderer { get; }
        
        public float moveX { get; set; }
        public bool jumpInput { get; set; }
        public bool isGrounded { get; set; }
        public Rigidbody2D Rigidbody { get; }
        public PlayerData Data { get; }
        public PlayerContext(Rigidbody2D rb, Animator animator, SpriteRenderer spriteRenderer, PlayerData data)
        {
            this.Rigidbody = rb;
            this.Animator = animator;
            this.SpriteRenderer = spriteRenderer;
            this.Data = data;
        }
    }
}