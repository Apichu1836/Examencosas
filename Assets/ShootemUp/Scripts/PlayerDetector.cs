using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public List<GameEntity> enemies;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameEntity enemy in enemies)
                enemy.Wake();
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if (enemies.Count > 0)
        {
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            Gizmos.DrawWireCube(bc.bounds.center, bc.bounds.size);
            foreach (GameEntity enemy in enemies)
            {
                Gizmos.DrawLine(bc.bounds.center,
                       enemy.gameObject.transform.position);
            }
        }
    }
}

