using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : PickupController
{
    public float speed;
    public float damage;

    public bool playerOwned = false;

    private void Start()
    {
    }

    private void Update()
    {
        if(state == PickUpState.Idle)
        {
            print("Moving up");
            transform.DOLocalMove(transform.position + transform.up * speed / 10, .1f);
        }
    }
    public override void PickUp(SwordController sword)
    {
        transform.rotation = sword.transform.rotation;
        transform.DOKill();
        playerOwned = true;
        state = PickUpState.PickedUp;
    }
    public override void Discard()
    {
        state = PickUpState.Idle;
        speed *= 2f;
    }
}
