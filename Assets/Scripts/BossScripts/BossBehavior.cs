using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBehavior : MonoBehaviour
{

    public enum State{
        Idle,
        Walk,
        Attack,
        Stunned,
        RandomizeAttack,
    }

    [SerializeField] private State state;

    [Header("hidden variables")]
    int facingRight = -1;
    // float chargeTimer = 1f;
    // float chargeCurrentSpeed = 0f;
    public bool checkPit, checkWall, playerDetected, isCharging, isAttacking;
    string stateString;
    // bool queueFlip = false;
    Vector2 hitDirection;

    [Header("Components")]
    public GameObject groundCheck;
    public GameObject wallCheck;
    // public Collider2D playerCheckZone;
    Animator animator;
    Rigidbody2D rb;

    [Header("Collision Checks")]
    public LayerMask groundLayer;
    public Collider2D playerInRange;

    I_BossIdle idleState;
    I_BossStunned stunnedState;
    // I_BossPlayerDetection playerDetection;

    // public List<I_BossAttack> attackStates;
    public List<BossAttackScriptables> attackScripts;

    public int attackIndex;

    // Start is called before the first frame update
    void Start()
    {
        //get states components
        idleState = GetComponent<I_BossIdle>();
        stunnedState = GetComponent<I_BossStunned>();
        // playerDetection = GetComponent<I_BossPlayerDetection>();

        state = State.Idle;        
        groundCheck = transform.Find("GroundCheck").gameObject;
        wallCheck = transform.Find("WallCheck").gameObject;
        animator = transform.Find("Character_Holder").transform.Find("Character_Renderer").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is in charge zone

        checkPit = !Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);
        checkWall = Physics2D.OverlapCircle(wallCheck.transform.position, 0.1f, groundLayer);
        facingRight = transform.localScale.x>0 ? -1 : 1;

        switch(state){
            case State.Idle:
                stateString = idleState.Idle(animator, playerInRange);
                break;
            case State.Walk:
                break;
            case State.Attack:
                if(playerInRange == null){
                    state = State.Idle;
                    // attackScripts[attackIndex].Reset();
                    break;
                }
                stateString = attackScripts[attackIndex].Attack(this);
                break;
            case State.Stunned:
                stateString = stunnedState.Stunned(animator);
                break;
            default:
                break;
        }
        checkExitState(stateString);
    }

    void checkExitState(string stateString){
        switch(stateString){
            case "Go Idle":
                state = State.Idle;
                break;
            case "Go Attack":
                attackIndex = Random.Range(0, attackScripts.Count);
                attackScripts[attackIndex].Reset();
                state = State.Attack;
                break;
            case "Go Stunned":
                state = State.Stunned;
                break;
            case "Exit Attack":
                state = State.Idle;
                break;
        }
    }

    IEnumerator Delay(float time){
        yield return new WaitForSeconds(time);
        state = State.Idle;
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.tag == "Player"){
    //         playerInRange = other;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.tag == "Player"){
    //         playerInRange = null;
    //     }
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

    public Animator getAnimator(){
        return animator;
    }

    public Rigidbody2D getRigidbody(){
        return rb;
    }

    public bool getCheckWall(){
        return checkWall;
    }
}
