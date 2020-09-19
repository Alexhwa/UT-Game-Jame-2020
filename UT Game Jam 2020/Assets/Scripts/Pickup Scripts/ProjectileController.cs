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

    private void FixedUpdate()
    {
        if(state == PickUpState.Idle)
        {
            print("Moving up");
            transform.DOLocalMove(transform.position + transform.up * speed / 20, .1f);
        }
    }
    public override void PickUp(SwordController sword)
    {
        transform.SetParent(sword.bladePos);
        transform.localPosition = Vector3.zero;
        transform.rotation = sword.transform.rotation;
        transform.DOKill();
        playerOwned = true;
        state = PickUpState.PickedUp;
    }
    public override void Discard()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        state = PickUpState.Idle;
        speed *= 2f;
    }
}
