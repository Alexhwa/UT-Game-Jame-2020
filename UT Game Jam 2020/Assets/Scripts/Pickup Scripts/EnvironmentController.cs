using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnvironmentController : PickupController
{
    public override void Start()
    {
        base.Start();
    }

    public override void Discard()
    {
        //Push away on release
        rb.velocity = transform.parent.up * 20;
        base.Discard();
        playerOwned = true;
        StartCoroutine(ResetPlayerOwned(.3f));
        //Push away on release
        //Return object to original rotation
        transform.eulerAngles = Vector3.zero;
    }
}
