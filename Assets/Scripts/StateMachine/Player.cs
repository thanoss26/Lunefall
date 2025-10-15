using StateMachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StatesInit states;

    private void Awake()
    {
        states = GetComponent<StatesInit>();
    }

    private void Upodate()
    {
        
    }
}
