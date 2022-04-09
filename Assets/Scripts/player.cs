using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    PlayerInput playerInput;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;
    public BoxCollider2D boxCollider2d;
    public GameObject A1_Hitbox;

    [Header("Inputs")]
    private Vector2 currentMovementInput;
    bool isMovementPressed = false;
    bool isJumpHeld = false;
    bool jumpKeyDown = false;
    bool isAttack1Held = false;
    bool Attack1KeyDown = false;


    [Header("Movement")]
    public Vector2 currentMovement;
    public float moveSpeed = 10f;
    public float maxSpeed = 7f;
    private bool facingRight = true;
    public float jumpSpeed = 10f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Physics")]
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Attacking")]
    // [SerializeField] private char attackKey = 'l';
    [SerializeField] private float attackDelay = 0.3f;
    public float atk = 10f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.1f;
    public Vector3 colliderOffset;

    [Header("Animation")]
    private bool isFalling = false;
    private bool isAttacking = false;
    private bool isAttackPressed = false;  
    [SerializeField] private string currentAnimaton;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Running";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_AIR_ATTACK = "Player_Air_Attack";

    
    
    
    void Awake(){
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Jump.started += onJumpKeyDown;
        playerInput.CharacterControls.Jump.canceled += onJumpKeyUp;
        // playerInput.CharacterControls.Jump.started += onJump;
        // playerInput.CharacterControls.Jump.canceled += onJump;
        playerInput.CharacterControls.Attack1.started += onAttack1KeyDown;
        playerInput.CharacterControls.Attack1.canceled += onAttack1KeyUp;
    }

    void onMovementInput (InputAction.CallbackContext context){
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x !=0 || currentMovementInput.y !=0;
    }

    // void onJump (InputAction.CallbackContext context){
    //     isJumpHeld = context.ReadValueAsButton();
    // }
    void onJumpKeyDown (InputAction.CallbackContext context){
        jumpKeyDown = true;
        isJumpHeld = context.ReadValueAsButton();
    }
    void onJumpKeyUp (InputAction.CallbackContext context){
        jumpKeyDown = false;
        isJumpHeld = context.ReadValueAsButton();
    }
    void onAttack1KeyDown (InputAction.CallbackContext context){
        Attack1KeyDown = true;
        isAttack1Held = context.ReadValueAsButton();
    }
    void onAttack1KeyUp (InputAction.CallbackContext context){
        Attack1KeyDown = false;
        isAttack1Held = context.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        if(Attack1KeyDown){
            isAttackPressed = true;
        }
        
        if(jumpKeyDown && !isAttacking){
            jumpKeyDown = false;
            jumpTimer = Time.time + jumpDelay;
        }
        checkAnimationState();              //animation controller function        
    }

    void FixedUpdate() {  
        onGround = isGrounded();                                                         //update onGround on each frame with function isGrounded()
        moveCharacter(currentMovement.x);
        if(jumpTimer > Time.time && onGround){                                              //disable spam jumps
            Jump();
            // Debug.Log("Jumping now");
        }
        modifyPhysics();
    }

    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }

    void modifyPhysics() {
        bool changingDirections = (currentMovement.x > 0 && rb.velocity.x < 0) || (currentMovement.x < 0 && rb.velocity.x > 0);

        if(onGround){
            if (Mathf.Abs(currentMovement.x) < 0.4f || changingDirections) {
                rb.drag = linearDrag*2f;
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
                        rb.drag = linearDrag*0.3f;
                    } else if(velX >= 0.5f){
                        rb.drag = linearDrag;
                    }
                }
                
                if(rb.velocity.y < 0){                                      //normal falling
                    rb.gravityScale = gravity * fallMultiplier;
                    rb.drag = linearDrag;
                    isFalling = true;                                           //Check Falling
                }else if(rb.velocity.y > 0 && !isJumpHeld){    //jump w/ released jump button
                    rb.gravityScale = gravity * (fallMultiplier);
                }
            }
        }
    }

    void Flip() {                                                           //flip player rotation physically (rendered sprite will flip too)
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    void moveCharacter(float horizontal) {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);
    
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vertical", rb.velocity.y);
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


    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
}
