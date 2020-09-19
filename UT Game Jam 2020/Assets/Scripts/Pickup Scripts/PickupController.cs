using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class PickupController : MonoBehaviour
{
    public enum PickUpState
    {
        Idle, PickedUp, Disabled
    }
    public PickUpState state = PickUpState.Idle;
    public static GameObject player;

    public virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public virtual void PickUp(SwordController sword)
    {
        transform.DOKill();
        transform.SetParent(sword.bladePos);
        transform.localPosition = Vector3.zero;
        transform.localRotation = sword.transform.rotation;
        state = PickUpState.PickedUp;
    }
    public virtual void Discard()
    {
        //Orphanize
        transform.SetParent(null);
        //Prevent any weird scaling
        transform.localScale = Vector3.one;
        state = PickUpState.Idle;
    }
    public virtual bool IsPickupable()
    {
        return true;
    }
}
