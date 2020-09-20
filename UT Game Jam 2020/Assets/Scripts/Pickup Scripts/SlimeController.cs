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
         NotJumping, Jumping, JumpEndLag
    }
    public override void Start()
    {
        base.Start();
    }
    
    public override void MoveTowardPlayer()
    {
        if (moveState == MoveState.NotJumping)
        {
            //Jump
            anim.SetTrigger("Jumping");
            var dirAtPlayer = player.transform.position - transform.position;
            dirAtPlayer = Vector3.Normalize(dirAtPlayer);
            rb.velocity = dirAtPlayer * moveSpeed;
            moveState = MoveState.Jumping;
            StartCoroutine(ResetJump(jumpCooldown));
        }
    }
    public override bool IsPickupable()
    {
        return true;
    }
    protected IEnumerator ResetJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        moveState = MoveState.JumpEndLag;
        StartCoroutine(ResetJumpLag(jumpEndLag));
    }
    protected IEnumerator ResetJumpLag(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveState = MoveState.NotJumping;
    }
}
