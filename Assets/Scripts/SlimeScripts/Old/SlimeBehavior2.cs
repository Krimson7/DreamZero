using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior2 : MonoBehaviour, I_enemyBehavior
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
    [SerializeField] private State prevState;
    

    public int facingRight = -1;
    public Rigidbody2D rb;
    public float walkSpeed = 60f;
    public bool needsFlip;
    public float atk = 10f;
    public float atkMaxSpeed = 300f;
    public float atkRampRate = 1f;
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

    public Collider2D playerCharged;
    public Transform chargeHitPoint;
    public Vector2 chargeHitSize;
    public bool playerInChargeZone = false;

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
            playerOnRight = playerInRange.transform.position.x >= transform.position.x - facingRight * 0.3f;
            playerDetected = (!playerOnRight ^ facingRight==1)? true:false;
        }else{
            playerDetected = false;
        }
        playerCharged = Physics2D.OverlapBox(chargeHitPoint.position, chargeHitSize, 0f, playerLayer);
        playerInChargeZone = playerCharged? true:false;

        switch(state){
            case State.Idle:
                // if(prevState == State.Attacking){
                //     Idle();
                //     idleCounter = idleTimer - atkDelay;
                //     StartCoroutine(AttackDelay());
                // }else 
                if(playerDetected){
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
                if(playerDetected) changeState(State.Attacking);
                break;
            case State.Attacking:
                if(atkStage==attackingStage.idle) atkStage = attackingStage.initiate;
                Attack();
                if((!playerDetected || (playerOnRight ^ facingRight==1)) && !isAttacking){  //&& !isAttacking){
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
        animator.Play("Everything");
        rb.velocity = new Vector2(0f, rb.velocity.y); 
        idleCounter += Time.deltaTime;

    }
    public void Wander(){
        animator.Play("Everything");
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
        
        if(atkStage == attackingStage.initiate){
            StartCoroutine(InitRushAttack());
        }else if(atkStage == attackingStage.running){
            animator.Play("Everything");
            if(playerCharged){
                playerCharged.gameObject.GetComponent<playerStateMachine>().checkTakeDamage(atk, transform.position - playerInRange.transform.position);
                // getKnockback();
                isAttacking = false;
                StartCoroutine(AttackDelay());
            }
            if(playerDetected && (!playerOnRight ^ facingRight==1)){
                rb.velocity = new Vector2(facingRight * atkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
                if(atkSpeed <= atkMaxSpeed){
                    atkSpeed += atkRampRate;
                }else if(atkSpeed > atkMaxSpeed){
                    atkSpeed = atkMaxSpeed;
                }
            }else{
                rb.velocity = new Vector2(facingRight * atkSpeed * Time.fixedDeltaTime, rb.velocity.y); 
                if(!isAttacking)
                    StartCoroutine(AttackDelay());
            }
            
        }
    }

    IEnumerator InitRushAttack(){
        animator.Play("Everything");
        yield return new WaitForSeconds(0.3f);
        // isAttacking = true;
        atkStage = attackingStage.running;

    }

    IEnumerator AttackDelay(){
        isAttacking = true;
        getKnockback(100);
        yield return new WaitForSeconds(atkDelay);
        atkSpeed = 0f;
        isAttacking = false;
        atkStage = attackingStage.idle;
        prevState = State.Idle;
        changeState(State.Idle);
    }

    public void getKnockback(float force){
        atkSpeed = 0f;
        rb.velocity = new Vector2(0, rb.velocity.y); 
        rb.AddForce(new Vector2(force, force));
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
        if(playerDetected) Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector3)transform.position, playerDetectionRange);
        Gizmos.color = playerCharged? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector3)chargeHitPoint.position, chargeHitSize);
    }

    void changeState(State nextState){
        prevState = state;
        state = nextState;
    }
}
