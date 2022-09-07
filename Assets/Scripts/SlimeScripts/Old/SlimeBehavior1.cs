using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior1 : MonoBehaviour
{
    public Animator animator;
    private string currentAnimation;

    private enum State{
        Idle,
        Wander,
        Flipping,
        Chase,
        Attacking,
    }
    [SerializeField] private State state;
    [SerializeField] private State prevState;
    

    public int facingRight = -1;
    public Rigidbody2D rb;
    public float walkSpeed;
    public bool needsFlip;
    public float atk = 10f;
    public float atkMaxSpeed = 1f;
    public float atkRampRate = 0.1f;
    public float atkSpeed = 0f;
    public bool isAttacking = false;
    public float atkDelay = 1f;
    
    public enum attackingStage{
        initiate,
        running,
        idle,
    };
    [SerializeField] private attackingStage atkStage;

    public GameObject groundCheck;
    public GameObject wallCheck;
    public LayerMask groundLayer;
    public bool checkPitfall;
    public bool checkWall;

    public LayerMask playerLayer;
    public float playerDetectionRange = 2f;
    public Collider2D playerInRange;
    public bool playerDetected;
    public bool playerOnRight;

    public float gravity = -1f;

    public float idleTimer;   // Time until quit idling
    public float idleCounter; // Variable to count time
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Wander;
        atkStage = attackingStage.idle;
    }

    // Update is called once per frame
    void Update()
    {
        // gravityEffect();
        checkPitfall = !Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
        checkWall = Physics2D.OverlapCircle(wallCheck.transform.position, 0.1f, groundLayer);

        playerInRange = Physics2D.OverlapCircle(transform.position, playerDetectionRange, playerLayer);
        if(playerInRange?.transform.position.y >= transform.position.y-0.3f){
            playerOnRight = playerInRange.transform.position.x >= transform.position.x - facingRight*0.3f;
            playerDetected = (!playerOnRight ^ facingRight==1)? true:false;

        }else{
            playerDetected = false;
        }
        

        switch(state){
            case State.Idle:
                if(playerDetected && atkStage == attackingStage.idle){
                    changeState(State.Attacking);
                }else if(idleCounter<=idleTimer){
                    Idle();
                }else if(needsFlip){
                    Flip();
                    needsFlip = false;
                }else{
                    idleCounter = 0f;
                    changeState(State.Wander);
                }
                break;
            case State.Wander:
                Wander();
                if(playerDetected){
                    changeState(State.Attacking);
                }
                break;
            case State.Attacking:
                if(atkStage==attackingStage.idle) atkStage = attackingStage.initiate;
                Attack();
                if((!playerDetected || (playerOnRight ^ facingRight==1)) && !isAttacking){
                    // Debug.Log("player not seen");
                    atkStage = attackingStage.idle;
                    changeState(State.Idle);
                }
                break;
            case State.Chase:
                break;
            default:
                break;
        }
    }

    public void Idle(){
        // changeAnimationState("Tako_Idle");
        animator.SetBool("walk 0", false);
        rb.velocity = new Vector2(0f, rb.velocity.y); 
        idleCounter += Time.deltaTime;

    }
    public void Wander(){
        // changeAnimationState("Tako_Walk");
        animator.SetBool("walk 0", true);

        //                          direction *   speed   *   time
        rb.velocity = new Vector2(facingRight * walkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
        if(checkWall){
            Flip();
        } else if(checkPitfall){
            needsFlip = true;
            changeState(State.Idle);
        }
    }
    public void Attack(){
        switch(atkStage){
            default: 
                break;
            case attackingStage.initiate:
                StartCoroutine(InitAttack());
                break;
            case attackingStage.running:
                if(!isAttacking)
                    StartCoroutine(AttackDelay());
                break;
        }
    }

    IEnumerator InitAttack(){
        // animator.Play("Tako_Attack");
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        atkStage = attackingStage.running;
    }

    IEnumerator AttackDelay(){
        // animator.Play("Tako_Idle");
        isAttacking = true;
        playerInRange?.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerInRange.transform.position);
        // atkStage = attackingStage.cooldown;
        yield return new WaitForSeconds(atkDelay);
        // Debug.Log("waitingDone?");
        isAttacking = false;
        atkStage = attackingStage.idle;
        prevState = State.Idle;
        changeState(State.Idle);
        
    }

    public void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        facingRight *= -1;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector3)groundCheck.transform.position, 0.1f);
        Gizmos.DrawWireSphere((Vector3)wallCheck.transform.position, 0.1f);
        if(playerDetected){
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere((Vector3)transform.position, playerDetectionRange);
    }

    void changeState(State nextState){
        prevState = state;
        state = nextState;
    }

    // void changeAnimationState(string nextAnimation){
    //     if(currentAnimation == nextAnimation) return;
    //     animator.Play(nextAnimation);
    //     currentAnimation = nextAnimation;
    // }
}
