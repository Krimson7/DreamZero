using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRunState : playerBaseState
{
    public playerRunState(playerStateMachine currentContext, playerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        var aniCon = Ctx.characterHolder.GetComponent<IAnimatorControl>();
        aniCon.animate("Run");
        // Debug.Log("Running");
    }

    public override void UpdateState(){
        CheckSwitchStates();
        moveCharacter(Ctx.currentMovement.x);
        modifyPhysics();
    }

    public override void ExitState(){}

    public override void CheckSwitchStates(){
        if(Ctx.Attack1KeyDown){
            SwitchState(Factory.Attack1());
        } else if(Mathf.Abs(Ctx.currentMovement.x)<=0.5){ 
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState(){}

    void Flip() {    
        Ctx.animator.Play("Player_Running");                                                       //flip player rotation physically (rendered sprite will flip too)
        Ctx.facingRight = !Ctx.facingRight;
        Ctx.transform.rotation = Quaternion.Euler(0, Ctx.facingRight ? 0 : 180, 0);
    }

    void moveCharacter(float horizontal) {
        Ctx.rb.AddForce(Vector2.right * horizontal * Ctx.moveSpeed);
    
        if ((horizontal > 0 && !Ctx.facingRight) || (horizontal < 0 && Ctx.facingRight)) {
            Flip();
        }

        if (Mathf.Abs(Ctx.rb.velocity.x) > Ctx.maxSpeed) {
            Ctx.rb.velocity = new Vector2(Mathf.Sign(Ctx.rb.velocity.x) * Ctx.maxSpeed, Ctx.rb.velocity.y);
        }
    }

    void modifyPhysics() {
        bool changingDirections = (Ctx.currentMovement.x > 0 && Ctx.rb.velocity.x < 0) || (Ctx.currentMovement.x < 0 && Ctx.rb.velocity.x > 0);

        if (Mathf.Abs(Ctx.currentMovement.x) < 0.4f || changingDirections) {
            Ctx.rb.drag = Ctx.linearDrag*2f;
        } else {
            Ctx.rb.drag = 0f;
        }
        Ctx.rb.gravityScale = 1f;
        // Ctx.isFalling = false;
        
    }
}
