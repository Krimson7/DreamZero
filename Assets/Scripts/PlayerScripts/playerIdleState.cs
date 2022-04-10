using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIdleState : playerBaseState
{
    public playerIdleState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        Ctx.animator.Play("Player_Idle");
        // Debug.Log("Idling");
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(Ctx.Attack1KeyDown){
            SwitchState(Factory.Attack1());
        } else if(Mathf.Abs(Ctx.currentMovement.x)>0.5){ 
            SwitchState(Factory.Run());
        }
    }

    public override void InitializeSubState(){}
}
