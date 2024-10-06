using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : StateMachineBehaviour
{
    public enum State
    {
        IDLE,
        ATTACKING1,
        ATTACKING2,
        DEAD
    }

    public State state;
    private Transform player;
    [SerializeField] private float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
