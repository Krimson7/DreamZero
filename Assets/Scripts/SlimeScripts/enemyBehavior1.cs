using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior1 : MonoBehaviour
{

    //charging enemy 

    public enum State{
        Idle,
        Wander,
        Attack,
        Knocked_back,
    }

    [SerializeField] private State state;
    [SerializeField] private State prevState;

    [Header("hidden variables")]
    int facingRight = -1;
    // float chargeTimer = 1f;
    // float chargeCurrentSpeed = 0f;
    bool checkPitfall, checkWall, playerDetected, playerInFront, playerInChargeZone, isCharging, isAttacking;
    string stateString;
    bool queueFlip = false;

    [Header("Components")]
    GameObject groundCheck;
    GameObject wallCheck;
    public Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    Animator animator;
    Rigidbody2D rb;

    I_enemyIdle idleState;
    I_enemyWander wanderState;
    I_enemyAttack attackState;

    [Header("Collision Checks")]
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public float playerDetectionRange = 2f;
    float playerDetectionRangeVar;
    public Collider2D playerInRange;
    public Collider2D playerCharged;
    


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

        playerDetectionRangeVar = playerDetectionRange;

        state = State.Idle;
        groundCheck = transform.Find("GroundCheck").gameObject;
        wallCheck = transform.Find("WallCheck").gameObject;
        chargeHitPoint = transform.Find("ChargePoint");
        animator = transform.Find("Character_Holder").transform.Find("Character_Renderer").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        checkPitfall = !Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
        checkWall = Physics2D.OverlapCircle(wallCheck.transform.position, 0.1f, groundLayer);
        facingRight = transform.localScale.x>0 ? -1 : 1;
        playerInRange = Physics2D.OverlapCircle(transform.position, playerDetectionRangeVar, playerLayer);
        playerInFront = (playerInRange != null) && (playerInRange.transform.position.x > transform.position.x) ^ (transform.localScale.x < 0);
        playerDetected = playerInRange != null;
        playerCharged = Physics2D.OverlapBox(chargeHitPoint.position, chargeHitSize, 0f, playerLayer);
        playerInChargeZone = playerCharged? true:false;
        switch(state){
            case State.Idle:
                playerDetectionRangeVar = playerDetectionRange;
                stateString = idleState.Idle(animator, playerInFront);
                checkExitIdle(stateString);
                break;
            case State.Wander:
                playerDetectionRangeVar = playerDetectionRange;
                stateString = wanderState.Wander(animator, playerInFront, checkWall, checkPitfall);
                checkExitWander(stateString);
                break;
            case State.Attack:
                playerDetectionRangeVar = playerDetectionRange * 2;
                stateString = attackState.Attack(animator, playerInRange);
                checkExitAttack(stateString);
                break;
            case State.Knocked_back:
                Knocked_back();
                break;
            default:
                break;
        }
    }


    void checkExitIdle(string stateString){
        switch(stateString){
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

    void checkExitWander(string stateString){
        switch(stateString){
            case "Go Idle":
                state = State.Idle;
                break;
            case "Go Idle, q flip":
                queueFlip = true;
                state = State.Idle;
                break;
            case "Go Attack":
                state = State.Attack;
                break;
        }
    }

    void checkExitAttack(string stateString){
        switch(stateString){
            case "Go Idle":
                state = State.Idle;
                break;
            case "Go Idle, q flip":
                queueFlip = true;
                state = State.Idle;
                break;
            case "Go Wander":
                state = State.Wander;
                break;
        }
    }

    void Attack(){
        //Attack
    }

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

    void Knocked_back(){
        //knocked back
        getKnockback(10f);
        
    }

    public void changeToKB(){

    }
    
    public void getKnockback(float force){

    }

    // void exit_S(){
        // exitState = true;
    // }

    public void changeState(State nextState){
        prevState = state;
        state = nextState;
    }

    public void Flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector3)groundCheck.transform.position, 0.1f);
        Gizmos.DrawWireSphere((Vector3)wallCheck.transform.position, 0.1f);
        if(playerDetected) Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector3)transform.position, playerDetectionRangeVar);
        Gizmos.color = playerCharged? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector3)chargeHitPoint.position, chargeHitSize);
    }
}
