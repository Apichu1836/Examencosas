using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        rb2d.velocity = movement * movementSpeed;
        UpdateState();
    }

    private void UpdateState()
    {
        bool stopped = Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0);
        animator.SetBool("isWalking", !stopped);
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
        //spriteRenderer.flipX = (movement.x < 0);
    }
}
