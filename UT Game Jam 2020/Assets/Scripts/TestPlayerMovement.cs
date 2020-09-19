using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    private const float SPEED = 75f;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float moveX = 0;
        float moveY = 0;
        
        if(Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if(Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector3 moveDirection = new Vector3(moveX, moveY).normalized;

        transform.position += moveDirection * SPEED * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Doorway passed.");
    }
}
