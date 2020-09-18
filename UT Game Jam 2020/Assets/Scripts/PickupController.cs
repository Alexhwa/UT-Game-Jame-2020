using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupController : MonoBehaviour
{
    public enum PickUpState
    {
        Idle, PickedUp
    }
    public PickUpState state;

    public abstract void Discard();
}
