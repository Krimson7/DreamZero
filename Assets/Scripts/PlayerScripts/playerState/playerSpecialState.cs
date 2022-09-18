using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpecialState : playerBaseState
{
    public Collider2D enemyInMeleeRange;
    public playerUseSpirit pus;
    public bool atkHit;

    public playerSpecialState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        Ctx.isSpecialing = true;
        atkHit = false;

        pus = Ctx.characterHolder.GetComponent<playerUseSpirit>();
        if(pus == null) {
            Debug.Log("No Specials found on this character");
        }
        int fr = Ctx.facingRight? 1 : -1;
        Ctx.rb.gravityScale = 0;
        pus.Special(Ctx._effectSpawnPoint.position, fr, Ctx.rb, Ctx.gameObject);
        Ctx.Invoke("SpecialComplete", Ctx.specialDelay);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        enemyInMeleeRange = Physics2D.OverlapCircle(Ctx.transform.position, 0.5f, Ctx.enemyLayer);
        if(pus.player.meleeSpecialAtk){
            if(enemyInMeleeRange != null && !atkHit){
                enemyInMeleeRange.GetComponent<enemyHp>().takeDamage(pus.player.specialAtkValue);
                Ctx.Invoke("SpecialComplete", 0f);
                Debug.Log("hit" + pus.player.specialAtkValue);
                Ctx.rb.gravityScale = Ctx.gravity;

                atkHit = true;
            }
        }
        
        // Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        // if(Ctx.invincibleTimer > 0){
        //     SwitchState(Factory.GotHit());
        // } else 
        if(atkHit){
            SwitchState(Factory.WallJump());
        }
        if(!Ctx.isSpecialing){
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState(){}



}
