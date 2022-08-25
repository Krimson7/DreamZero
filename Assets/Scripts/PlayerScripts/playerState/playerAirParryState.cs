using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAirParryState : playerBaseState
{
    public playerAirParryState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        // Ctx.animator.Play("Player_Attack");
        
        // Debug.Log("Attacking_Air");
        Ctx.isParrying = true;

        var SpiritAttack = Ctx.characterHolder.GetComponent<IplayerParryState>();
        if(SpiritAttack == null) {
            Debug.Log("No parries found on this character");
        }
        SpiritAttack.Parry();
        Ctx.Invoke("ParryComplete", Ctx.parryTimer);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(!Ctx.isParrying){
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState(){}
}
