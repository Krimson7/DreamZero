using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFallState : playerBaseState
{
    // private bool nowInAir = false;

    public playerFallState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        var aniCon = Ctx.characterHolder.GetComponent<IAnimatorControl>();
        aniCon.animate("Fall");
        // Debug.Log("Falling");
        // nowInAir = true;
    }

    public override void UpdateState(){
        CheckSwitchStates();
        moveCharacter(Ctx.currentMovement.x);
        modifyPhysics();
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        // if(Ctx.invincibleTimer > 0){
        //     SwitchState(Factory.GotHit());
        // } else 
        if(Ctx.Attack1KeyDown){
            SwitchState(Factory.AirAttack1());
        } else if(Ctx.ParryKeyDown && Ctx.canParry){
            SwitchState(Factory.AirParry());
        } else if(Ctx.isSpecialing){
            SwitchState(Factory.Special());
        } else if(Ctx.onGround){
            SwitchState(Factory.Grounded());
        } else if(Ctx.clinging && ((Ctx.facingRight && Ctx._currentMovement.x>0.1) || (!Ctx.facingRight && Ctx._currentMovement.x<-0.1))){
            SwitchState(Factory.WallSlide());
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
    void modifyPhysics() {
        Ctx.rb.gravityScale = Ctx.gravity;
        if(Ctx.rb.velocity.y > 0){
            float velX = Mathf.Abs(Ctx.rb.velocity.x);
            if(velX < 0.5f){
                Ctx.rb.drag = Ctx.linearDrag*0.3f;
            } else {//if(velX >= 0.5f)
                Ctx.rb.drag = Ctx.linearDrag;
            }
        }
        
        if(Ctx.rb.velocity.y < 0){                                      //normal falling
            Ctx.rb.gravityScale = Ctx.gravity * Ctx.fallMultiplier;
            Ctx.rb.drag = Ctx.linearDrag;
            // Ctx._isFalling = true;                                           //Check Falling
        }
    }
}
