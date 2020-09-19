using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnvironmentController : PickupController
{
    public override void Discard()
    {
        transform.DOLocalMove(transform.position + transform.parent.up * 4, .3f).SetEase(Ease.OutCubic);
        base.Discard();
        playerOwned = true;
        StartCoroutine(ResetPlayerOwned(.3f));
        //Push away on release
        //Return object to original rotation
        transform.eulerAngles = Vector3.zero;
    }
}
