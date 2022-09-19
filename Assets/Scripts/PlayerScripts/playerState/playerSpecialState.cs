using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpecialState : playerBaseState
{
    public Collider2D enemyInMeleeRange;
    public playerUseSpirit pus;
    public bool atkHit;
    GameObject trail;

    public playerSpecialState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {
        isRootState = true;
    }

    public override void EnterState(){
        Ctx.isSpecialing = true;
        atkHit = false;
        Ctx.rb.velocity = new Vector2(0,0);
        
        // GameObject Trail = Instantiate(specialPrefab, spawnPoint, Quaternion.identity);
        // trail = Ctx.spawnEffect(pus.player.specialPrefab, Ctx.transform.position);

        pus = Ctx.characterHolder.GetComponent<playerUseSpirit>();
        if(pus == null) {
            Debug.Log("No Specials found on this character");
        }
        int fr = Ctx.facingRight? 1 : -1;
        pus.Special(Ctx._effectSpawnPoint.position, fr, Ctx.rb, Ctx.gameObject);
        Ctx.Invoke("SpecialComplete", Ctx.specialDelay);
    }

    public override void UpdateState(){
        Ctx.rb.gravityScale = 0f;
        Ctx.rb.drag = 0f;
        CheckSwitchStates();
        enemyInMeleeRange = Physics2D.OverlapCircle(Ctx.transform.position, 0.5f, Ctx.enemyLayer);
        if(pus.player.meleeSpecialAtk){
            if(enemyInMeleeRange != null && !atkHit){
                enemyInMeleeRange.GetComponent<enemyHp>().takeDamage(pus.player.specialAtkValue);
                // Ctx.Invoke("SpecialComplete", 0f);
                Debug.Log("hit" + pus.player.specialAtkValue);
                

                atkHit = true;
            }
        }
        
        // Ctx.rb.drag = Ctx.linearDrag*4f;
    }

    public override void ExitState(){
        // Ctx.destroyEffect(trail);
        Ctx.rb.gravityScale = Ctx.gravity;
        pus.destroyEffect();
        Debug.Log("Special Complete");
    }

    public override void CheckSwitchStates(){
        // if(Ctx.invincibleTimer > 0){
        //     SwitchState(Factory.GotHit());
        // } else 
        if(atkHit){
            // Ctx.rb.velocity = new Vector2(0, 0);
            // int fr = Ctx.facingRight? -1 : 1;
            // Ctx.rb.AddForce(new Vector2(fr,1) * 10, ForceMode2D.Impulse);
            
            SwitchState(Factory.WallJump());
        }
        if(!Ctx.isSpecialing){
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState(){}

    // public void SpawnTrailEffect(){
    //     trail = Ctx.spawnEffect(pus.player.specialPrefab, Ctx.transform.position);
    // }

}
