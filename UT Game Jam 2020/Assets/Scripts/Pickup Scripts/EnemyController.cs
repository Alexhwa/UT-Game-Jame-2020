using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : PickupController
{
    public float health;
    public float moveSpeed;
    public float attackRange;
    public float attackCooldown;
    public float attackEndLag;

    private Animator anim;
    public EnemyState enemyState = EnemyState.NotAttacking; 

    public enum EnemyState
    {
        NotAttacking, Attacking, AttackEndLag
    }

    public override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("Offset", Random.Range(0f, 3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == PickUpState.Idle)
        {
            DoEnemyAI();
        }
    }
    public virtual void DoEnemyAI()
    {
        if (enemyState != EnemyState.Attacking && enemyState != EnemyState.AttackEndLag)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < attackRange)
            {
                Attack();
            }
            else
            {
                MoveTowardPlayer();
            }
        }
    }
    public virtual void MoveTowardPlayer()
    {
        var dirAtPlayer = player.transform.position - transform.position;
        transform.DOMove(transform.position + Vector3.Normalize(dirAtPlayer) * moveSpeed / 10, .1f);
    }
    public virtual void Attack()
    {
        enemyState = EnemyState.Attacking;
        StartCoroutine(ResetAttacking(attackCooldown));
    }
    public override bool IsPickupable()
    {
        return enemyState != EnemyState.Attacking;
    }
    public override void Discard()
    {
        //Push away on release
        transform.DOLocalMove(transform.position + transform.parent.up * 4, .3f).SetEase(Ease.OutCubic);
        base.Discard();
        //Return object to original rotation
        transform.eulerAngles = Vector3.zero;
    }
    protected IEnumerator ResetAttacking(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyState = EnemyState.AttackEndLag;
        StartCoroutine(ResetEndLag(attackEndLag));
    }
    protected IEnumerator ResetEndLag(float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyState = EnemyState.NotAttacking;
    }
    
}
