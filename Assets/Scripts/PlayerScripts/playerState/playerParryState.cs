using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParryState : playerBaseState
{
    public playerParryState (playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        // Debug.Log("Help me");
        Ctx.parryTimer = 0;
        Ctx.isParrying = true;
        // Debug.Log("set true start parry");
        Ctx.canParry = false;
        Ctx.parrySuccess = false;
        Ctx.parryState= true;

        // Ctx.Invoke("startParry",0);     
        var SpiritAttack = Ctx.characterHolder.GetComponent<IplayerParryState>();
        if(SpiritAttack == null) {
            Debug.Log("No parries found on this character");
        }else
            SpiritAttack.Parry();
        
        Ctx.Invoke("ParryComplete", Ctx.parryFullCD);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){

        // Ctx.isParrying = false;
    }

    public override void CheckSwitchStates(){
        if(!Ctx.parryState)
            SwitchState(Factory.Grounded());
        
    }

    public override void InitializeSubState(){}


}
