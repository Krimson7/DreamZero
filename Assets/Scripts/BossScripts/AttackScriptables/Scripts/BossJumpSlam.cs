using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpSlamMove", menuName = "BossAttack/JumpSlam", order = 0)]
public class BossJumpSlam : BossAttackScriptables
{
    enum State{
        Jump,
        Hover,
        Fall,
        Delay,
    }

    [SerializeField] State state;
    public AnimationClip JumpAnim;
    public AnimationClip JumpToFallAnim;
    public AnimationClip FallAnim;
    public AnimationClip DelayAnim;
    
    public float jumpHeight;
    public float jumpSpeed = 10f;
    // public float atk;
    float hoverTimer;
    public float hoverTime;
    float delayTimer;
    public float AttackDelay;
    Vector3 playerStartPos;
    Vector3 bossStartPos;
    Vector3 targetPos;
    [SerializeField] bool firstTrigger = false;

    int facingRight = 1;


    public override string Attack(BossBehavior boss)
    {
        if(!firstTrigger){
            firstTrigger = true;
            playerStartPos = boss.playerInRange.transform.position;
            bossStartPos = boss.transform.position;
            targetPos = new Vector3(playerStartPos.x, bossStartPos.y + jumpHeight, 0);
            state = State.Jump;
        }

        facingRight = boss.transform.localScale.x > 0 ? 1 : -1;
        if(boss.playerInRange == null){
            return "Go Idle";
        }

        switch(state){
            case State.Jump:
                boss.getAnimator().Play(JumpAnim.name);
                boss.getRigidbody().isKinematic = true;
                targetPos = new Vector3(boss.playerInRange.transform.position.x, bossStartPos.y + jumpHeight, 0);
                boss.transform.position = Vector3.MoveTowards(boss.transform.position, targetPos, jumpSpeed * Time.deltaTime);
                if((boss.transform.position.x >= targetPos.x - .1f && boss.transform.position.x <= targetPos.x + .1f) && (boss.transform.position.y >= targetPos.y - .1f && boss.transform.position.y <= targetPos.y + .1f)){
                    state = State.Hover;
                }
                return "No changes";
            case State.Hover:
                boss.getRigidbody().isKinematic = true;
                boss.getAnimator().Play(JumpToFallAnim.name);
                hoverTimer += Time.deltaTime;
                if(hoverTimer >= JumpToFallAnim.length){
                    state = State.Fall;
                }
                return "No changes";
            
            case State.Fall:
                boss.getAnimator().Play(FallAnim.name);
                boss.getRigidbody().isKinematic = false;
                boss.getRigidbody().velocity = new Vector2(0, -jumpSpeed);
                if(boss.checkPit){
                    state = State.Delay;
                }
                return "No changes";
            case State.Delay:
                delayTimer += Time.deltaTime;
                if(delayTimer >= 0.5f){
                    boss.getAnimator().Play(DelayAnim.name);
                }
                if(delayTimer >= AttackDelay){
                    delayTimer = 0;
                    return "Exit Attack";
                }
                return "No changes";
        }
        return "No changes";
    }

    public void Flip(Transform boss){
        boss.localScale = new Vector2(boss.localScale.x * -1, boss.localScale.y);
        facingRight *= -1;
    }

    public override void Reset() {
        state = State.Jump;
        firstTrigger = false;
        delayTimer = 0f;
    }
}
