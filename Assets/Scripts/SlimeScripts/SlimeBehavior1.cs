using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior1 : MonoBehaviour
{
    public Animator animator;
    private enum State{
        Idle,
        Wander,
        Flipping,
        Chase,
        Attacking,
    }
    [SerializeField] private State state;
    public int facingRight = -1;
    public Rigidbody2D rb;
    public float walkSpeed;
    public bool needsFlip;

    public Vector2 groundCheckPos;
    public LayerMask groundLayer;
    public bool checkPitfall;

    public float gravity = -1f;

    public float idleTimer;   // Time until quit idling
    public float idleCounter; // Variable to count time
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Wander;
    }

    // Update is called once per frame
    void Update()
    {
        // gravityEffect();
        checkPitfall = !Physics2D.OverlapCircle((Vector2)transform.position+groundCheckPos, 0.1f, groundLayer);

        // if(playerDistance < AggroRange){}

        switch(state){
            case State.Idle:
                if(idleCounter<=idleTimer){
                    Idle();
                }else if(needsFlip){
                    Flip();
                    needsFlip = false;
                }else{
                    state = State.Wander;
                    idleCounter = 0f;
                }
                
                break;
            case State.Wander:
                Wander();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.Chase:
                break;
            default:
                break;
        }
    }

    public void Idle(){
        animator.Play("Slime_Idle");
        rb.velocity = new Vector2(0f, rb.velocity.y); 
        idleCounter += Time.deltaTime;

    }
    public void Wander(){
        animator.Play("Slime_Walk");
        //                          direction *   speed   *   time
        rb.velocity = new Vector2(facingRight * walkSpeed * Time.fixedDeltaTime, rb.velocity.y); 

        if(checkPitfall){
            needsFlip = true;
            state = State.Idle;
        }
    }
    public void Attack(){
        animator.Play("Slime_Jump");
    }

    public void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        groundCheckPos.x *= -1;
        facingRight *= -1;
    }

    // public void gravityEffect(){
    //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + gravity); 
    // }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((Vector3)((Vector2)transform.position+groundCheckPos), 0.1f);
    }
}
