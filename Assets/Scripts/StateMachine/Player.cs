using UnityEngine;

namespace StateMachine
{
    public class Player : MonoBehaviour
    {
        private StatesInit states;

        private void Awake()
        {
            states = GetComponent<StatesInit>();
        }
    }
}