using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundedState : playerBaseState
{
    public playerGroundedState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState(){
        Debug.Log("now started as grounded");
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(Ctx.isJumpHeld && !Ctx.requireNewJumpPress ){
            SwitchState(Factory.Jump());
        }else if(!Ctx.onGround && !Ctx.isJumpHeld){
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){
        if(Ctx.Attack1KeyDown){
            SetSubState(Factory.Attack1());
        } else if(Mathf.Abs(Ctx.currentMovement.x)>=0.5 ){  //it supposed to be the opposite. But to let it update itself on landing, we must do this to encourage them to swap
            SetSubState(Factory.Idle());
        } else{
            SetSubState(Factory.Run());
        }
    }
}
