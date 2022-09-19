using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParryState : playerBaseState
{
    bool a;
    public playerUseSpirit pus;

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
        var SpiritAttack = Ctx.characterHolder.GetComponent<playerUseSpirit>();
        if(SpiritAttack == null) {
            Debug.Log("No parries found on this character");
        }else{
            int fr = Ctx.facingRight? 1 : -1;
            a = SpiritAttack.Parry(fr, Ctx.rb);
            // if(a && Ctx.parrySuccess == true){
                
            // }
        }
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
        if(a && Ctx.parrySuccess == true){
            // pus.animator.Play(pus.player);
            Ctx.rb.velocity = new Vector2(0, 0);
            int fr = Ctx.facingRight? -1 : 1;
            Ctx.rb.AddForce(new Vector2(fr,1) * Ctx.jumpSpeed, ForceMode2D.Impulse);
            SwitchState(Factory.Fall());
        }
        if(!Ctx.parryState){
            
            SwitchState(Factory.Grounded());
        }
            
        
    }

    public override void InitializeSubState(){}


}
