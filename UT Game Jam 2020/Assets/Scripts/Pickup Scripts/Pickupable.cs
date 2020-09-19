using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : ScriptableObject
{
    //How fast the sword can be swung with this blade
    public float swingSpeed = 1;
    //How many hits the blade can do without breaking
    public float durability = 1;
    //How much damage this blade does to enemies
    public float damage = 1;
}
