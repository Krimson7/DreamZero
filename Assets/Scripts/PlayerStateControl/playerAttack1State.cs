using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack1State : playerBaseState
{

    public playerAttack1State(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        Ctx.animator.Play("Player_Attack");
        
        Debug.Log("Attacking_Ground");
        Ctx.isAttacking = true;
        Attakkuu();
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){

    }

    public override void CheckSwitchStates(){
        if(!Ctx.isAttacking){
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState(){}

    
    void Attakkuu(){
        
        Ctx.Invoke("AttackComplete", Ctx.attackDelay);
    }
}
