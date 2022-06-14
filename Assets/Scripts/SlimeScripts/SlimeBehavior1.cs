using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior1 : MonoBehaviour
{
    public Animator animator;
    private enum State{
        Idle,
        Wander,
        Chase,
        Attacking,
    }
    [SerializeField] private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Idle:
                Idle();
                break;
            case State.Wander:
                Wander();
                break;
            case State.Attacking:
                break;
            case State.Chase:
                break;
            default:
                break;
        }
    }

    public void Idle(){
        animator.Play("Slime_Idle");
    }
    public void Wander(){
        animator.Play("Slime_Walk");
    }
}
