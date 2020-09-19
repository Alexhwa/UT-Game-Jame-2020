using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : PickupController
{
    public float speed;
    public float damage;
    public float dieTime;

    public bool playerOwned = false;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Die(dieTime));
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
        base.PickUp(sword);
        transform.localEulerAngles = Vector3.zero;
        transform.DOKill();
        playerOwned = true;
        
    }
    public override void Discard()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        state = PickUpState.Idle;
        speed *= 3f;
    }
    IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
