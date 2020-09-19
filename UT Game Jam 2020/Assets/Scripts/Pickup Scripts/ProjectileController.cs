using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : PickupController
{
    public float speed;
    public float projDamage;
    public float dieTime;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Die(dieTime));
    }

    private void FixedUpdate()
    {
        if(state == PickUpState.Idle)
        {
            transform.DOLocalMove(transform.position + transform.up * speed / 20, .1f);
        }
    }
    public override void PickUp(SwordController sword)
    {
        base.PickUp(sword);
        transform.localEulerAngles = Vector3.zero;
        transform.DOKill();
    }
    public override void Discard()
    {
        transform.SetParent(null);
        playerOwned = true;
        transform.localScale = Vector3.one;
        state = PickUpState.Idle;
        speed *= 3f;
    }
    IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.DOKill();
        Destroy(gameObject);
    }
}
