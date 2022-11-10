using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorShooter : MonoBehaviour
{

    //charging enemy 

    public enum State{
        Idle,
        Wander,
        Attack,
        Knocked_back,
    }

    [SerializeField] private State state;


    // [SerializeField] private State prevState;

    [Header("hidden variables")]
    int facingRight = -1;
    // float chargeTimer = 1f;
    // float chargeCurrentSpeed = 0f;
    bool checkPit, checkWall, playerDetected, playerInFront, playerInChargeZone, isCharging, isAttacking;
    string stateString;
    bool queueFlip = false;
    bool gotKnockedBack = false;
    Vector2 hitDirection;

    [Header("Components")]
    public GameObject groundCheck;
    public GameObject wallCheck;
    public Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    Animator animator;
    Rigidbody2D rb;
    

    I_enemyIdle idleState;
    I_enemyWander wanderState;
    I_enemyAttack attackState;
    I_enemyKnockedback knockedbackState;
    I_enemyPlayerDetection playerDetection;


    [Header("Collision Checks")]
    // public LayerMask playerLayer;
    public LayerMask groundLayer;
    public float playerDetectionRange = 2f;
    float playerDetectionRangeVar;
    public Collider2D playerInRange;
    // public Collider2D playerCharged;
    


    // [Header("Stats")]
    // public float walkSpeed = 60f;
    // public float atkDamage = 10f;
    // public float chargeMaxSpeed = 300f;
    // public float chargeRampRate = 1f;
    // public float chargeTotalDelay = 1f;
    // public float idleTime = 1f;

    // bool enterState, exitState;

    void Start()
    {
        idleState = GetComponent<I_enemyIdle>();
        wanderState = GetComponent<I_enemyWander>();
        attackState = GetComponent<I_enemyAttack>();
        knockedbackState = GetComponent<I_enemyKnockedback>();
        playerDetection = GetComponent<I_enemyPlayerDetection>();

        playerDetectionRangeVar = playerDetectionRange;

        state = State.Idle;

        groundCheck = transform.Find("GroundCheck").gameObject;
        wallCheck = transform.Find("WallCheck").gameObject;
        chargeHitPoint = transform.Find("ChargePoint");
        animator = transform.Find("Character_Holder").transform.Find("Character_Renderer").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        checkPit = !Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
        checkWall = Physics2D.OverlapCircle(wallCheck.transform.position, 0.1f, groundLayer);
        facingRight = transform.localScale.x>0 ? -1 : 1;

        playerInRange = playerDetection.Detect(playerDetectionRangeVar);

        // playerInRange = Physics2D.OverlapCircle(transform.position, playerDetectionRangeVar, playerLayer);
        playerInFront = (playerInRange != null) && ((playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x < 0));
        playerDetected = playerInRange != null;
        // playerCharged = Physics2D.OverlapBox(chargeHitPoint.position, chargeHitSize, 0f, playerLayer);
        // playerInChargeZone = playerCharged? true:false;
        switch(state){
            case State.Idle:
                playerDetectionRangeVar = playerDetectionRange;
                stateString = idleState.Idle(animator, playerDetected); //hererererererer
                break;
            case State.Wander:
                playerDetectionRangeVar = playerDetectionRange;
                stateString = wanderState.Wander(animator, playerInFront, checkWall, checkPit);
                break;
            case State.Attack:
                playerDetectionRangeVar = playerDetectionRange * 2;
                stateString = attackState.Attack(animator, playerInRange);
                break;
            case State.Knocked_back:
                // getKnockback();
                stateString = knockedbackState.Knocked_back(animator, hitDirection);
                break;
            default:
                break;
        }
        checkExitState(stateString);
    }



    void checkExitState(string stateString){
        if(gotKnockedBack){
            state = State.Knocked_back;
            gotKnockedBack = false;
            return;
        }
        switch(stateString){
            case "Go Idle":
                state = State.Idle;
                break;
            case "Go Idle, q flip":
                queueFlip = true;
                state = State.Idle;
                break;
            case "Go Wander":
                if(queueFlip){
                    queueFlip = false;
                    Flip();
                }
                state = State.Wander;
                break;
            case "Go Attack":
                state = State.Attack;
                break;
        }
    }

    // void Attack(){
    //     //Attack
    // }

    // public void Attack2(){
        
    //     if(atkStage == attackingStage.initiate){
    //         StartCoroutine(InitRushAttack());
    //     }else if(atkStage == attackingStage.running){
    //         animator.Play("Everything");
    //         if(playerCharged){
    //             playerCharged.gameObject.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerInRange.transform.position);
    //             // getKnockback();
    //             isAttacking = false;
    //             StartCoroutine(AttackDelay());
    //         }
    //         if(playerDetected && (!playerInFront ^ facingRight==1)){
    //             rb.velocity = new Vector2(facingRight * atkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
    //             if(atkSpeed <= atkMaxSpeed){
    //                 atkSpeed += atkRampRate;
    //             }else if(atkSpeed > atkMaxSpeed){
    //                 atkSpeed = atkMaxSpeed;
    //             }
    //         }else{
    //             rb.velocity = new Vector2(facingRight * atkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
    //             if(!isAttacking)
    //                 StartCoroutine(AttackDelay());
    //         }
            
    //     }
    // }

    // public void changeToKB(){

    // }
    
    public void getKnockback(float force, Vector3 hitD){
        gotKnockedBack = true;
        hitDirection = hitD;
        // rb.AddForce(new Vector2(force, force*2), ForceMode2D.Impulse);
    }

    // void exit_S(){
        // exitState = true;
    // }

    // public void changeState(State nextState){
    //     prevState = state;
    //     state = nextState;
    // }

    public void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector3)groundCheck.transform.position, 0.1f);
        Gizmos.DrawWireSphere((Vector3)wallCheck.transform.position, 0.1f);
        // Gizmos.color = playerInRange != null? Color.green : Color.red;
        // Gizmos.DrawWireSphere((Vector3)transform.position, playerDetectionRangeVar);
    }
}
