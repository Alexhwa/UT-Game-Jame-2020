using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : PickupController
{
    [Header("Basic Enemy Variables")]
    public int health = 2;
    public float moveSpeed = 3;
    public float attackRange = 1;
    protected float attackCooldown = 1;
    public float attackEndLag = 2;

    protected Animator anim;
    public EnemyState enemyState = EnemyState.NotAttacking;

    protected bool pushedBack;
    public enum EnemyState
    {
        NotAttacking, Attacking, AttackEndLag
    }

    public override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("Offset", Random.Range(0f, 3f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickup") && state != PickUpState.PickedUp)
        {
            if (collision.GetComponent<PickupController>().DoesDamage() && collision.gameObject != gameObject && !pushedBack)
            {
                GetHit(collision.GetComponentInChildren<PickupController>());
            }
        }
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
                if (pushedBack)
                {
                    return;
                }
                MoveTowardPlayer();
            }
        }
    }
    public virtual void MoveTowardPlayer()
    {
        
        var dirAtPlayer = player.transform.position - transform.position;
        rb.velocity = Vector3.Normalize(dirAtPlayer) * moveSpeed / 10;
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
    public override void PickUp(SwordController sword)
    {
        base.PickUp(sword);
    }
    public override void Discard()
    {
        //Push away on release
        rb.velocity = transform.parent.up * 20;
        base.Discard();
        playerOwned = true;
        StartCoroutine(ResetPlayerOwned(.3f));
        //Return object to original rotation
        transform.eulerAngles = Vector3.zero;
        pushedBack = true;
        StartCoroutine(ResetPushedBack());
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
    public virtual void GetHit(PickupController pickup)
    {
        print("Get Hit reached");
        var ps = pickup.GetComponentInChildren<ParticleSystem>();
        if (ps != null) {
            ps.Play();
        }
        TakeRecoil(pickup);
        health--;
        print("Hit. Health = " + health);
        if(health <= 0)
        {
            Die();
        }
    }
    public virtual void TakeRecoil(PickupController hitter)
    {
        //Recoil
        Vector3 recoilForce = transform.position - player.transform.position;
        recoilForce = Vector3.Normalize(recoilForce);
        transform.DOKill();
        rb.velocity = recoilForce * hitter.weaponKnockback;
        pushedBack = true;
        StartCoroutine(ResetPushedBack());
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    protected IEnumerator ResetPushedBack()
    {
        yield return new WaitForSeconds(.75f);
        pushedBack = false;
    }
}
