using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MeleeSwing", menuName = "BossAttack/MeleeSwing", order = 0)]
public class BossMeleeSwing : BossAttackScriptables
{
    enum State{
        Initiate,
        Rush,
        Swing,
    }

    [SerializeField] State state;

    // Rigidbody2D rb;
    // public BoxCollider2D meleeHitBox;
    public AnimationClip runAnim;
    public AnimationClip chargeAnim;
    public AnimationClip SwingAnim;


    
    
    // public float atk;
    float timer;     
    public float startDelay;
    float rushSpeed;  
    public float maxRushSpeed;
    public float rushAccel;
    float delayTimer;
    public float AttackDelay;
    [SerializeField] bool firstTrigger = false;
    [SerializeField] bool swinging = false;

    int facingRight = 1;

    public override string Attack(BossBehavior boss)
    {
        if(!firstTrigger){
            firstTrigger = true;
            state = State.Initiate;
        }

        facingRight = boss.transform.localScale.x > 0 ? 1 : -1;
        if(boss.playerInRange == null){
            return "Go Idle";
        }

        switch(state){
            case State.Initiate:
                boss.getAnimator().Play(chargeAnim.name);
                if(timer <= startDelay){
                    timer += Time.fixedDeltaTime;
                    return "No changes";
                }
                timer = 0f;
                state = State.Rush;
                return "No changes";
            case State.Rush:
                boss.getAnimator().Play(runAnim.name);
                if(boss.getCheckWall()){
                    Debug.Log("flip");
                    Flip(boss.transform);
                    return "No changes";
                }
                if((boss.playerInRange.transform.position.x > boss.transform.position.x) ^ facingRight == 1){
                    Debug.Log("flip");
                    Flip(boss.transform);
                    return "No changes";
                }
                else if(Mathf.Abs(boss.playerInRange.transform.position.x - boss.transform.position.x) <= 1f){
                    swinging = false;
                    state = State.Swing;
                    return "No changes";
                }
                else{
                    if(rushSpeed < maxRushSpeed){
                        boss.getRigidbody().velocity = new Vector2(rushSpeed * facingRight, boss.getRigidbody().velocity.y);
                        rushSpeed += rushAccel;
                    }else{
                        boss.getRigidbody().velocity = new Vector2(maxRushSpeed * facingRight, boss.getRigidbody().velocity.y);
                    }
                }
                
                return "No changes";
            case State.Swing:
                if(swinging == false){
                    boss.getRigidbody().velocity = new Vector2(0, boss.getRigidbody().velocity.y);
                    swinging = true;
                    boss.getAnimator().Play(SwingAnim.name);
                    return "No changes";
                    // Debug.Log("Swinging");
                }
                if(delayTimer<= SwingAnim.length + AttackDelay){
                    delayTimer+= Time.fixedDeltaTime;
                    return "No changes";
                }else if(delayTimer > SwingAnim.length + AttackDelay){
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
        state = State.Initiate;
        firstTrigger = false;
        timer = 0f;
        delayTimer = 0f;
        rushSpeed = 0f;
    }
}
