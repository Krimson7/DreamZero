using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAirParryState : playerBaseState
{
    public playerAirParryState (playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }
    bool a;

    public override void EnterState(){
        // Debug.Log("Help me");
        Ctx.parryTimer = 0;
        Ctx.isParrying = true;
        // Debug.Log("set true start parry");
        Ctx.canParry = false;
        Ctx.parrySuccess = false;
        Ctx.parryState= true;

        // Ctx.Invoke("startParry",0);     
        var SpiritAttack = Ctx.characterHolder.GetComponent<playerUseSpirit>();
        if(SpiritAttack == null) {
            Debug.Log("No parries found on this character");
        }else{
            int fr = Ctx.facingRight? 1 : -1;
            a = SpiritAttack.Parry(fr, Ctx.rb);
        }
        Ctx.Invoke("ParryComplete", Ctx.parryFullCD);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = 1;
    }

    public override void ExitState(){

        // Ctx.isParrying = false;
    }

    public override void CheckSwitchStates(){
        if(a && Ctx.parrySuccess == true){
            Ctx.rb.velocity = new Vector2(0, 0);
            int fr = Ctx.facingRight? -1 : 1;
            Ctx.rb.AddForce(new Vector2(fr,1) * Ctx.jumpSpeed, ForceMode2D.Impulse);
            SwitchState(Factory.Fall());
        }
        if(!Ctx.parryState)
            SwitchState(Factory.Fall());
    }

    public override void InitializeSubState(){}


}
