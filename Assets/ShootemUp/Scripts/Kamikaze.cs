using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Kamikaze : EnemyShootemUp
{
    private GameObject player;
    private Rigidbody2D rb2d;
    public float speed;
    private Animator animator;
    protected override void OnWake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Cannot find the player");
        }
        Debug.Log("Kamikaze wakes");
    }
    void Update()
    {
        if (sleeping || player == null) return;
        Vector3 direction = Vector3.Normalize(player.transform.position - transform.position);
        rb2d.velocity = direction * speed;
    }
    protected override void OnHit(int damage)
    {
        Debug.Log("Kamikaze hit");
        animator.SetTrigger("Hit");
    }
    protected override void OnDie()
    {
        Debug.Log("Kamikaze dies");
        //Destroy(gameObject);
        animator.SetTrigger("Die");
    }
}
