using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;

    private bool facingLeft = false;
    private bool facingUp = false;
    private Animator anim;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Pickup"))){
            var pickup = collision.GetComponentInChildren<PickupController>();
            if (pickup != null)
            {
                print("Hit by: " + collision.name);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
            rb.velocity = new Vector2(horizontal, vertical) * moveSpeed;
        }
        if(vertical > 0)
        {
            facingUp = true;
        }
        else if(vertical < 0)
        {
            facingUp = false;
        }
        anim.SetBool("FacingUp", facingUp);

        if(facingLeft && horizontal > 0)
        {
            facingLeft = false;
            spriteRend.flipX = false;
        }
        else if(!facingLeft && horizontal < 0)
        {
            facingLeft = true;
            spriteRend.flipX = true;
        }
    }
}
