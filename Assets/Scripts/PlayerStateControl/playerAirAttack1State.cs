using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAirAttack1State : playerBaseState
{
    public playerAirAttack1State(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        Ctx.animator.Play("Player_Attack");
        
        Debug.Log("Attacking_Air");
        Ctx.isAttacking = true;
        Attakkuu();
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(!Ctx.isAttacking){
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){}

    void Attakkuu(){
        
        Ctx.Invoke("AttackComplete", Ctx.attackDelay);
    }
}
