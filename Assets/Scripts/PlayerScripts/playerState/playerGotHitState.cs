using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGotHitState : playerBaseState
{
    public playerGotHitState (playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState(){

        // Ctx.isInvincible = true;
        // Ctx.Invoke("invincibleComplete", Ctx.invincibleTimer);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        // Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){
        var aniCon = Ctx.characterHolder.GetComponent<IAnimatorControl>();
        aniCon.animate("Fall");
        int FR = Ctx.facingRight?1:-1;
        Ctx.rb.AddForce(Vector2.right * FR * -1 * Ctx.knockbackForce, ForceMode2D.Impulse);
    }

    public override void CheckSwitchStates(){
        if(Ctx.onGround){
            SwitchState(Factory.Idle());
        } else {
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){}


}
