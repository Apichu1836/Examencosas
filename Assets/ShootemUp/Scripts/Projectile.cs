using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    public int damage;
    public string damageOnlyToTag;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (damageOnlyToTag == "" || other.CompareTag(damageOnlyToTag))
        {
            GameEntity hit = other.gameObject.GetComponentInParent<GameEntity>();
            if (hit != null)
            {
                hit.TakeDamage(damage);
            }
            Die();
        }
    }
    private void Update()
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < -0.1 || pos.x > 1.1)
            Die();
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}

