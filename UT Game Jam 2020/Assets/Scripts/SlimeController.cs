using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    [Header("Slime Variables")]
    public float jumpCooldown;
    public float jumpEndLag;
    public MoveState moveState = MoveState.NotJumping;     
    
    public enum MoveState
    {
        NotMoving, NotJumping, Jumping, JumpEndLag
    }
    public override void Start()
    {
        base.Start();
    }
    
    public override void MoveTowardPlayer()
    {
        anim.SetTrigger("Jumping");
        var dirAtPlayer = player.transform.position - transform.position;
        
        Debug.Log("Successful call to jump animation.");
        
        ResetJump(jumpCooldown);
        enemyState = EnemyState.NotAttacking;
    }
    public override bool IsPickupable()
    {
        return moveState != MoveState.Jumping;
    }
    protected IEnumerator ResetJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveState = MoveState.JumpEndLag;
        StartCoroutine(ResetJumpLag(jumpEndLag));
    }
    protected IEnumerator ResetJumpLag(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveState = MoveState.NotJumping;
    }
    public override void Attack()
    {
        enemyState = EnemyState.Attacking;
    }
}
