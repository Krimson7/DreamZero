using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWallSlideState : playerBaseState
{
    public playerWallSlideState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
        Ctx.animator.Play("Player_WallSlide");
    }

    public override void EnterState(){

        WallCling();
    }

    public override void UpdateState(){
        CheckSwitchStates();
        
        // modifyPhysics();
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(Ctx.onGround){
            SwitchState(Factory.Grounded());
        }else if(Ctx.isJumpHeld && !Ctx.requireNewJumpPress){
            SwitchState(Factory.WallJump());
        }else if(Ctx.clinging && ((Ctx.facingRight && Ctx._currentMovement.x<0.1) || (!Ctx.facingRight && Ctx._currentMovement.x>-0.1))){
            SwitchState(Factory.Fall());
        }else if(!Ctx.clinging){
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){}

    void WallCling() {
        Ctx.requireNewJumpPress = false;
        Ctx.rb.velocity = new Vector2(0f, 0f);
    }

    void modifyPhysics() {
        Ctx.rb.gravityScale = Ctx.gravity;
        Ctx.rb.drag = Ctx.linearDrag * 4;
        // if(Ctx.rb.velocity.y > 0){
        //     float velX = Mathf.Abs(Ctx.rb.velocity.x);
        //     if(velX < 0.5f){
        //         Ctx.rb.drag = Ctx.linearDrag*0.3f;
        //     } else if(velX >= 0.5f){
                
        //     }
        // }
        // if(Ctx.rb.velocity.y > 0 && !Ctx.isJumpHeld){    //jump w/ released jump button
        //     Ctx.rb.gravityScale = Ctx.gravity * (Ctx.fallMultiplier);
        // }
    }

}
