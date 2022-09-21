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

    public AnimationClip chargeAnim;
    public AnimationClip SwingAnim;


    
    
    public float atk;
    public float timer;      //temp
    public float startDelay;
    public float rushSpeed;  //temp
    public float maxRushSpeed;
    public float rushAccel;

    [SerializeField]bool swinging = false;

    int facingRight = 1;

    public override string Attack(BossBehavior boss)
    {

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
                boss.getAnimator().Play(chargeAnim.name);
                if((boss.playerInRange.transform.position.x > boss.transform.position.x) ^ facingRight == 1){
                    Flip(boss.transform);
                    return "No changes";
                }else if(Mathf.Abs(boss.playerInRange.transform.position.x - boss.transform.position.x) <= 1f){
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
                    swinging = true;
                    boss.getAnimator().Play(SwingAnim.name);
                    Debug.Log("Swinging");
                }
                if(timer <= SwingAnim.length +1){
                    timer += Time.fixedDeltaTime;
                    return "No changes";
                }
                timer = 0f;
                swinging = false;
                
                state = State.Initiate;
                return "Go Delay 1";
        }
        return "No changes";
    }

    public void Flip(Transform boss){
        boss.localScale = new Vector2(boss.localScale.x * -1, boss.localScale.y);
        facingRight *= -1;
    }
}
