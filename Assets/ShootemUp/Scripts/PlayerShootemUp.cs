using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShootemUp : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb2d;
    private GameEntity lifetime;
    private Animator animator;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lifetime = GetComponent<GameEntity>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement.x != 0.0f || movement.y != 0.0f)
        {
            movement = Vector2.ClampMagnitude(movement, 1.0f);
            rb2d.velocity = movement * force;
        }
    }

    private void OnEnable()
    {
        lifetime.OnHitEvent += OnHit;
        lifetime.OnDieEvent += OnDie;
    }
    private void OnDisable()
    {
        lifetime.OnHitEvent -= OnHit;
        lifetime.OnDieEvent -= OnDie;
    }
    private void OnDie()
    {
        Debug.Log("Game over");
        //Destroy(gameObject);
        animator.SetTrigger("Die");
        CameraManagerShootemUp.Instance.ShakeCamera(5);
    }
    private void OnHit(int damage)
    {
        Debug.Log("Player hit");
        animator.SetTrigger("Hit");
        CameraManagerShootemUp.Instance.ShakeCamera(damage);
    }

}

