using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJumpState : playerBaseState
{
    // private float _jumpTimer;
    private bool nowInAir = false;

    public playerJumpState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }
    
    public override void EnterState(){
        // Debug.Log("Jumping");
        var aniCon = Ctx.characterHolder.GetComponent<IAnimatorControl>();
        aniCon.animate("Jump");
        Jump();
        if(Ctx.isJumpHeld){
            Ctx.requireNewJumpPress = true;
        }
        nowInAir = false;
    }

    public override void UpdateState(){
        CheckSwitchStates();
        moveCharacter(Ctx.currentMovement.x);
        if(!Ctx.onGround){
            nowInAir = true;
        }
        modifyPhysics();
    }

    public override void ExitState(){
        
    }

    public override void CheckSwitchStates(){
        if(Ctx.Attack1KeyDown){
            SwitchState(Factory.AirAttack1());
        } else if(Ctx.ParryKeyDown){
            SwitchState(Factory.AirParry());
        }  else if(Ctx.onGround && nowInAir){
            SwitchState(Factory.Grounded());
        // } else if(Ctx.clinging && ((Ctx.facingRight && Ctx._currentMovement.x>0.1) || (!Ctx.facingRight && Ctx._currentMovement.x<-0.1))){
        //     SwitchState(Factory.WallSlide());
        } else if(Ctx.rb.velocity.y<0.5f){
            SwitchState(Factory.Fall());
        } 
    }


    public override void InitializeSubState(){}

    void Flip() {                                                           //flip player rotation physically (rendered sprite will flip too)
        Ctx.facingRight = !Ctx.facingRight;
        Ctx.transform.rotation = Quaternion.Euler(0, Ctx.facingRight ? 0 : 180, 0);
    }

    void moveCharacter(float horizontal) {
        Ctx.rb.AddForce(Vector2.right * horizontal * Ctx.moveSpeed);
    
        if ((horizontal > 0 && !Ctx.facingRight) || (horizontal < 0 && Ctx.facingRight)) {
            Flip();
        }

        if (Mathf.Abs(Ctx.rb.velocity.x) > Ctx.maxSpeed) {
            Ctx.rb.velocity = new Vector2(Mathf.Sign(Ctx.rb.velocity.x) * Ctx.maxSpeed, Ctx.rb.velocity.y);
        }
    }

    void Jump(){
        
        // Ctx.animator.Play("Player_Jump");
        Ctx.rb.velocity = new Vector2(Ctx.rb.velocity.x, 0);
        Ctx.rb.AddForce(Vector2.up * Ctx.jumpSpeed, ForceMode2D.Impulse);
        // _jumpTimer = 0;
        Ctx.StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) {    //Enumerator for jumping squeeze effect
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            Ctx.characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            Ctx.characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    void modifyPhysics() {
        Ctx.rb.gravityScale = Ctx.gravity;
        if(Ctx.rb.velocity.y > 0){
            float velX = Mathf.Abs(Ctx.rb.velocity.x);
            if(velX < 0.5f){
                Ctx.rb.drag = Ctx.linearDrag*0.3f;
            } else if(velX >= 0.5f){
                Ctx.rb.drag = Ctx.linearDrag;
            }
        }
        if(Ctx.rb.velocity.y > 0 && !Ctx.isJumpHeld){    //jump w/ released jump button
            Ctx.rb.gravityScale = Ctx.gravity * (Ctx.fallMultiplier);
        }
    }

}
