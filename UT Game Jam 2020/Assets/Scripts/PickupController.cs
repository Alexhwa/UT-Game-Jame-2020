using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupController : MonoBehaviour
{
    public enum PickUpState
    {
        Idle, PickedUp
    }
    public PickUpState state = PickUpState.Idle;

    public virtual void PickUp(SwordController sword)
    {
        transform.localRotation = sword.transform.rotation;
        state = PickUpState.PickedUp;
    }
    public virtual void Discard()
    {
        transform.eulerAngles = Vector3.zero;
        state = PickUpState.Idle;
    }
}
