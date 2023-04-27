using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinearProjectile : MonoBehaviour
{
    public Vector2 direction = new Vector2(1f, 0f);
    public float speed;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb2d.velocity = direction * speed;
    }
}

