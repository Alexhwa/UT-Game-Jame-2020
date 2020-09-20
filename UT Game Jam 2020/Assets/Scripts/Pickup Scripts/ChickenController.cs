using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : EnemyController
{
    [Header("Chicken Variables")]
    public float wanderInterval;
    public float pauseTime;
    public MoveState moveState = MoveState.Idle;

    public enum MoveState
    {
        Idle, Wandering, Paused
    }
    public override void Start()
    {
        base.Start();
    }

    public override void MoveTowardPlayer()
    {
        if (moveState == MoveState.Idle)
        {
            //Jump
            anim.SetBool("Walking", true);
            rb.velocity = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
            moveState = MoveState.Wandering;
            StartCoroutine(ResetWander(Random.Range(wanderInterval/ 2, wanderInterval)));
        }
    }
    protected IEnumerator ResetWander(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        anim.SetBool("Walking", false);
        moveState = MoveState.Paused;
        StartCoroutine(ResetJumpLag(Random.Range(pauseTime/2, pauseTime)));
    }
    protected IEnumerator ResetJumpLag(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveState = MoveState.Idle;
    }
    public override bool IsPickupable()
    {
        return true;
    }
}
