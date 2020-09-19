using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float jumpCooldown;
    public float jumpEndLag;
    public MoveState moveState = MoveState.NotMoving; 

    public enum MoveState
    {
        NotJumping, Jumping, JumpEndLag
    }

    public override void Start()
    {
        base.Start();
    }
    public override void MoveTowardPlayer()
    {
        anim.SetTrigger("Jumping");
        var dirAtPlayer = player.transform.position - transform.position;
        
        //REPLACE THIS LINE WITH JUMP ANIMATION
        transform.DOMove(transform.position + Vector3.Normalize(dirAtPlayer) * moveSpeed / 10, .1f);
        
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
        enemyState = MoveState.JumpEndLag;
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
