﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : PickupController
{
    public float health;
    public float moveSpeed;
    public float attackRange;

    private Animator anim;

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
        if(Vector2.Distance(player.transform.position, transform.position) < attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardPlayer();
        }
    }
    public virtual void MoveTowardPlayer()
    {
        var dirAtPlayer = player.transform.position - transform.position;
        transform.DOMove(transform.position + Vector3.Normalize(dirAtPlayer) * moveSpeed / 10, .1f);
    }
    public virtual void Attack()
    {

    }
}
