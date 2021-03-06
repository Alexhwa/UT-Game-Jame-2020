﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class PickupController : MonoBehaviour
{
    [Header("Weapon Stats")]
    //How fast the sword can be swung with this blade
    public float weaponSwingSpeed = 1;
    //How many hits the blade can do without breaking
    public float weaponDurability = 1;
    //How much damage this blade does to enemies
    public float weaponDamage = 1;
    public float weaponKnockback = 2;

    //[HideInInspector]
    //Used to check if projectile will hurt enemy and when throwing objects
    public bool playerOwned = false;

    protected Rigidbody2D rb;

    public enum PickUpState
    {
        Idle, PickedUp, Disabled
    }
    public PickUpState state = PickUpState.Idle;
    public static GameObject player;

    protected AudioManager audioManager;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        audioManager = FindObjectOfType<AudioManager>();
    }

    public virtual void PickUp(SwordController sword)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.DOKill();
        transform.SetParent(sword.bladePos);
        transform.localPosition = Vector3.zero;
        transform.localRotation = sword.transform.rotation;
        state = PickUpState.PickedUp;
    }
    public virtual void Discard()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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
    public virtual bool DoesDamage()
    {
        return (state == PickUpState.PickedUp && GetComponentInParent<SwordController>().swinging) || playerOwned;
    }
    public virtual IEnumerator ResetPlayerOwned(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerOwned = false;
    }
}
