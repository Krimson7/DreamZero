using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpecialState : playerBaseState
{

    public playerSpecialState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        Ctx.isSpecialing = true;

        var SpiritSpecial = Ctx.characterHolder.GetComponent<playerUseSpirit>();
        if(SpiritSpecial == null) {
            Debug.Log("No Specials found on this character");
        }
        int fr = Ctx.facingRight? 1 : -1;
        SpiritSpecial.Special(Ctx._effectSpawnPoint.position, fr);
        Ctx.Invoke("SpecialComplete", Ctx.specialDelay);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        // if(Ctx.invincibleTimer > 0){
        //     SwitchState(Factory.GotHit());
        // } else 
        if(!Ctx.isSpecialing){
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState(){}


}
