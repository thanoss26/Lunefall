namespace StateMachine
{
    [System.Serializable]
    public class PlayerContext
    {
        public float MoveX {get; private set;}
        public bool Jump {get; private set;}

        public void Move(float speed)
        {
            
        }
    }
}