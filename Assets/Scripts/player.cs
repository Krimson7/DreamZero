using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 10f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Attacking")]
    // [SerializeField] private char attackKey = 'l';
    [SerializeField] private float attackDelay = 0.3f;
    public float atk = 10f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;
    public BoxCollider2D boxCollider2d;
    public GameObject A1_Hitbox;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.1f;
    public Vector3 colliderOffset;

    

    //Animation States
    [Header("Animation")]
    [SerializeField]private bool isFalling = false;
    // private bool isJumping = false;
    // private bool isRunning = false;
    // private bool isIdle = true;
    [SerializeField]private bool isAttacking = false;
    [SerializeField]private bool isAttackPressed = false;
    
    [SerializeField] private string currentAnimaton;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Running";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_AIR_ATTACK = "Player_Air_Attack";

    // Update is called once per frame
    void Update() {
        bool wasOnGround = onGround;
        

        if(!wasOnGround && onGround){
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking){
            isAttackPressed = true;
        }

        if(Input.GetButtonDown("Jump") && !isAttacking){
            jumpTimer = Time.time + jumpDelay;
        }

        
        animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() {                                                                    //update each physical frame
        onGround = isGrounded();                                                            //update onGround on each frame with function isGrounded()
        
        moveCharacter(direction.x);                                                         //function for moving player
        if(jumpTimer > Time.time && onGround){                                              //disable spam jumps
            Jump();
        }
        modifyPhysics();                                                                    //modify drag and gravity according to player's activity

        checkAnimationState();                                                              //animation controller function
    }

    void moveCharacter(float horizontal) {
        if(!isAttacking){
            rb.AddForce(Vector2.right * horizontal * moveSpeed);
        
            if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) {
                Flip();
            }

            if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }

            // isRunning = true; //Check running

            animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("vertical", rb.velocity.y);
        }
    }
    
    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        // isJumping = true; //Check jumping

        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }

    void modifyPhysics() {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if(onGround){
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections) {
                rb.drag = linearDrag;
            } else {
                rb.drag = 0f;
            }
            rb.gravityScale = 1f;
            isFalling = false;
        }else{
            if(isAttacking){
                rb.gravityScale = 0.1f;
                rb.drag = linearDrag*3f; 
            }else{
                rb.gravityScale = gravity;
                if(rb.velocity.y > 0){
                    float velX = Mathf.Abs(rb.velocity.x);
                    if(velX < 0.5f){
                        rb.drag = linearDrag*0.2f;
                    } else if(velX >= 0.5f){
                        rb.drag = linearDrag;
                    }
                }
                
                if(rb.velocity.y < 0){                                      //normal falling
                    rb.gravityScale = gravity * fallMultiplier;
                    rb.drag = linearDrag;
                    isFalling = true;                                           //Check Falling
                }else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){    //jump w/ released jump button
                    rb.gravityScale = gravity * (fallMultiplier / 2);
                }
            }
        }
    }

    void Flip() {                                                           //flip player rotation physically (rendered sprite will flip too)
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {    //Enumerator for jumping squeeze effect
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }
    
    private bool isGrounded(){                                              //Check Ground function
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, colliderOffset.y, groundLayer);
        
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }
        else{
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + colliderOffset.y), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + colliderOffset.y), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + colliderOffset.y), Vector2.right * (boxCollider2d.bounds.extents.x), rayColor);
        return raycastHit.collider != null;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void checkAnimationState(){
        if(isAttackPressed){
            isAttackPressed = false;
            if(!isAttacking){
                isAttacking = true;
                if(onGround){
                    ChangeAnimationState(PLAYER_ATTACK);
                }else{
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }
                Invoke("AttackComplete", attackDelay);
            }
        } 
        if(!isAttacking){
            if(onGround){
                if(Mathf.Abs(rb.velocity.x)>0.5 ){ 
                    ChangeAnimationState(PLAYER_RUN);
                } else if(!isAttacking){
                    ChangeAnimationState(PLAYER_IDLE);
                }
            }
            else{
                if(rb.velocity.y > 0.2){
                    ChangeAnimationState(PLAYER_JUMP);
                } else if(isFalling){
                    ChangeAnimationState(PLAYER_FALL);
                }
            }
        }
    }
}