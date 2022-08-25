using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParryState : playerBaseState
{
    public playerParryState (playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        // Debug.Log("Help me");
        Ctx.isParrying = true;
        // Ctx.Invoke("startParry",0);     
        var SpiritAttack = Ctx.characterHolder.GetComponent<IplayerParryState>();
        if(SpiritAttack == null) {
            Debug.Log("No parries found on this character");
        }
        SpiritAttack.Parry();
        Ctx.Invoke("ParryComplete", Ctx.parryTimer);




        // SpiritAttack.Attack(Ctx.atk);
        // Ctx.Invoke("AttackComplete", Ctx.attackDelay);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){
        Debug.Log("Kill me");

        // Ctx.isParrying = false;
    }

    public override void CheckSwitchStates(){
        if(!Ctx.isParrying){
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState(){}


}
