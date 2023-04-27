using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    public GameObject spawnGameObject;
    public float exitTime = 1.0f;
    public override void OnStateEnter(Animator animator,
      AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spawnGameObject != null)
        {
            Vector3 spawnPos;
            Collider2D collider = animator.gameObject.GetComponentInChildren<Collider2D>();
            if (collider != null)
                spawnPos = collider.transform.position;
            else
                spawnPos = animator.transform.position;
            Instantiate(spawnGameObject, spawnPos, Quaternion.identity);
        }
        Destroy(animator.gameObject, stateInfo.length * exitTime);
    }
}
