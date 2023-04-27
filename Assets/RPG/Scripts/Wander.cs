using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;

    public float directionChangeInterval;
    public bool followPlayer;
    Coroutine moveCoroutine;

    Rigidbody2D rb2d;
    Animator animator;

    Transform targetTransform = null;

    Vector3 endPosition;
    float currentAngle = 0;

    CircleCollider2D circleCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        StartCoroutine(WanderRoutine());
    }

    private void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(circleCollider.transform.position, circleCollider.radius);
            Gizmos.DrawLine(rb2d.position, endPosition);
        }
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move());
            yield return new
                  WaitForSeconds(directionChangeInterval);
        }
    }
    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float radians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }

    IEnumerator Move()
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }
            animator.SetBool("isWalking", true);
            Vector3 newPosition =
                    Vector3.MoveTowards(rb2d.position, endPosition, currentSpeed * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            remainingDistance = (transform.position - endPosition).sqrMagnitude;
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = other.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }

    }
}

