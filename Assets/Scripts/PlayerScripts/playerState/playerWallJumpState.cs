using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWallJumpState : playerBaseState
{
    public playerWallJumpState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        Debug.Log("Enter Wall Jump");
        if(Ctx.isJumpHeld){
            Ctx.requireNewJumpPress = true;
        }
        Ctx.wallJumpCounter = Ctx.wallJumpTimer;
        WallJump();

    }

    public override void UpdateState(){
        Ctx.wallJumpCounter-=Time.deltaTime;
        if(Ctx.isJumpHeld){
            Ctx.requireNewJumpPress = true;
        }
        modifyPhysics();
        CheckSwitchStates();
    }

    public override void ExitState(){
        // Debug.Log("Exit Wall Jump");
    }

    public override void CheckSwitchStates(){
        // if(Ctx.clinging && ((Ctx.facingRight && Ctx._currentMovement.x<0.1) || (!Ctx.facingRight && Ctx._currentMovement.x>-0.1))){
        if(Ctx.wallJumpCounter<=0){
            Debug.Log("jumped");
            SwitchState(Factory.Fall());
        // }
        // }else if(Ctx.onGround){
        //     Debug.Log("grounded");
        //     SwitchState(Factory.Grounded());
        }else if(!Ctx.clinging && Ctx.wallJumpCounter<=0){
            Debug.Log("Exit by timeout");
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){}

    void WallJump(){
        Flip();
        Ctx.animator.Play("Player_Jump");
        Ctx.rb.velocity = new Vector2(0, 0);
        if(Ctx.facingRight){
            Ctx.rb.AddForce(new Vector2(1,1) * Ctx.jumpSpeed, ForceMode2D.Impulse);
        }else{
            Ctx.rb.AddForce(new Vector2(-1,1) * Ctx.jumpSpeed, ForceMode2D.Impulse);
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

    void Flip() {                                                           //flip player rotation physically (rendered sprite will flip too)
        Ctx.facingRight = !Ctx.facingRight;
        Ctx.transform.rotation = Quaternion.Euler(0, Ctx.facingRight ? 0 : 180, 0);
    }
}
