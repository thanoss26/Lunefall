using UnityEngine;

namespace StateMachine
{
    [System.Serializable]
    public class PlayerContext
    {
        public Rigidbody2D rb {get; set;}
        public Animator anim {get; set;}
        
        public float MoveX {get; private set;}
        public float playerSpeed { get; private set; } = 6.0f;
        
        public bool Jump {get; private set;}


        public PlayerContext(Rigidbody2D rb, Animator animator)
        {
            this.rb = rb;
            this.anim = animator;
        }

        public void SetAnimationBool(string paramName, bool value)
        {
            anim.SetBool(paramName, value);
        }
    }
}