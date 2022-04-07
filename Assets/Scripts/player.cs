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

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;
    public BoxCollider2D boxCollider2d;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.1f;
    public Vector3 colliderOffset;

    // Update is called once per frame
    void Update() {
        bool wasOnGround = onGround;
        

        // if(!wasOnGround && onGround){
        //     // StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        // }

        if(Input.GetButtonDown("Jump")){
            jumpTimer = Time.time + jumpDelay;
        }
        animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate() {
        onGround = isGrounded();
        moveCharacter(direction.x);
        if(jumpTimer > Time.time && onGround){
            Jump();
        }
        
        modifyPhysics();
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
    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        // StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
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
        }else{
            rb.gravityScale = gravity;
            if(rb.velocity.y > 0){
                float velX = Mathf.Abs(rb.velocity.x);
                if(velX < 0.5f){
                    rb.drag = linearDrag*0.1f;
                } else if(velX >= 0.5f){
                    rb.drag = linearDrag;
                }
            }
            
            if(rb.velocity.y < 0){
                rb.gravityScale = gravity * fallMultiplier;
            }else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip() {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    // IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {
    //     Vector3 originalSize = Vector3.one;
    //     Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
    //     float t = 0f;
    //     while (t <= 1.0) {
    //         t += Time.deltaTime / seconds;
    //         characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
    //         yield return null;
    //     }
    //     t = 0f;
    //     while (t <= 1.0) {
    //         t += Time.deltaTime / seconds;
    //         characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
    //         yield return null;
    //     }
    // }
    
    private bool isGrounded(){
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
}